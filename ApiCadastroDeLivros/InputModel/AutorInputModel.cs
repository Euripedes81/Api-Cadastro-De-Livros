using System.ComponentModel.DataAnnotations;

namespace ApiCadastroDeLivros.InputModel
{
    public class AutorInputModel
    {      
       
        [Required(ErrorMessage = "O Campo Nome é Obrigatório!")]
        public string Nome { get; set; }
    }
}
