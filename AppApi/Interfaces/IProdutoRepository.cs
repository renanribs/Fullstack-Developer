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
        public Task IserirProdutoNoPedido(string ProdutoId, string PedidoId, double Quantidade);
        public Task ExcluirProdutoDoPedido(string PedidoId);
    }
}
