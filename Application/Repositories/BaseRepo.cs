using Microsoft.Data.Sqlite;
using System.Data;

namespace Application.Repositories
{
    public abstract class BaseRepo
    {
        private readonly IDbConnection _db;

        public BaseRepo(string cnnString)
        {
            _db = new SqliteConnection(cnnString);
        }

        public IDbConnection GetDb()
        {
            return _db;
        }
    }
}
