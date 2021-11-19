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
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;
        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var autores = await _autorService.Obter(pagina, quantidade);

            if (autores.Count() == 0)
                return NoContent();

            return Ok(autores);
        }
        
        [HttpGet("{idAutor}")]
        public async Task<ActionResult<AutorViewModel>> Obter([FromRoute] int idAutor)
        {
            var autor = await _autorService.Obter(idAutor);
            if (autor == null)
            {
                return null;
            }
            return Ok(autor);
        }
        [HttpPost]
        public async Task<ActionResult<AutorViewModel>> InserirAutor([FromBody] AutorInputModel autorInputModel)
        {
            try
            {
                var livro = await _autorService.Inserir(autorInputModel);
                return base.Ok((object)livro);
            }
            catch (Exception)
            {
                return UnprocessableEntity("Livro existente ou Id do Autor inválida!");
            }
        }
        [HttpPut("{idAutor}")]
        public async Task<ActionResult> AtualizarLivro([FromRoute] int idAutor, [FromBody] AutorInputModel autor)
        {
            try
            {
                await _autorService.Atualizar(idAutor, autor);
                return Ok();
            }
            catch (Exception)
            {

                return NotFound("Não existe este Autor!");
            }
        }
        
        [HttpDelete("{idAutor}")]
        public async Task<ActionResult> ApagarAutor([FromRoute] int idAutor)
        {
            try
            {
                await _autorService.Remover(idAutor);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound("Autor não encontrado ou sem permissão para exclusão!");                
                
            }
        }
    }
}
