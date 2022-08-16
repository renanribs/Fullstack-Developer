using AppApi.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Crontroller
{
    [Route("api/[controller]")]
    [ApiController]

    public class ClienteController : ControllerBase
    {
        private IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            this._clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                return Ok(await _clienteRepository.Listar());
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] Cliente Cliente)
        {
            try
            {
                await _clienteRepository.Inserir(Cliente);
                return Created(Cliente.Id, Cliente);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha");
            }
        }

        [HttpPut("{Id}")]
        public IActionResult Editar(string Id, [FromBody] Cliente Cliente)
        {
            try
            {
                Cliente.Id = Id;
                if (_clienteRepository.Obter(Cliente.Id) == null)
                {
                    return NotFound();
                }
                _clienteRepository.Editar(Cliente);
                return Created(Cliente.Id, Cliente);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha");
            }

        }

        [HttpDelete("{Id}")]
        public IActionResult Excluir(string Id, [FromBody] Cliente Cliente)
        {
            try
            {
                Cliente.Id = Id;
                if (_clienteRepository.Obter(Id) == null)
                {
                    return NotFound();
                }
                _clienteRepository.Excluir(Cliente);
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha");
            }
        }


    }
}