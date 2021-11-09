using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
