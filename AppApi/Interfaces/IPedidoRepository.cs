using Entities;

namespace AppApi.Interfaces
{
    public interface IPedidoRepository
    {
        public Task Incluir(Pedido Pedido);
        public Task Editar(Pedido Pedido);
        public Task Excluir(Pedido Pedido);
        public Task<List<Pedido>> Listar();
        public Task<Pedido?> Obter(string Id);
    }
}
