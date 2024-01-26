using Microsoft.Data.Sqlite;
using System.Data;

namespace Application.Repositories.Base
{
    public abstract class BaseRepo
    {
        private readonly IDbConnection _db;

        public BaseRepo(string cnnString)
        {
            _db = new SqliteConnection(cnnString);
        }

        public IDbConnection Db()
        {
            return _db;
        }
    }
}
