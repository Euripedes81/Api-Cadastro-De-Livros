using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ApiCadastroDeLivros.Repositories
{
    public interface IRepository<ENT> : IDisposable
    {
        Task<List<ENT>> Obter(int pagina, int quantidade);
        Task<List<ENT>> Obter(string nome1, string nome2);
        Task<ENT> Obter(int id);
        Task Inserir(ENT objeto);
        Task Atualizar(ENT objeto);       
        Task Remover(int id);

    }
}
