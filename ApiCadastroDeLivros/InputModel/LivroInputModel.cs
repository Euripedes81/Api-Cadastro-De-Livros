using System;
using System.ComponentModel.DataAnnotations;

namespace ApiCadastroDeLivros.InputModel
{
    public class LivroInputModel
    {
        [Required(ErrorMessage = "O Campo Nome é Obrigatório!")]
        public string Nome { get; set; }       
        public DateTime DataLancamento { get; set; }

        public int IdAutor { get; set; }
        
    }
}
