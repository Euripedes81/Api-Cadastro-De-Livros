using ApiCadastroDeLivros.InputModel;
using ApiCadastroDeLivros.Services;
using ApiCadastroDeLivros.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastroDeLivros.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;
        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }
        /// <summary>
        /// Buscar todos os Autores de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os Autores sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de registros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de Autores</response>
        /// <response code="204">Caso não haja Autores</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var autores = await _autorService.Obter(pagina, quantidade);

            if (autores.Count() == 0)
                return NoContent();

            return Ok(autores);
        }

        /// <summary>
        /// Buscar um Autor pelo seu Id
        /// </summary>
        /// <param name="idAutor">Id do Autor buscado</param>
        /// <response code="200">Retorna o Autor filtrado</response>
        /// <response code="204">Caso não haja Autor com este id</response>   
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

        /// <summary>
        /// Inserir um Autor
        /// </summary>
        /// <param name="autorInputModel">Dados do Autor a ser inserido</param>
        /// <response code="200">Caso o Autor seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um Autor com mesmo nome para a mesma produtora</response>   
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

        /// <summary>
        /// Atualizar um Autor
        /// </summary>
        /// /// <param name="idAutor">Id do Autor a ser atualizado</param>
        /// <param name="autorInputModel">Novos dados para atualizar o Autor indicado</param>
        /// <response code="200">Caso o Autor seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um Autor com este Id</response>   
        [HttpPut("{idAutor}")]
        public async Task<ActionResult> AtualizarLivro([FromRoute] int idAutor, [FromBody] AutorInputModel autorInputModel)
        {
            try
            {
                await _autorService.Atualizar(idAutor, autorInputModel);
                return Ok();
            }
            catch (Exception)
            {

                return NotFound("Não existe este Autor!");
            }
        }

        /// <summary>
        /// Excluir um Autor
        /// </summary>
        /// /// <param name="idAutor">Id do Autor a ser excluído</param>
        /// <response code="200">Caso o Autor seja excluído com sucesso</response>
        /// <response code="404">Caso não exista um Autor com este Id</response>   
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
