using AppApi.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Crontroller
{
    [Route("api/[controller]")]
    [ApiController]

    public class ClienteController : ControllerBase
    {
        private readonly IAppApi _repo;
        public ClienteController(IAppApi repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var resultado = await _repo.GetTodosClientesAsync();
                return Ok(resultado);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao conectar");
            }


        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post(Cliente model)
        {
            try
            {
                _repo.Add(model);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/cliente/{model.ClienteId}", model);
                }
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao conectar");
            }

            return BadRequest();
        }


    }
}
