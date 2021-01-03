using MySqlConnector;
using Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly string _connString;

        public StudentsService(string connString)
        {
            _connString = connString;
        }

        public async Task<List<Student>> GetAll()
        {
            var result = new List<Student>();
            using var connection = new MySqlConnection(_connString);
            {
                await connection.OpenAsync();

                using var command = new MySqlCommand(
                    "SELECT * FROM mydb.student s " +
                    "JOIN mydb.semester se ON s.id_student = se.id_student " +
                    "JOIN mydb.discipline d ON d.id_semester = se.id_semester;", connection);
                using var reader = await command.ExecuteReaderAsync();
                {
                    while (await reader.ReadAsync())
                    {
                        int id = (int)reader.GetValue(0);
                        var student = result.FirstOrDefault(s => s.IdStudent == id);
                        if (student == null)
                        {
                            student = new Student();
                            student.IdStudent = id;
                            student.FirstName = reader.GetValue(1).ToString();
                            student.LastName = reader.GetValue(2).ToString();
                            student.Semesters = new List<Semester>();
                            result.Add(student);
                        }
                        int idSemester = (int)reader.GetValue(3);
                        var semester = student.Semesters.FirstOrDefault(se => se.IdSemester == idSemester);
                        if (semester == null)
                        {
                            semester = new Semester();
                            semester.IdSemester = idSemester;
                            semester.Name = reader.GetValue(5).ToString();
                            semester.Disciplines = new List<Discipline>();
                            student.Semesters.Add(semester);
                        }
                        int idDiscipline = (int)reader.GetValue(6);
                        var discipline = semester.Disciplines.FirstOrDefault(d => d.IdDiscipline == idDiscipline);
                        if (discipline == null)
                        {
                            discipline = new Discipline(idDiscipline, reader.GetValue(7).ToString(), reader.GetValue(8).ToString());
                            float score;
                            if (float.TryParse(reader.GetValue(9).ToString(), System.Globalization.NumberStyles.Float, null, out score))
                            {
                                discipline.Score = score;
                            }
                        }
                        semester.Disciplines.Add(discipline);
                    }

                }
                await connection.CloseAsync();
            }

            return result;
        }
    }
}
