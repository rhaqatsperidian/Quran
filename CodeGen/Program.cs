using CodeGen.ExtensionMethods;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

namespace CodeGen
{
    class Program
    {
        static void Main()
        {
            string dbPath = "quran_db.db"; // Replace with your SQLite database path
            string outputFolder = "output_classes"; // Output folder for generated class files

            using (var dbConnection = new SqliteConnection($"Data Source={dbPath}"))
            {
                dbConnection.Open();

                var tables = dbConnection.Query<string>("SELECT name FROM sqlite_master WHERE type='table';").ToList();

                if (tables.Any())
                {
                    Directory.CreateDirectory(outputFolder);

                    foreach (var tableName in tables)
                    {
                        var classCode = GenerateClassCode(dbConnection, tableName);
                        File.WriteAllText(Path.Combine(outputFolder, $"{tableName}.cs"), classCode);
                    }

                    Console.WriteLine("Class files generated successfully.");
                }
                else
                {
                    Console.WriteLine("No tables found in the database.");
                }
            }
        }

        static string GenerateClassCode(IDbConnection dbConnection, string tableName)
        {
            var columns = dbConnection.Query($"PRAGMA table_info({tableName});").ToList();
            var className = tableName.Replace("_", "").ToTitleCase(); // Convert table name to PascalCase

            var classCode = $@"using System;

namespace YourNamespace
{{
    public class {className}
    {{
";

            foreach (var column in columns)
            {
                var columnName = column.name.ToString();
                var columnType = column.type.ToString();

                classCode += $@"        public {GetCSharpType(columnType)} {columnName} {{ get; set; }}
";
            }

            classCode += $@"    }}
}}";

            return classCode;
        }

        static string GetCSharpType(string columnType)
        {
            switch (columnType.ToLower())
            {
                case "integer":
                    return "int";
                case "text":
                    return "string";
                case "real":
                    return "double";
                default:
                    return "object";
            }
        }
    }
}
