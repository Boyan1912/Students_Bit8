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
                    "SELECT * FROM semester s " +
                    "JOIN discipline d " +
                    "ON s.id_semester = d.id_semester;", connection);
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
                            semester.Name = reader.GetValue(2).ToString();
                            semester.Disciplines = new List<Discipline>();
                            result.Add(semester);
                        }
                        var discipline = new Discipline((int)reader.GetValue(3), reader.GetValue(4).ToString(), reader.GetValue(5).ToString());
                        float score;
                        if (float.TryParse(reader.GetValue(6).ToString(), System.Globalization.NumberStyles.Float, null, out score))
                        {
                            discipline.Score = score;
                        }
                        semester.Disciplines.Add(discipline);
                    }

                }
                await connection.CloseAsync();
            }

            return result;
        }

        public async Task Create(int studentId, string name)
        {
            using var connection = new MySqlConnection(_connString);
            {
                await connection.OpenAsync();
                using var command = new MySqlCommand(
                    "INSERT INTO mydb.semester(name, id_student) " +
                    "VALUES('" + name + "'," + studentId + ")", connection);
                await command.ExecuteScalarAsync();
                await connection.CloseAsync();
            }
        }

    }
}
