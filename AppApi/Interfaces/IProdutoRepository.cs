using Entities;
namespace AppApi.Interfaces
{
    public interface IProdutoRepository
    {
        public Task Inserir(Produto Produto);
        public Task Editar(Produto Produto);
        public Task Excluir(Produto Produto);
        public Task<List<Produto>> Listar();
        public Task<Produto?> Obter(string Id);
        public Task<List<Produto>> ObterPorPedido(string PedidoId);
        public Task InserirProdutoNoPedido(string ProdutoId, string PedidoId, double QntItem);
        public Task ExcluirProdutoDoPedido(string PedidoId);
    }
}
