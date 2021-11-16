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
    public class LivroService : ILivroService
    {
        private readonly IRepository<Livro> _livroRepository;
        public LivroService(IRepository<Livro> livroRepository)
        {
            _livroRepository = livroRepository;
        }  
        public async Task<LivroViewModel> Inserir(LivroInputModel livro )
        {
            //Random rd = new Random();
            var listaLivro = await _livroRepository.Obter(livro.Nome);
            if(listaLivro.Count > 0)
            {
                throw new Exception();
            }

            var livroInsert = new Livro
            {
                //Id = rd.Next(),
                Nome = livro.Nome,
                DataLancamento = livro.DataLancamento,
                AutorLivro = new Autor { Id = livro.IdAutor}
            };
            await _livroRepository.Inserir(livroInsert);

            return new LivroViewModel
            {
                //Id = livroInsert.Id,
                Nome = livroInsert.Nome,
                DataLancamento = livroInsert.DataLancamento,
                AutorLivroViewModel = new AutorViewModel { Id = livroInsert.AutorLivro.Id, Nome = livroInsert.AutorLivro.Nome }
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
                AutorLivroViewModel = new AutorViewModel()
                {Id = livro.AutorLivro.Id, Nome = livro.AutorLivro.Nome }
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
                AutorLivroViewModel = new ViewModels.AutorViewModel { Id = livro.AutorLivro.Id}
            };
        }
        public async Task Atualizar(int id, LivroInputModel livro)
        {
            var entidadeLivro = await _livroRepository.Obter(id);
            if(entidadeLivro == null)
            {
                throw new NotImplementedException();
            }
            entidadeLivro.Nome = livro.Nome;
            entidadeLivro.DataLancamento = livro.DataLancamento;
            entidadeLivro.AutorLivro = new Autor { Id = livro.IdAutor};
            await _livroRepository.Atualizar(entidadeLivro);
        }
       
        public async Task Remover(int id)
        {
            var livro = await _livroRepository.Obter(id);
            if(livro == null)
            {
                throw new NotImplementedException();
            }
            await _livroRepository.Remover(id);
        }
        public void Dispose()
        {
            _livroRepository?.Dispose();
        }
       
    }
}
