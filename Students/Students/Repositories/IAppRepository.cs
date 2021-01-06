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
        //Task<MySqlDataReader> GetResults(string sql);
        Task<object> GetSingleResult(string sql, MySqlConnection connection);
    }
}
