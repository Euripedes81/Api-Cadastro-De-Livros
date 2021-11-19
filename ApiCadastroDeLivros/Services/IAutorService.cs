using ApiCadastroDeLivros.InputModel;
using ApiCadastroDeLivros.ViewModels;
using System.Threading.Tasks;

namespace ApiCadastroDeLivros.Services
{
    public interface IAutorService : IService<AutorViewModel>
    {
        Task<AutorViewModel> Inserir(AutorInputModel autorInputModel);
        Task Atualizar(int id, AutorInputModel autorInputModel);
    }
}
