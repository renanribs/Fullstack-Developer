using Microsoft.AspNetCore.Mvc;
using Entities;
using FreteApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreteController : ControllerBase
    {
        private readonly ICalculaFrete calculaFrete;

        public FreteController(ICalculaFrete calculaFrete)
        {
            this.calculaFrete = calculaFrete;
        }

        [HttpPost]
        public IActionResult CalculaValor([FromBody] Pedido Pedido) {
            return Ok(calculaFrete.CalcularFrete(Pedido));
            }
    }
}
