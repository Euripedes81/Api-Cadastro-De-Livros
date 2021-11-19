using System;
using System.ComponentModel.DataAnnotations;

namespace ApiCadastroDeLivros.InputModel
{
    public class LivroInputModel
    {
        [Required(ErrorMessage = "O Campo Nome é Obrigatório!")]
        public string Nome { get; set; }       
        public DateTime DataLancamento { get; set; }
       
        [Required(ErrorMessage = "O Campo IdAutor é Obrigatório!")]
        public int IdAutor { get; set; }
        
    }
}
