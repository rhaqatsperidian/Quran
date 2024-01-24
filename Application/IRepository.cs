namespace Application
{
    /// <summary>
    /// Interface defining standard CRUD operations for a generic repository.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Asynchronously retrieves all entities from the repository with an optional limit.
        /// </summary>
        Task<IEnumerable<T>> GetAll(int limit = 0);

        /// <summary>
        /// Retrieves a single entity from the repository by its unique identifier.
        /// </summary>
        T GetById(int id);

        /// <summary>
        /// Asynchronously inserts a new entity into the repository and returns its unique identifier.
        /// </summary>
        Task<long> Insert(T entity);

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        bool Update(T entity);

        /// <summary>
        /// Deletes an entity from the repository.
        /// </summary>
        bool Delete(T entity);

        /// <summary>
        /// Executes a parameterized SQL query and returns the result as a collection of entities.
        /// </summary>
        IEnumerable<T> Query(string sql, object param = null);

        /// <summary>
        /// Executes a parameterized SQL command without returning any result.
        /// </summary>
        void Execute(string sql, object param = null);

        /// <summary>
        /// Opens the connection to the underlying database.
        /// </summary>
        void OpenConnection();

        /// <summary>
        /// Closes the connection to the underlying database.
        /// </summary>
        void CloseConnection();

        /// <summary>
        /// Retrieves a specified page of entities from the repository.
        /// </summary>
        IEnumerable<T> GetPaged(int page, int pageSize);

        /// <summary>
        /// Retrieves entities from the repository based on a specified filter condition.
        /// </summary>
        IEnumerable<T> GetFiltered(string filterSql, object parameters);

        /// <summary>
        /// Retrieves entities from the repository and sorts them based on the specified column and order.
        /// </summary>
        IEnumerable<T> GetSorted(string orderByColumn, bool ascending = true);
    }
}
