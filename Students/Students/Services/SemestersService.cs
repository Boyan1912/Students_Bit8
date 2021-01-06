using MySqlConnector;
using Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Services
{
    public class SemestersService : ISemestersService
    {
        private readonly string _connString;

        public SemestersService(string connString)
        {
            _connString = connString;
        }

        public async Task<List<Semester>> GetAll()
        {
            var result = new List<Semester>();
            using var connection = new MySqlConnection(_connString);
            {
                await connection.OpenAsync();

                using var command = new MySqlCommand(
                    "SELECT s.id_semester, s.name, s.start_date, s.end_date, d.id_discipline, d.name, d.professor_name, d.score, " +
                        "(SELECT count(id) FROM students_semesters WHERE id_semester = s.id_semester) AS count_students FROM semester s " +
                    "LEFT JOIN discipline d ON s.id_semester = d.id_semester;", connection);
                using var reader = await command.ExecuteReaderAsync();
                {
                    while (await reader.ReadAsync())
                    {
                        int id = (int)reader.GetValue(0);
                        var semester = result.FirstOrDefault(s => s.IdSemester == id);
                        if (semester == null)
                        {
                            semester = new Semester();
                            semester.IdSemester = id;
                            semester.Name = reader.GetValue(1).ToString();
                            DateTime start;
                            if (DateTime.TryParse(reader.GetValue(2).ToString(), null, System.Globalization.DateTimeStyles.None, out start))
                            {
                                semester.StartDate = start.ToString("MM/dd/yyyy");
                            }
                            DateTime end;
                            if (DateTime.TryParse(reader.GetValue(3).ToString(), null, System.Globalization.DateTimeStyles.None, out end))
                            {
                                semester.EndDate = end.ToString("MM/dd/yyyy");
                            }
                            semester.Disciplines = new List<Discipline>();
                            result.Add(semester);
                        }
                        if (!semester.HasStudents)
                        {
                            semester.HasStudents = (long)reader.GetValue(8) > 0;
                        }
                        int disciplineId;
                        if (int.TryParse(reader.GetValue(4).ToString(), System.Globalization.NumberStyles.Integer, null, out disciplineId))
                        {
                            var discipline = new Discipline(disciplineId, reader.GetValue(5).ToString(), reader.GetValue(6).ToString());
                            float score;
                            if (float.TryParse(reader.GetValue(7).ToString(), System.Globalization.NumberStyles.Float, null, out score))
                            {
                                discipline.Score = score;
                            }
                            semester.Disciplines.Add(discipline);
                        }
                    }

                }
                await connection.CloseAsync();
            }

            return result;
        }

        public async Task Create(int? studentId, string name)
        {
            using var connection = new MySqlConnection(_connString);
            {
                string script = $"INSERT INTO semester(name) VALUES('{name}');";
                if (studentId.HasValue)
                {
                    script += $"INSERT INTO students_semesters (id_student, id_semester) VALUES({studentId}, (SELECT LAST_INSERT_ID()));";
                }
                await connection.OpenAsync();
                string studIdStr = studentId.HasValue ? studentId.ToString() : "null";
                using var command = new MySqlCommand(script, connection);
                await command.ExecuteScalarAsync();
                await connection.CloseAsync();
            }
        }

        public async Task Delete(int id)
        {
            using var connection = new MySqlConnection(_connString);
            {
                await connection.OpenAsync();
                if (!await CanBeDeleted(id, connection))
                {
                    throw new ArgumentException("Only semesters without any students can be deleted!");
                }
                using var command = new MySqlCommand("DELETE FROM semester WHERE id_semester = " + id, connection);
                await command.ExecuteScalarAsync();
                await connection.CloseAsync();
            }
        }

        private async Task<bool> CanBeDeleted(int id, MySqlConnection connection)
        {
            using var command = new MySqlCommand("SELECT count(id) FROM students_semesters WHERE id_semester = " + id, connection);
            using var reader = await command.ExecuteReaderAsync();
            {
                await reader.ReadAsync();
                long countRows = (long)reader.GetValue(0);

                return countRows == 0;
            }
        }
    }
}
