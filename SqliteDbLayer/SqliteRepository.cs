using Application.Repositories.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System.Data;

namespace SqliteDbLayer
{
    /// <summary>
    /// Generic repository for SQLite using Dapper and Dapper.Contrib.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public class SqliteRepository<T> : IRepository<T> where T : class
    {
        private readonly IDbConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqliteRepository{T}"/> class.
        /// </summary>
        /// <param name="connectionString">SQLite database connection string.</param>
        public SqliteRepository(string connectionString)
        {
            _db = new SqliteConnection(connectionString);
        }

        /// <summary>
        /// Asynchronously retrieves all entities with an optional limit.
        /// </summary>
        /// <param name="limit">Maximum number of rows to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, returning the list of entities.</returns>
        public Task<IEnumerable<T>> GetAll(int limit = 0)
        {
            if (limit > 0)
            {
                // Use LIMIT clause to retrieve a limited number of rows
                return _db.QueryAsync<T>($"SELECT * FROM {GetTableName()} LIMIT @Limit", new { Limit = limit });
            }
            else
            {
                // Retrieve all rows if no limit specified
                return _db.GetAllAsync<T>();
            }
        }

        /// <summary>
        /// Retrieves a paged set of entities.
        /// </summary>
        /// <param name="page">Page number.</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <returns>The paged set of entities.</returns>
        public IEnumerable<T> GetPaged(int page, int pageSize)
        {
            // Use OFFSET and FETCH clauses for paging
            return _db.Query<T>($"SELECT * FROM {GetTableName()} ORDER BY Id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY",
                new { Offset = (page - 1) * pageSize, PageSize = pageSize });
        }

        /// <summary>
        /// Retrieves entities based on a custom SQL filter.
        /// </summary>
        /// <param name="filterSql">Custom SQL filter condition.</param>
        /// <param name="parameters">Parameters for the filter condition.</param>
        /// <returns>The filtered set of entities.</returns>
        public IEnumerable<T> GetFiltered(string filterSql, object parameters)
        {
            // Apply filtering using a WHERE clause
            return _db.Query<T>($"SELECT * FROM {GetTableName()} WHERE {filterSql}", parameters);
        }

        /// <summary>
        /// Retrieves entities with sorting based on the specified column.
        /// </summary>
        /// <param name="orderByColumn">Column to be used for sorting.</param>
        /// <param name="ascending">True for ascending order, false for descending order.</param>
        /// <returns>The sorted set of entities.</returns>
        public IEnumerable<T> GetSorted(string orderByColumn, bool ascending = true)
        {
            // Apply sorting using ORDER BY clause
            var direction = ascending ? "ASC" : "DESC";
            return _db.Query<T>($"SELECT * FROM {GetTableName()} ORDER BY {orderByColumn} {direction}");
        }

        // Additional IRepository<T> methods...

        /// <summary>
        /// Retrieves an entity by its primary key.
        /// </summary>
        /// <param name="id">Primary key value.</param>
        /// <returns>The entity with the specified primary key.</returns>
        public T GetById(int id)
        {
            return _db.Get<T>(id);
        }

        /// <summary>
        /// Inserts a new entity into the database.
        /// </summary>
        /// <param name="entity">Entity to be inserted.</param>
        /// <returns>The primary key value of the inserted entity.</returns>
        public async Task<long> Insert(T entity)
        {
            return await _db.InsertAsync(entity);
        }

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        /// <returns>True if the update operation is successful, otherwise false.</returns>
        public bool Update(T entity)
        {
            return _db.Update(entity);
        }

        /// <summary>
        /// Deletes an entity from the database.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        /// <returns>True if the delete operation is successful, otherwise false.</returns>
        public bool Delete(T entity)
        {
            return _db.Delete(entity);
        }

        /// <summary>
        /// Executes a SQL query and returns a set of entities.
        /// </summary>
        /// <param name="sql">SQL query string.</param>
        /// <param name="param">Query parameters.</param>
        /// <returns>The result set of entities.</returns>
        public IEnumerable<T> Query(string sql, object param = null)
        {
            return _db.Query<T>(sql, param);
        }

        /// <summary>
        /// Executes a SQL command without returning any result set.
        /// </summary>
        /// <param name="sql">SQL command string.</param>
        /// <param name="param">Command parameters.</param>
        public void Execute(string sql, object param = null)
        {
            _db.Execute(sql, param);
        }

        /// <summary>
        /// Opens the database connection if it is closed.
        /// </summary>
        public void OpenConnection()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
        }

        /// <summary>
        /// Closes the database connection if it is open.
        /// </summary>
        public void CloseConnection()
        {
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        /// <summary>
        /// Gets the table name for the entity.
        /// </summary>
        /// <returns>The table name for the entity.</returns>
        private string GetTableName()
        {
            // Dapper.Contrib library uses the TableAttribute or class name as the table name
            var tableAttr = typeof(T).GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() as TableAttribute;

            if (tableAttr != null)
            {
                return tableAttr.Name; // If TableAttribute is defined, use its specified name
            }
            else
            {
                // If TableAttribute is not defined, use class name with _ instead of CamelCase
                return string.Concat(typeof(T).Name.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()));
            }
        }
    }
}
