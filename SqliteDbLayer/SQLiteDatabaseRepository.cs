using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteDbLayer
{
    public class SQLiteDatabaseRepository<T> : IDatabaseRepository<T> where T : class
    {
        private readonly string _connectionString;

        public SQLiteDatabaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IRepository<T> GetRepository()
        {
            return new SqliteRepository<T>(_connectionString);
        }
    }

}
