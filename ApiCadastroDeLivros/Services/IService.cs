using ApiCadastroDeLivros.InputModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastroDeLivros.Services
{
    public interface IService<TVM> : IDisposable
    {
        Task<List<TVM>> Obter(int pagina, int quantidade);
        Task<TVM> Obter(int id);    
        Task Remover(int id);
    }
}
