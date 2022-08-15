using AppApi.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Crontroller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IAppApi _repo;
        public ProdutoController(IAppApi repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public async Task<IActionResult> GetTask()
        {
            try
            {
                var resultado = await _repo.GetTodosProdutosAsync();
                return Ok(resultado);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao conectar no banco de dados!");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            try
            {
                var resultado = await _repo.GetProdutosAsyncPorID(id);
                return Ok(resultado);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao conectar");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post(Produto model)
        {
            try
            {
                _repo.Add(model);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/produto/{model.Id}", model);
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
