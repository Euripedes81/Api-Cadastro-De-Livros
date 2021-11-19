using ApiCadastroDeLivros.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastroDeLivros.Repositories
{
    public class AutorRepository : IRepository<Autor>
    {
        private readonly SqlConnection sqlConnection;
        public AutorRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }
        public async Task<List<Autor>> Obter(int pagina, int quantidade)
        {
            var autores = new List<Autor>();
            var comando = $"select * from Autor order by Autor.Id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            while (sqlDataReader.Read())
            {
                autores.Add(new Autor
                {
                    Id = (int)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],

                });
            }
            await sqlConnection.CloseAsync();
            return autores;
        }
        public async Task<Autor> Obter(int id)
        {
            Autor autor = null;
            var comando = $"select * from Autor where Id = '{id}'";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            while (sqlDataReader.Read())
            {
                autor = new Autor
                {
                    Id = (int)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"]                  

                };
            }
            await sqlConnection.CloseAsync();
            return autor;
        }

        public async Task Atualizar(Autor autor)
        {
            var comando = $"update Autor set Nome = '{autor.Nome}' where Id = '{autor.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

       
        public async Task Inserir(Autor autor)
        {
            var comando = $"insert Autor ( Nome) values ('{autor.Nome}')";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        

        public async Task<List<Autor>> Obter(string nome)
        {
            var autores = new List<Autor>();
            var comando = $"select * from Autor where Nome = '{nome}'";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            while (sqlDataReader.Read())
            {
                autores.Add(new Autor
                {
                    Id = (int)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"]                  

                });
            }
            await sqlConnection.CloseAsync();

            return autores;
        }

       

        public async Task Remover(int id)
        {
            var comando = $"delete from Autor where Id = '{id}'";

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
