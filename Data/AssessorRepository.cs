using MatchInvest.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace MatchInvest.Data
{
    public class AssessorRepository
    {
        private readonly DatabaseContext _context;

        public AssessorRepository()
        {
            _context = new DatabaseContext();
        }

        public void AddAssessor(Assessor assessor)
        {
            using (var connection = new SqliteConnection(_context.DatabasePath))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    INSERT INTO Assessores (Nome, Certificacoes, Especializacao, Atuacao)
                    VALUES (@Nome, @Certificacoes, @Especializacao, @Atuacao);
                ";
                command.Parameters.AddWithValue("@Nome", assessor.Nome);
                command.Parameters.AddWithValue("@Certificacoes", assessor.Certificacoes);
                command.Parameters.AddWithValue("@Especializacao", assessor.Especializacao);
                command.Parameters.AddWithValue("@Atuacao", assessor.Atuacao);
                command.ExecuteNonQuery();
            }
        }

        public Assessor GetAssessorById(int id)
        {
            using (var connection = new SqliteConnection(_context.DatabasePath))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Assessores WHERE Id = @Id;";
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Assessor
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Certificacoes = reader.GetString(2),
                            Especializacao = reader.GetString(3),
                            Atuacao = reader.GetString(4)
                        };
                    }
                    return null;
                }
            }
        }

        public void UpdateAssessor(Assessor assessor)
        {
            using (var connection = new SqliteConnection(_context.DatabasePath))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    UPDATE Assessores
                    SET Nome = @Nome, Certificacoes = @Certificacoes, Especializacao = @Especializacao,
                    Atuacao = @Atuacao
                    WHERE Id = @Id;
                ";
                command.Parameters.AddWithValue("@Nome", assessor.Nome);
                command.Parameters.AddWithValue("@Certificacoes", assessor.Certificacoes);
                command.Parameters.AddWithValue("@Especializacao", assessor.Especializacao);
                command.Parameters.AddWithValue("@Atuacao", assessor.Atuacao);
                command.Parameters.AddWithValue("@Id", assessor.Id);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteAssessor(int id)
        {
            using (var connection = new SqliteConnection(_context.DatabasePath))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Assessores WHERE Id = @Id;";
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        public List<Assessor> GetAssessoresByEspecializacao(string especializacao)
        {
            var assessores = new List<Assessor>();
            using (var connection = new SqliteConnection(_context.DatabasePath))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Assessores WHERE Especializacao LIKE @Especializacao;";
                command.Parameters.AddWithValue("@Especializacao", $"%{especializacao}%");

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        assessores.Add(new Assessor
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Certificacoes = reader.GetString(2),
                            Especializacao = reader.GetString(3),
                            Atuacao = reader.GetString(4)
                        });
                    }
                }
            }
            return assessores;
        }

        public List<Assessor> GetAllAssessors()
        {
            var assessors = new List<Assessor>();
            using (var connection = new SqliteConnection(_context.DatabasePath))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Assessores;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        assessors.Add(new Assessor
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Certificacoes = reader.GetString(2),
                            Especializacao = reader.GetString(3),
                            Atuacao = reader.GetString(4)
                        });
                    }
                }
            }
            return assessors;
        }

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