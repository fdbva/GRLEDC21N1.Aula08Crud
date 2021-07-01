using System;
using System.Collections.Generic;
using System.Data;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data.Repositories
{
    public class LivroSqlServerRepository : ILivroRepository
    {
        private readonly string _connectionString;

        public LivroSqlServerRepository(
            IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BibliotecaDatabase");
        }

        public IEnumerable<LivroModel> GetAll(bool orderAscendant, string search = null)
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            var command = sqlConnection.CreateCommand();

            var commandText = "SELECT * FROM Livro";

            if (!string.IsNullOrWhiteSpace(search))
            {
                commandText += " WHERE Titulo LIKE @search";

                command.Parameters
                    .Add("@search", SqlDbType.NVarChar)
                    .Value = $"%{search}%";
            }

            var order = orderAscendant ? "ASC" : "DESC";
            commandText += $" ORDER BY Titulo {order}";

            command.CommandType = CommandType.Text;
            command.CommandText = commandText;

            var reader = command.ExecuteReader();

            var livros = new List<LivroModel>();
            while (reader.Read())
            {
                var livro = new LivroModel()
                {
                    Id = reader.GetFieldValue<int>("Id"),
                    Titulo = reader.GetFieldValue<string>("Titulo"),
                    Isbn = reader.GetFieldValue<string>("Isbn"),
                    Lancamento = reader.GetFieldValue<DateTime>("Lancamento"),
                    AutorId = reader.GetFieldValue<int>("AutorId"),
                };
                livros.Add(livro);
            }

            return livros;
        }

        public LivroModel GetById(int id)
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            var command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT Id, Titulo, Isbn, Lancamento, AutorId FROM Livro WHERE Id = @id;";

            command.Parameters
                .Add("@id", SqlDbType.Int)
                .Value = id;

            var reader = command.ExecuteReader();

            var canRead = reader.Read();
            if (!canRead)
            {
                return null;
            }

            var livro = new LivroModel
            {
                Id = reader.GetFieldValue<int>(0),
                Titulo = reader.GetFieldValue<string>(1),
                Isbn = reader.GetFieldValue<string>(2),
                Lancamento = reader.GetFieldValue<DateTime>(3),
                AutorId = reader.GetFieldValue<int>(4),
            };

            return livro;
        }

        public LivroModel Create(LivroModel livroModel)
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            var command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText =
                @"INSERT INTO Livro" +
                "	(Titulo, Isbn, Lancamento, AutorId)" +
                "	OUTPUT INSERTED.Id" +
                "	VALUES (@titulo, @isbn, @lancamento, @autorId);";

            command.Parameters
                .Add("@titulo", SqlDbType.NVarChar)
                .Value = livroModel.Titulo;
            command.Parameters
                .Add("@isbn", SqlDbType.NVarChar)
                .Value = livroModel.Isbn;
            command.Parameters
                .Add("@lancamento", SqlDbType.DateTime2)
                .Value = livroModel.Lancamento;
            command.Parameters
                .Add("@autorId", SqlDbType.Int)
                .Value = livroModel.AutorId;

            var scalarResult = command.ExecuteScalar();

            livroModel.Id = (int)scalarResult;

            return livroModel;
        }

        public LivroModel Edit(LivroModel livroModel)
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            var command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText =
                @"UPDATE Livro
	SET Titulo = @titulo, Isbn = @isbn, Lancamento = @lancamento, AutorId = @autorId
	WHERE Id = @id;";

            command.Parameters
                .Add("@titulo", SqlDbType.NVarChar)
                .Value = livroModel.Titulo;
            command.Parameters
                .Add("@isbn", SqlDbType.NVarChar)
                .Value = livroModel.Isbn;
            command.Parameters
                .Add("@lancamento", SqlDbType.DateTime2)
                .Value = livroModel.Lancamento;
            command.Parameters
                .Add("@autorId", SqlDbType.Int)
                .Value = livroModel.AutorId;
            command.Parameters
                .Add("@id", SqlDbType.Int)
                .Value = livroModel.Id;

            command.ExecuteScalar();

            return livroModel;
        }

        public void Delete(int id)
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            var command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM Livro WHERE Id = @id;";

            command.Parameters
                .Add("@id", SqlDbType.Int)
                .Value = id;

            command.ExecuteScalar();
        }
    }
}
