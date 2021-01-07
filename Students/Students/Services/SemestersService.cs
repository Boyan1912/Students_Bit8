using MySqlConnector;
using Students.Models;
using Students.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Services
{
    public class SemestersService : ISemestersService
    {
        private readonly string _connString;
        private readonly IAppRepository repo;

        public SemestersService(string connString)
        {
            _connString = connString;
            repo = new AppRepository(_connString);
        }

        public async Task<List<Semester>> GetAll()
        {
            string sql = "SELECT s.id_semester, s.name, s.start_date, s.end_date, d.id_discipline, d.name, d.professor_name, d.score, " +
                        "(SELECT count(id) FROM students_semesters WHERE id_semester = s.id_semester) AS count_students FROM semester s " +
                    "LEFT JOIN discipline d ON s.id_semester = d.id_semester;";

            return await repo.GetResults<Semester>(sql, (reader, result) => ParseSemester(reader, result));
        }

        public async Task Create(int? studentId, string name, string startDate, string endDate)
        {
            using var connection = new MySqlConnection(_connString);
            {
                string script = $"INSERT INTO semester(name, start_date, end_date) VALUES('{name}',STR_TO_DATE('{startDate}', '%d/%m/%Y'),STR_TO_DATE('{endDate}', '%d/%m/%Y'));";
                if (studentId.HasValue)
                {
                    script += $"INSERT INTO students_semesters (id_student, id_semester) VALUES({studentId}, (SELECT LAST_INSERT_ID()));";
                }
                string studIdStr = studentId.HasValue ? studentId.ToString() : "null";
                await repo.Execute(script);
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
            long countRows = (long)await repo.GetSingleResult("SELECT count(id) FROM students_semesters WHERE id_semester = " + id, connection);

            return countRows == 0;
        }

        private async Task ParseSemester(MySqlDataReader reader, List<Semester> result)
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
    }
}
