using ApiCadastroDeLivros.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastroDeLivros.Repositories
{
    public class LivroRepository : IRepository<Livro>
    {
        private readonly SqlConnection sqlConnection;

        public LivroRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }
        public async Task<List<Livro>> Obter(int pagina, int quantidade)
        {
            var livros = new List<Livro>();
            var comando = $"select * from Livro order by Livro.Id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            while (sqlDataReader.Read())
            {                
                livros.Add(new Livro
                {
                    Id = (int)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    DataLancamento = (DateTime)sqlDataReader["DataLancamento"],
                    AutorLivro = new Autor { Id = (int)sqlDataReader["IdAutor"] }

                }) ;
            }

            await sqlConnection.CloseAsync();

            return livros;
        }
        public async Task<Livro> Obter(int id)
        {
            Livro livro = null;
            var comando = $"select * from Livro where Id = '{id}'";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            while (sqlDataReader.Read())
            {
                livro = new Livro
                {
                    Id = (int)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    DataLancamento = (DateTime)sqlDataReader["DataLancamento"],
                    AutorLivro = new Autor { Id = (int)sqlDataReader["IdAutor"] }

                };          
                              
            }

            await sqlConnection.CloseAsync();

            return livro;
        }


        public async Task<List<Livro>> Obter(string nome)
        {
            var livros = new List<Livro>();
            var comando = $"select * from Livro where Nome = '{nome}'";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            while (sqlDataReader.Read())
            {
                livros.Add(new Livro
                {
                    Id = (int)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    DataLancamento = (DateTime)sqlDataReader["DataLancamento"],
                    AutorLivro = new Autor { Id = (int)sqlDataReader["IdAutor"] }

                });
            }
            await sqlConnection.CloseAsync();

            return livros;
        }
        public async Task Atualizar(Livro livro)
        {
            var comando = $"update livro set Nome = '{livro.Nome}', DataLancamento = '{livro.DataLancamento}', IdAutor = '{livro.AutorLivro.Id}' where Id = '{livro.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }
               
        public async Task Inserir(Livro livro)
        {
            var comando = $"insert Livro ( Nome, DataLancamento, IdAutor) values ('{livro.Nome}', '{livro.DataLancamento}', {livro.AutorLivro.Id})";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }     
               
        public async Task Remover(int id)
        {
            var comando = $"delete from Livro where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }

}
