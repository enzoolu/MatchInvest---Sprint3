using Microsoft.Data.Sqlite;
using System.IO;

namespace MatchInvest.Data
{
    public class DatabaseContext
    {
        public string DatabasePath { get; }

        public DatabaseContext()
        {
            var dbFilePath = Path.Combine(Directory.GetCurrentDirectory(), "MatchInvest.db");
            DatabasePath = $"Data Source={dbFilePath}";

            if (!File.Exists(dbFilePath))
            {
                InitializeDatabase();
            }
        }

        private void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(DatabasePath))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    CREATE TABLE Investors (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nome TEXT NOT NULL,
                        CapitalDisponivel REAL,
                        ApetiteRisco TEXT,
                        Objetivos TEXT,
                        Preferencias TEXT
                    );

                    CREATE TABLE Assessores (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nome TEXT NOT NULL,
                        Certificacoes TEXT,
                        Especializacao TEXT,
                        Atuacao TEXT
                    );
                ";
                command.ExecuteNonQuery();
            }
        }
    }
}