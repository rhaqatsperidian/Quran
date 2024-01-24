namespace Application
{
    public interface IDatabaseRepository<T> where T : class
    {
        IRepository<T> GetRepository();
    }

}
