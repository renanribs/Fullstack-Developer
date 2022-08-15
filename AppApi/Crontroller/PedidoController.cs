using AppApi.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Crontroller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IAppApi _repo;
        public PedidoController(IAppApi repo)
        {
            _repo = repo;
        }



        [HttpGet]
        public async Task<IActionResult> GetTask()
        {
            try
            {
                var resultado = await _repo.GetTodosPedidosAsync();
                return Ok(resultado);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha ao conectar");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post(Pedido model)
        {
            try
            {
                _repo.Add(model);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/pedido/{model.PedidoId}", model);
                }
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha ao conectar");
            }

            return BadRequest();
        }
    }
}
