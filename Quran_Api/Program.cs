using Application;
using DbModels;
using SqliteDbLayer;

namespace Quran_Api
{
    /// <summary>
    /// The entry point class for the Quran API application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method that initializes and runs the application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            // Create a web application builder to configure and build the application.
            var builder = WebApplication.CreateBuilder(args);

            // Add essential services to the dependency injection container.

            // Add controllers for handling HTTP requests.
            builder.Services.AddControllers();

            // Add API endpoint exploration services for documentation generation.
            builder.Services.AddEndpointsApiExplorer();

            // Add Swagger services for API documentation.
            builder.Services.AddSwaggerGen();

            // Replace with your actual SQLite connection string.
            string sqliteConnectionString = "Data Source=quran_db;";

            // Register the SQLite repository for the QuranData model in the dependency injection container.
            builder.Services.AddSingleton<IDatabaseRepository<QuranData>>(provider => new SQLiteDatabaseRepository<QuranData>(sqliteConnectionString));

            // Build the web application.
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            // Enable Swagger and Swagger UI in the development environment.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Enable authorization handling.

            // Map controllers to handle incoming HTTP requests.
            app.MapControllers();

            // Run the application.
            app.Run();
        }
    }
}
