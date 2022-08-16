using AppApi.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Crontroller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            this._produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                return Ok(await _produtoRepository.Listar());
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] Produto Produto)
        {
            try
            {
                await _produtoRepository.Inserir(Produto);
                return Created(Produto.Id, Produto);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha");
            }
        }

        [HttpPut("{Id}")]
        public IActionResult Editar(string Id, [FromBody] Produto Produto)
        {
            try
            {
                Produto.Id = Id;
                if (_produtoRepository.Obter(Produto.Id) == null)
                {
                    return NotFound();
                }
                _produtoRepository.Editar(Produto);
                return Created(Produto.Id, Produto);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha");
            }

        }

        [HttpDelete("{Id}")]
        public IActionResult Excluir(string Id, [FromBody] Produto Produto)
        {
            try
            {
                Produto.Id = Id;
                if (_produtoRepository.Obter(Id) == null)
                {
                    return NotFound();
                }
                _produtoRepository.Excluir(Produto);
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha");
            }
        }


    }
}