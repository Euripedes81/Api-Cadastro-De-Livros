using ApiCadastroDeLivros.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastroDeLivros.Repositories
{
    public class LivroRepository : IRepository<Livro>
    {
        private static Dictionary<int, Livro> livros = new Dictionary<int, Livro>()
        {
            { 1, new Livro {Id = 1, Nome = "A Revolução dos Bichos", DataLancamento = new DateTime( 1950, 01, 01) , AutorLivro = new Autor {Id = 1, Nome = "George Orwell" } } },
            { 2, new Livro {Id = 2, Nome = "1984", DataLancamento = new DateTime( 1950, 01, 01) , AutorLivro = new Autor {Id = 1, Nome = "George Orwell" } } },
            { 3, new Livro {Id = 3, Nome = "Ditadura à brasileira", DataLancamento = new DateTime( 1950, 01, 01) , AutorLivro = new Autor {Id = 2, Nome = "Marco Antônio Villa" } } }
        };     
              
        public Task<List<Livro>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(livros.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Livro> Obter(int id)
        {
            if (!livros.ContainsKey(id))
            {
                return Task.FromResult<Livro>(null);
            }
            return Task.FromResult(livros[id]);

        }
        public Task<List<Livro>> Obter(string nomeLivro)
        {
            return Task.FromResult(livros.Values.Where(livro => livro.AutorLivro.Nome.Equals(nomeLivro)).ToList());
        }
        public Task Inserir(Livro livro)
        {
            livros.Add(livro.Id, livro);
            return Task.CompletedTask;
        }
        public Task Atualizar(Livro livro)
        {
            livros[livro.Id] = livro;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task Remover(int id)
        {
            throw new NotImplementedException();
        }
    }

}
