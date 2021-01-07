using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Repositories
{
    public interface IAppRepository
    {
        Task Execute(string sql, MySqlConnection connection = null);
        Task<object> GetSingleResult(string sql, MySqlConnection connection);
        Task<List<T>> GetResults<T>(string sql, Func<MySqlDataReader, List<T>, Task> parseFunc);
    }
}
