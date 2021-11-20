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
    public class AutorService : IAutorService
    {
        private readonly IRepository<Autor> _autorRepository;
        public AutorService(IRepository<Autor> autorRepository)
        {
            _autorRepository = autorRepository;
        }
        public async Task<AutorViewModel> Inserir(AutorInputModel autor)
        {
            var listaAutor = await _autorRepository.Obter(autor.Nome);
            if (listaAutor.Count > 0)
            {
                throw new Exception("Nome do Autor já existe!");
            }

            var autorInsert = new Autor
            {
                Nome = autor.Nome,               
            };
            await _autorRepository.Inserir(autorInsert);
            return new AutorViewModel
            {
                Id = autorInsert.Id,
                Nome = autorInsert.Nome              
            };
        }
        public async Task<List<AutorViewModel>> Obter(int pagina, int quantidade)
        {
            var autores = await _autorRepository.Obter(pagina, quantidade);
            return autores.Select(autor => new AutorViewModel
            {
                Id = autor.Id,
                Nome = autor.Nome              
                
            }).ToList();
        }

        public async Task<AutorViewModel> Obter(int id)
        {
            var autor = await _autorRepository.Obter(id);
            if (autor == null)
            {
                throw new Exception("Este Autor não existe!");
            }
            return new AutorViewModel
            {
                Id = autor.Id,
                Nome = autor.Nome                
            };
        }


        public async Task Atualizar(int id, AutorInputModel autor)
        {
            var entidadeAutor = await _autorRepository.Obter(id);
            if (entidadeAutor == null)
            {
                throw new NotImplementedException();
            }
            entidadeAutor.Nome = autor.Nome;            
            await _autorRepository.Atualizar(entidadeAutor);
        }

        
       
        public async Task Remover(int id)
        {
            var autor = await _autorRepository.Obter(id);
            if (autor == null)
            {
                throw new NotImplementedException();
            }
            await _autorRepository.Remover(id);
        }

        public void Dispose()
        {
            _autorRepository?.Dispose();
        }

      

    }
}
