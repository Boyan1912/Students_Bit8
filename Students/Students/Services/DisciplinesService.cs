using MySqlConnector;
using Students.Models;
using Students.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Services
{
    public class DisciplinesService : IDisciplinesService
    {
        private readonly string _connString;
        private readonly IAppRepository repo;

        public DisciplinesService(string connString)
        {
            _connString = connString;
            repo = new AppRepository(_connString);
        }

        public async Task<List<Discipline>> GetAll()
        {
            string sql = "SELECT * FROM discipline;";

            return await repo.GetResults<Discipline>(sql, (reader, result) => ParseDiscipline(reader, result));
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
            string sql = "UPDATE discipline " +
                    "SET professor_name = '" + professorName + "'" +
                    " WHERE id_discipline = " + id;
            await repo.Execute(sql);
        }

        public async Task Create(string name, string professorName, int? semesterId, float? score = null)
        {
            using var connection = new MySqlConnection(_connString);
            {
                string semId = semesterId.HasValue ? semesterId.ToString() : "null";
                string scoreStr = score.HasValue ? score.ToString().Replace(",", ".") : "null";
                string sql = "INSERT INTO discipline (name, professor_name, score, id_semester)  " +
                    "VALUES('" + name + "','" + professorName + "'," + scoreStr + "," + semId + ")";

                await repo.Execute(sql);
            }
        }

        private async Task<bool> CanBeDeleted(int id, MySqlConnection connection)
        {
            string sql = "SELECT score FROM discipline WHERE id_discipline = " + id;
            var result = await repo.GetSingleResult(sql, connection);
            float score;
            return !float.TryParse(result.ToString(), System.Globalization.NumberStyles.Float, null, out score);
        }

        private async Task ParseDiscipline(MySqlDataReader reader, List<Discipline> result)
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
    }
}
