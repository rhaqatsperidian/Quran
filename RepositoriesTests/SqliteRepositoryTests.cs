using DbModels;
using SqliteDbLayer;

namespace RepositoriesTests
{
    namespace RepositoriesTests
    {
        public class SqliteRepositoryIntegrationTests : IDisposable
        {
            private readonly SqliteRepository<QuranData> _repository;
            private readonly string _testDbConnectionString;

            public SqliteRepositoryIntegrationTests()
            {
                // Use a test SQLite database
                _testDbConnectionString = "Data Source=quran_db";
                _repository = new SqliteRepository<QuranData>(_testDbConnectionString);
            }

            public void Dispose()
            {
                // Clean up resources after each test
                _repository.CloseConnection();
            }

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
}