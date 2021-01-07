using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Repositories
{
    public class AppRepository : IAppRepository
    {
        private readonly string _connString;

        public AppRepository(string connString)
        {
            _connString = connString;
        }

        public async Task Execute(string sql, MySqlConnection connection = null)
        {
            connection = connection ?? new MySqlConnection(_connString);
            using (connection)
            {
                await connection.OpenAsync();
                using var command = new MySqlCommand(sql, connection);
                await command.ExecuteScalarAsync();
                await connection.CloseAsync();
            }
        }

        public async Task<object> GetSingleResult(string sql, MySqlConnection connection)
        {
            using var command = new MySqlCommand(sql, connection);
            {
                using var reader = await command.ExecuteReaderAsync();
                {
                    await reader.ReadAsync();
                    return reader.GetValue(0);
                }
            }
        }

        public async Task<List<T>> GetResults<T>(string sql, Func<MySqlDataReader, List<T>, Task> parseFunc)
        {
            var result = new List<T>();
            using var connection = new MySqlConnection(_connString);
            {
                await connection.OpenAsync();

                using var command = new MySqlCommand(sql, connection);
                using var reader = await command.ExecuteReaderAsync();
                {
                    await parseFunc(reader, result);
                }
                await connection.CloseAsync();
            }

            return result;
        }
    }
}
