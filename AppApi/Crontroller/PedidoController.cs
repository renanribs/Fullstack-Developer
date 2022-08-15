using AppApi.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Crontroller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private IPedidoRepository _pedidoRepository;

        public PedidoController(IPedidoRepository pedidoRepository)
        {
            this._pedidoRepository = pedidoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                return Ok(await _pedidoRepository.Listar());
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] Pedido Pedido)
        {
            try
            {
                await _pedidoRepository.Incluir(Pedido);
                return Created(Pedido.Id, Pedido);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha");
            }
        }

        [HttpPut("{Id}")]
        public IActionResult Editar(string Id, [FromBody] Pedido Pedido)
        {
            try
            {
                Pedido.Id = Id;
                if (_pedidoRepository.Obter(Pedido.Id) == null)
                {
                    return NotFound();
                }
                _pedidoRepository.Editar(Pedido);
                return Created(Pedido.Id, Pedido);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha");
            }

        }

        [HttpDelete("{Id}")]
        public IActionResult Excluir(string Id, [FromBody] Pedido Pedido)
        {
            try
            {
                Pedido.Id = Id;
                if (_pedidoRepository.Obter(Id) == null)
                {
                    return NotFound();
                }
                _pedidoRepository.Excluir(Pedido);
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha");
            }
        }

    }
}
