using MatchInvest.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace MatchInvest.Data
{
    public class InvestorRepository
    {
        private readonly DatabaseContext _context;

        public InvestorRepository()
        {
            _context = new DatabaseContext();
        }

        public void AddInvestor(Investor investor)
        {
            using (var connection = new SqliteConnection(_context.DatabasePath))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    INSERT INTO Investors (Nome, CapitalDisponivel, ApetiteRisco, Objetivos, Preferencias)
                    VALUES (@Nome, @CapitalDisponivel, @ApetiteRisco, @Objetivos, @Preferencias);
                ";
                command.Parameters.AddWithValue("@Nome", investor.Nome);
                command.Parameters.AddWithValue("@CapitalDisponivel", investor.CapitalDisponivel);
                command.Parameters.AddWithValue("@ApetiteRisco", investor.ApetiteRisco);
                command.Parameters.AddWithValue("@Objetivos", investor.Objetivos);
                command.Parameters.AddWithValue("@Preferencias", investor.Preferencias);
                command.ExecuteNonQuery();
            }
        }

        public Investor GetInvestorById(int id)
        {
            using (var connection = new SqliteConnection(_context.DatabasePath))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Investors WHERE Id = @Id;";
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Investor
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            CapitalDisponivel = reader.GetDecimal(2),
                            ApetiteRisco = reader.GetString(3),
                            Objetivos = reader.GetString(4),
                            Preferencias = reader.GetString(5)
                        };
                    }
                    return null;
                }
            }
        }

        public void UpdateInvestor(Investor investor)
        {
            using (var connection = new SqliteConnection(_context.DatabasePath))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    UPDATE Investors
                    SET Nome = @Nome, CapitalDisponivel = @CapitalDisponivel, ApetiteRisco = @ApetiteRisco,
                    Objetivos = @Objetivos, Preferencias = @Preferencias
                    WHERE Id = @Id;
                ";
                command.Parameters.AddWithValue("@Nome", investor.Nome);
                command.Parameters.AddWithValue("@CapitalDisponivel", investor.CapitalDisponivel);
                command.Parameters.AddWithValue("@ApetiteRisco", investor.ApetiteRisco);
                command.Parameters.AddWithValue("@Objetivos", investor.Objetivos);
                command.Parameters.AddWithValue("@Preferencias", investor.Preferencias);
                command.Parameters.AddWithValue("@Id", investor.Id);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteInvestor(int id)
        {
            using (var connection = new SqliteConnection(_context.DatabasePath))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Investors WHERE Id = @Id;";
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        // --- MÉTODOS QUE DEVEM SER ADICIONADOS ---

        // Método para listar todos os investidores
        public List<Investor> GetAllInvestors()
        {
            var investors = new List<Investor>();
            using (var connection = new SqliteConnection(_context.DatabasePath))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Investors;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        investors.Add(new Investor
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            CapitalDisponivel = reader.GetDecimal(2),
                            ApetiteRisco = reader.GetString(3),
                            Objetivos = reader.GetString(4),
                            Preferencias = reader.GetString(5)
                        });
                    }
                }
            }
            return investors;
        }

        // Método para obter o ID do último registro inserido
        public int GetLastInsertedId()
        {
            using (var connection = new SqliteConnection(_context.DatabasePath))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT last_insert_rowid();";
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }
    }
}