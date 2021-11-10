using ApiCadastroDeLivros.InputModel;
using ApiCadastroDeLivros.ViewModels;
using System.Threading.Tasks;

namespace ApiCadastroDeLivros.Services
{
    public interface ILivroService : IService<LivroViewModel>
    {
        Task<LivroViewModel> Inserir(LivroInputModel livroInputModel);
        Task Atualizar(int id, LivroInputModel livroInputModel);
    }
}
