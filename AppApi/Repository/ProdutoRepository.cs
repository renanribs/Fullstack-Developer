using Dapper;
using Entities;
using AppApi.Interfaces;
using AppApi.Services;

namespace AppApi.Repository
{
    public class ProdutoRepository : DapperService, IProdutoRepository
    {

        public ProdutoRepository(IConfiguration config, IProdutoService produtoService) : base(config)
        {
            if (Listar().Result.Count == 0)
            {
                produtoService.ListarProdutos().ForEach(p => Inserir(p).Wait());
            }
        }

        public async Task Inserir(Produto Produto)
        {
            var db = "INSERT INTO Produto (ID, CODIGO, NOME, PRECOUNITARIO, IMAGEMURL) VALUES (@Id, @Codigo, @Nome, @PrecoUnitario, @ImagemUrl)";
            using var conn = await GetDbConnection();
            await conn.ExecuteAsync(db, Produto);
        }

        public async Task Editar(Produto Produto)
        {
            var db = "UPDATE Produto SET Codigo = @Codigo, NOME = @Nome, PRECOUNITARIO = @PrecoUnitario, IMAGEMURL = @ImagemUrl WHERE ID = @Id";
            using var conn = await GetDbConnection();
            await conn.ExecuteAsync(db, Produto);
        }

        public async Task Excluir(Produto Produto)
        {
            using var conn = await GetDbConnection();
            await conn.ExecuteAsync("DELETE FROM Produto WHERE ID = @Id", new { Produto.Id });
        }

        public async Task<List<Produto>> Listar()
        {
            var db = "SELECT * FROM Produto";
            using var conn = await GetDbConnection();
            return (await conn.QueryAsync<Produto>(db)).ToList();
        }

        public async Task<Produto?> Obter(string Id)
        {
            var db = "SELECT * FROM Produto WHERE ID = @Id";
            using var conn = await GetDbConnection();
            return await conn.QueryFirstAsync<Produto>(db, new { Id });
        }

        public async Task<List<Produto>> ObterPorPedido(string PedidoId)
        {
            var db = "SELECT Produto.*, QntItem FROM PedidoProduto INNER JOIN Produto ON PedidoProduto.ProdutoId = Produto.Id WHERE PedidoProduto.PEDIDOID = @PedidoId";
            using var conn = await GetDbConnection();
            return (await conn.QueryAsync<Produto>(db, new { PedidoId })).ToList();
        }

        public async Task InserirProdutoNoPedido(string ProdutoId, string PedidoId, double QntItem)
        {
            var db = "INSERT INTO PedidoProduto (PEDIDOID, PRODUTOID, QNTITEM) VALUES (@PedidoId, @ProdutoId, @QntItem)";
            using var conn = await GetDbConnection();
            await conn.ExecuteAsync(db, new { ProdutoId, PedidoId, QntItem });
        }

        public async Task ExcluirProdutoDoPedido(string PedidoId)
        {
            using var conn = await GetDbConnection();
            await conn.ExecuteAsync("DELETE FROM PedidoProduto WHERE PEDIDOID = @PedidoId", new { PedidoId });
        }

    }
}