using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastroDeLivros.ViewModels
{
    public class LivroViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        public DateTime DataLancamento { get; set; }
        
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public AutorViewModel AutorLivroViewModel { get; set; }        
    }
}
