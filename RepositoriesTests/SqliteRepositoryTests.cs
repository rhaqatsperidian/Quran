using DbModels;
using SqliteDbLayer;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RepositoriesTests
{
    /// <summary>
    /// Integration tests for the <see cref="SqliteRepository{T}"/> with the <see cref="QuranData"/> entity.
    /// </summary>
    public class SqliteRepositoryIntegrationTests : IDisposable
    {
        private readonly SqliteRepository<QuranData> _repository;
        private readonly string _testDbConnectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqliteRepositoryIntegrationTests"/> class.
        /// </summary>
        public SqliteRepositoryIntegrationTests()
        {
            // Use a test SQLite database
            _testDbConnectionString = "Data Source=quran_db";
            _repository = new SqliteRepository<QuranData>(_testDbConnectionString);
        }

        /// <summary>
        /// Disposes of resources used by the test class.
        /// </summary>
        public void Dispose()
        {
            // Clean up resources after each test
            _repository.CloseConnection();
        }

        /// <summary>
        /// Tests whether the GetAll method retrieves all entities.
        /// </summary>
        [Fact]
        public async Task GetAll_ShouldRetrieveAllEntities()
        {
            // Arrange
            _repository.OpenConnection();

            // Act
            var list = await _repository.GetAll();

            // Assert
            Assert.NotEmpty(list);
        }

        /// <summary>
        /// Tests whether the GetById method retrieves an entity by its identifier.
        /// </summary>
        [Fact]
        public void GetById_ShouldRetrieveEntityById()
        {
            // Arrange
            _repository.OpenConnection();

            // Act
            var retrievedEntity = _repository.GetById(1);

            // Assert
            Assert.NotNull(retrievedEntity);
        }
    }
}
