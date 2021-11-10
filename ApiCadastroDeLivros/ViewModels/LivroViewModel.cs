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
        
       
        public string Nome { get; set; }

        
        public DateTime DataLancamento { get; set; }
        
       
        public AutorViewModel AutorLivroViewModel { get; set; }        
    }
}
