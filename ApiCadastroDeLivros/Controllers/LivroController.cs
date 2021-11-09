using ApiCadastroDeLivros.InputModel;
using ApiCadastroDeLivros.Services;
using ApiCadastroDeLivros.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastroDeLivros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly IService<LivroViewModel>_livroService;

        public LivroController(IService<LivroViewModel> livroService)
        {
            _livroService = livroService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivroViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var livros = await _livroService.Obter(pagina, quantidade);

            if (livros.Count() == 0)
                return NoContent();

            return Ok(livros);
        }
        [HttpGet("{idLivro}")]
        public async Task<ActionResult<LivroViewModel>> Obter(/*[FromRoute]*/ int idLivro)
        {
            var livro = await _livroService.Obter(idLivro);
            if(livro == null)
            {
                return null;
            }
            return Ok(livro);
        }

        [HttpPost]
        public async Task<ActionResult<LivroViewModel>> InserirLivro([FromBody] LivroViewModel livroViewModel)
        {
            try
            {
                var livro = await _livroService.Inserir(livroViewModel);
                return base.Ok((object)livro);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
