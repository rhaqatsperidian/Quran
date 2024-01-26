using Application.Repositories.Interfaces;

namespace Application
{
    /// <summary>
    /// Interface representing a generic database repository factory.
    /// </summary>
    /// <typeparam name="T">Type of the entity for which the repository is created.</typeparam>
    public interface IDatabaseRepository<T> where T : class
    {
        /// <summary>
        /// Gets an instance of the repository for the specified entity type.
        /// </summary>
        /// <returns>An instance of the repository for the specified entity type.</returns>
        IRepository<T> GetRepository();
    }
}
