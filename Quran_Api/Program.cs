
using Application;
using DbModels;
using SqliteDbLayer;

namespace Quran_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Replace with your actual SQLite connection string
            string sqliteConnectionString = "Data Source=quran_db;";

            // Register the SQLite repository
            builder.Services.AddTransient<IDatabaseRepository<QuranData>>(provider => new SQLiteDatabaseRepository<QuranData>(sqliteConnectionString));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
