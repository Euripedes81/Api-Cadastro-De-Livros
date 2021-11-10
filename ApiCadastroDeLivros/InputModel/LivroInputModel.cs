using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastroDeLivros.InputModel
{
    public class LivroInputModel
    {
        [Required(ErrorMessage = "O Campo Nome é Obrigatório!")]
        public string Nome { get; set; }       
        public DateTime DataLancamento { get; set; }

        public int IdAutor { get; set; }
        //[Required(ErrorMessage = "O Campo Autor é Obrigatório!")] 
        //public AutorInputModel AutorLivro { get; set; }
    }
}
