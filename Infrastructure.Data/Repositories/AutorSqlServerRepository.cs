using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data.Models;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Data.Repositories
{
    public class AutorSqlServerRepository
    {
        private const string ConnectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GRLEDC21N1sql;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //TODO: Adicionar arquivo do banco dentro do projeto e do git
        public IEnumerable<AutorModel> GetAll(string search = null)
        {
            using var sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            var command = sqlConnection.CreateCommand();

            var commandText = "SELECT * FROM Autor";

            if (!string.IsNullOrWhiteSpace(search))
            {
                commandText += " WHERE Nome LIKE @search OR UltimoNome LIKE @search";

                command.Parameters
                    .Add("@search", SqlDbType.NVarChar)
                    .Value = $"%{search}%";
            }

            command.CommandType = CommandType.Text;
            command.CommandText = commandText;

            var reader = command.ExecuteReader();

            var autores = new List<AutorModel>();
            while (reader.Read())
            {
                var autor = new AutorModel
                {
                    Id = reader.GetFieldValue<int>("Id"),
                    Nacionalidade = reader.GetFieldValue<string>("Nacionalidade"),
                    Nascimento = reader.GetFieldValue<DateTime>("Nascimento"),
                    Nome = reader.GetFieldValue<string>("Nome"),
                    UltimoNome = reader.GetFieldValue<string>("UltimoNome"),
                    QuantidadeLivrosPublicados = reader.GetFieldValue<int>("QuantidadeLivrosPublicados")
                };
                autores.Add(autor);
            }

            return autores;
        }

        public AutorModel GetById(int id)
        {
            using var sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            var command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT Id, Nacionalidade, Nascimento, Nome, UltimoNome, QuantidadeLivrosPublicados FROM Autor WHERE Id = @id;";

            command.Parameters
                .Add("@id", SqlDbType.Int)
                .Value = id;

            var reader = command.ExecuteReader();

            var canRead = reader.Read();
            if (!canRead)
            {
                return null;
            }

            var autor = new AutorModel
            {
                Id = reader.GetFieldValue<int>(0),
                Nacionalidade = reader.GetFieldValue<string>(1),
                Nascimento = reader.GetFieldValue<DateTime>(2),
                Nome = reader.GetFieldValue<string>(3),
                UltimoNome = reader.GetFieldValue<string>(4),
                QuantidadeLivrosPublicados = reader.GetFieldValue<int>(5)
            };

            return autor;
        }

        public AutorModel Create(AutorModel autorModel)
        {
            using var sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            var command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText =
                @"INSERT INTO Autor" +
                "	(Nome, UltimoNome, Nacionalidade, QuantidadeLivrosPublicados, Nascimento)" +
                "	OUTPUT INSERTED.Id" +
                "	VALUES (@nome, @ultimoNome, @nacionalidade, @quantidadeLivrosPublicados, @nascimento);";

            command.Parameters
                .Add("@nome", SqlDbType.NVarChar)
                .Value = autorModel.Nome;
            command.Parameters
                .Add("@ultimoNome", SqlDbType.NVarChar)
                .Value = autorModel.UltimoNome;
            command.Parameters
                .Add("@nacionalidade", SqlDbType.NVarChar)
                .Value = autorModel.Nacionalidade;
            command.Parameters
                .Add("@quantidadeLivrosPublicados", SqlDbType.Int)
                .Value = autorModel.QuantidadeLivrosPublicados;
            command.Parameters
                .Add("@nascimento", SqlDbType.DateTime2)
                .Value = autorModel.Nascimento;

            var scalarResult = command.ExecuteScalar();

            autorModel.Id = (int) scalarResult;

            return autorModel;
        }

        public AutorModel Edit(AutorModel autorModel)
        {
            using var sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            var command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText =
                @"UPDATE Autor
	SET Nome = @nome, UltimoNome = @ultimoNome, Nacionalidade = @nacionalidade,
        QuantidadeLivrosPublicados = @quantidadeLivrosPublicados, Nascimento = @nascimento
	WHERE Id = @id;";

            command.Parameters
                .Add("@nome", SqlDbType.NVarChar)
                .Value = autorModel.Nome;
            command.Parameters
                .Add("@ultimoNome", SqlDbType.NVarChar)
                .Value = autorModel.UltimoNome;
            command.Parameters
                .Add("@nacionalidade", SqlDbType.NVarChar)
                .Value = autorModel.Nacionalidade;
            command.Parameters
                .Add("@quantidadeLivrosPublicados", SqlDbType.Int)
                .Value = autorModel.QuantidadeLivrosPublicados;
            command.Parameters
                .Add("@nascimento", SqlDbType.DateTime2)
                .Value = autorModel.Nascimento;
            command.Parameters
                .Add("@id", SqlDbType.Int)
                .Value = autorModel.Id;

            command.ExecuteScalar();

            return autorModel;
        }

        public void Delete(int id)
        {
            using var sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            var command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM Autor WHERE Id = @id;";

            command.Parameters
                .Add("@id", SqlDbType.Int)
                .Value = id;

            command.ExecuteScalar();
        }
    }
}
