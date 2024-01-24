namespace Application
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(int limit = 0);
        T GetById(int id);
        Task<long> Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        IEnumerable<T> Query(string sql, object param = null);
        void Execute(string sql, object param = null);
        void OpenConnection();
        void CloseConnection();
        IEnumerable<T> GetPaged(int page, int pageSize);
        IEnumerable<T> GetFiltered(string filterSql, object parameters);
        IEnumerable<T> GetSorted(string orderByColumn, bool ascending = true);
    }
}
