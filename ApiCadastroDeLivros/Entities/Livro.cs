using System;

namespace ApiCadastroDeLivros.Entities
{
    public class Livro
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataLancamento { get; set; }
        public Autor AutorLivro { get; set; }       
    }
}
