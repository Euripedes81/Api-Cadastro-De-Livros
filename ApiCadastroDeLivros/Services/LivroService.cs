using ApiCadastroDeLivros.Entities;
using ApiCadastroDeLivros.InputModel;
using ApiCadastroDeLivros.Repositories;
using ApiCadastroDeLivros.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ApiCadastroDeLivros.Services
{
    public class LivroService : IService<LivroViewModel>
    {
        private readonly IRepository<Livro> _livroRepository;
        public LivroService(IRepository<Livro> livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public Task Atualizar(int id, LivroViewModel objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(int id, double preco)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<LivroViewModel> Inserir(LivroViewModel livro )
        {
            var listaLivro = await _livroRepository.Obter(livro.Nome, livro.AutorLivroViewModel.Nome);
            if(listaLivro.Count > 0)
            {
                throw new Exception();
            }

            var livroInsert = new Livro
            {
                Id = livro.Id,
                Nome = livro.Nome,
                DataLancamento = livro.DataLancamento,
                AutorLivro = new Autor { Id = livro.AutorLivroViewModel.Id, Nome = livro.AutorLivroViewModel.Nome}
            };
            await _livroRepository.Inserir(livroInsert);
           
            return new LivroViewModel 
            {
                Id = livroInsert.Id,
                Nome = livroInsert.Nome,
                DataLancamento = livroInsert.DataLancamento,
                AutorLivroViewModel = new AutorViewModel { Id = livroInsert.AutorLivro.Id, Nome = livro.AutorLivroViewModel.Nome}
            };
        }

       

        public async Task<List<LivroViewModel>> Obter(int pagina, int quantidade)
        {
            var livros = await _livroRepository.Obter(pagina, quantidade);
            return livros.Select(livro => new LivroViewModel
            {
                Id = livro.Id,
                Nome = livro.Nome,
                DataLancamento = livro.DataLancamento,
                AutorLivroViewModel = new AutorViewModel() { Id = livro.AutorLivro.Id, Nome = livro.AutorLivro.Nome }
            }).ToList();

        }

        public async Task<LivroViewModel> Obter(int id)
        {
            var livro = await _livroRepository.Obter(id);
            if(livro == null)
            {
                return null;
            }
            return new LivroViewModel
            {
                Id = livro.Id,
                Nome = livro.Nome,
                DataLancamento = livro.DataLancamento,
                AutorLivroViewModel = new AutorViewModel { Id = livro.Id, Nome = livro.Nome }
            };
        }

        public Task Remover(int id)
        {
            throw new NotImplementedException();
        }
    }
}
