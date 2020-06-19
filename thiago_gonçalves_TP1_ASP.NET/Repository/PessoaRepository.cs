using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using thiago_gonçalves_TP1_ASP.NET.Models;

namespace thiago_gonçalves_TP1_ASP.NET.Repository
{
    public class PessoaRepository
    {
        private string ConnectionString { get; set; }

        public PessoaRepository(IConfiguration configuration)
        {
            this.ConnectionString = configuration.GetConnectionString("PessoaConnection");
        }

        public void Save(PessoaModel pessoa)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                var sql = @" INSERT INTO Pessoa(Nome, DataDeAniversario, DiasRestantes)
                             VALUES (@P1, @P2, @P3)
                ";

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.AddWithValue("P1", pessoa.Nome);
                sqlCommand.Parameters.AddWithValue("P2", pessoa.DataDeAniversario);
                sqlCommand.Parameters.AddWithValue("P3", pessoa.DiasRestantes);

                sqlCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Update(PessoaModel pessoa)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                var sql = @" UPDATE PESSOA
                             SET Nome = @P1,
                             DataDeAniversario = @P2
                             WHERE Id = @P3
                ";

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.AddWithValue("P1", pessoa.Nome);
                sqlCommand.Parameters.AddWithValue("P2", pessoa.DataDeAniversario);
                sqlCommand.Parameters.AddWithValue("P3", pessoa.Id);

                sqlCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Delete(PessoaModel pessoa)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                var sql = @" DELETE FROM Pessoa
                             WHERE Id = @P1
                ";

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.AddWithValue("P1", pessoa.Id);

                sqlCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        public List<PessoaModel> GetAll()
        {
            List<PessoaModel> result = new List<PessoaModel>();

            using (var connection = new SqlConnection(this.ConnectionString))
            {

                var sql = @" SELECT Id, Nome, DataDeAniversario, DiasRestantes FROM Pessoa";

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = sql;

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    PessoaModel pessoa = new PessoaModel()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Nome = reader["Nome"].ToString(),
                        DataDeAniversario = Convert.ToDateTime(reader["DataDeAniversario"]),
                        DiasRestantes = int.Parse(reader["DiasRestantes"].ToString())
                    };

                    result.Add(pessoa);
                }

                connection.Close();
            }

            return result;
        }

        public PessoaModel GetById(int id)
        {
            List<PessoaModel> result = new List<PessoaModel>();

            using (var connection = new SqlConnection(this.ConnectionString))
            {

                var sql = @" SELECT Id, Nome, DataDeAniversario, DiasRestantes 
                             FROM Pessoa
                             WHERE Id = @P1
                ";

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.AddWithValue("P1", id);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    PessoaModel pessoa = new PessoaModel()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Nome = reader["Nome"].ToString(),
                        DataDeAniversario = DateTime.Parse(reader["DataDeAniversario"].ToString()),
                        DiasRestantes = int.Parse(reader["DiasRestantes"].ToString())
                    };

                    result.Add(pessoa);
                }

                connection.Close();
            }

            return result.FirstOrDefault();
        }
    }
}