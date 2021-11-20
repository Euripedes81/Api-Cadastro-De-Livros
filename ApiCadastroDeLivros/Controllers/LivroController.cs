using ApiCadastroDeLivros.InputModel;
using ApiCadastroDeLivros.Services;
using ApiCadastroDeLivros.ViewModels;
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
        private readonly ILivroService _livroService;

        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        /// <summary>
        /// Buscar todos os Livros de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os Livros sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de registros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de Livros</response>
        /// <response code="204">Caso não haja Livros</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivroViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var livros = await _livroService.Obter(pagina, quantidade);

            if (livros.Count() == 0)
                return NoContent();

            return Ok(livros);
        }

        /// <summary>
        /// Buscar um Livro pelo seu Id
        /// </summary>
        /// <param name="idLivro">Id do Livro buscado</param>
        /// <response code="200">Retorna o Livro filtrado</response>
        /// <response code="204">Caso não haja Livro com este id</response>   
        [HttpGet("{idLivro}")]
        public async Task<ActionResult<LivroViewModel>> Obter([FromRoute] int idLivro)
        {
            var livro = await _livroService.Obter(idLivro);
            if(livro == null)
            {
                return null;
            }
            return Ok(livro);
        }

        /// <summary>
        /// Inserir um Livro no catálogo
        /// </summary>
        /// <param name="livroInputModel">Dados do Livro a ser inserido</param>
        /// <response code="200">Cao o Livro seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um Livro com mesmo nome para a mesma produtora</response>   
        [HttpPost]
        public async Task<ActionResult<LivroViewModel>> InserirLivro([FromBody] LivroInputModel livroInputModel)
        {
            try
            {
                var livro = await _livroService.Inserir(livroInputModel);
                return base.Ok((object)livro);
            }
            catch (Exception)
            {
                return UnprocessableEntity("Livro existente ou Id do Autor inválida!");
            }
        }

        /// <summary>
        /// Atualizar um Livro no catálogo
        /// </summary>
        /// /// <param name="idLivro">Id do Livro a ser atualizado</param>
        /// <param name="livroInputModel">Novos dados para atualizar o Livro indicado</param>
        /// <response code="200">Cao o Livro seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um Livro com este Id</response>   
        [HttpPut("{idLivro}")]
        public async Task<ActionResult> AtualizarLivro ([FromRoute] int idLivro, [FromBody] LivroInputModel livroInputModel)
        {
            try
            {
               await _livroService.Atualizar(idLivro, livroInputModel);
                return Ok();
            }
            catch (Exception)
            {

                return NotFound("Não existe este Livro");
            }
        }
        /// <summary>
        /// Excluir um Livro
        /// </summary>
        /// /// <param name="idLivro">Id do Livro a ser excluído</param>
        /// <response code="200">Caso o Livro seja excluído com sucesso</response>
        /// <response code="404">Caso não exista um Livro com este Id</response>   
        [HttpDelete("{idLivro}")]
        public async Task<ActionResult> ApagarLivro([FromRoute] int idLivro)
        {
            try
            {
                await _livroService.Remover(idLivro);
                return Ok();
            }
            catch (Exception)
            {

                return NotFound("Não existe este livro.");
            }
        }

    }
}
