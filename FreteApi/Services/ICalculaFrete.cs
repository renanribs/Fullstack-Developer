using Entities;
namespace FreteApi.Services
{
    public interface ICalculaFrete
    {
        public Pedido CalcularFrete(Pedido qntitens);
    }
}
