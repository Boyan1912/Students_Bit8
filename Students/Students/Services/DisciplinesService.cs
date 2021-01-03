using MySqlConnector;
using Students.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Services
{
    public class DisciplinesService : IDisciplinesService
    {
        private readonly string _connString;

        public DisciplinesService(string connString)
        {
            _connString = connString;
        }

        public async Task<List<Discipline>> GetAll()
        {
            var result = new List<Discipline>();
            using var connection = new MySqlConnection(_connString);
            {
                await connection.OpenAsync();

                using var command = new MySqlCommand("SELECT * FROM discipline;", connection);
                using var reader = await command.ExecuteReaderAsync();
                {
                    while (await reader.ReadAsync())
                    {
                        var discipline = new Discipline((int)reader.GetValue(0), reader.GetValue(1).ToString(), reader.GetValue(2).ToString());
                        float score;
                        if (float.TryParse(reader.GetValue(3).ToString(), System.Globalization.NumberStyles.Float, null, out score))
                        {
                            discipline.Score = score;
                        }

                        result.Add(discipline);
                    }
                    
                }
                await connection.CloseAsync();
            }

            return result;
        }

        public async Task Delete(int id)
        {
            using var connection = new MySqlConnection(_connString);
            {
                await connection.OpenAsync();

                if (!await CanBeDeleted(id, connection))
                {
                    throw new ArgumentException("Only disciplines without scores can be deleted!");
                }

                using var command = new MySqlCommand("DELETE FROM discipline WHERE id_discipline = " + id, connection);
                await command.ExecuteScalarAsync();
                await connection.CloseAsync();
            }
        }

        public async Task UpdateProfessorName(int id, string professorName)
        {
            using var connection = new MySqlConnection(_connString);
            {
                await connection.OpenAsync();
                using var command = new MySqlCommand(
                    "UPDATE discipline " +
                    "SET professor_name = '" + professorName + "'" +
                    " WHERE id_discipline = " + id, connection);
                await command.ExecuteScalarAsync();
                await connection.CloseAsync();
            }
        }

        private async Task<bool> CanBeDeleted(int id, MySqlConnection connection)
        {
                using var command = new MySqlCommand("SELECT score FROM discipline WHERE id_discipline = " + id, connection);
                using var reader = await command.ExecuteReaderAsync();
                {
                    await reader.ReadAsync();
                    float score;
                    if (float.TryParse(reader.GetValue(0).ToString(), System.Globalization.NumberStyles.Float, null, out score))
                    {
                        return false;
                    }
                }
        
            return true;
        }
    }
}
