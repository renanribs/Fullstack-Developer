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
                var sql = "INSERT INTO Produto (ID, CODIGO, NOME, PRECOUNITARIO, IMAGEMURL) VALUES (@Id, @Codigo, @Nome, @PrecoUnitario, @ImagemUrl)";
                using var conn = await GetDbConnection();
                await conn.ExecuteAsync(sql, Produto);
            }

            public async Task Editar(Produto Produto)
            {
                var sql = "UPDATE Produto SET Codigo = @Codigo, NOME = @Nome, PRECOUNITARIO = @PrecoUnitario, IMAGEMURL = @ImagemUrl WHERE ID = @Id";
                using var conn = await GetDbConnection();
                await conn.ExecuteAsync(sql, Produto);
            }

            public async Task Excluir(Produto Produto)
            {
                using var conn = await GetDbConnection();
                await conn.ExecuteAsync("DELETE FROM Produto WHERE ID = @Id", new { Produto.Id });
            }

            public async Task<List<Produto>> Listar()
            {
                var sql = "SELECT * FROM Produto";
                using var conn = await GetDbConnection();
                return (await conn.QueryAsync<Produto>(sql)).ToList();
            }

            public async Task<Produto?> Obter(string Id)
            {
                var sql = "SELECT * FROM Produto WHERE ID = @Id";
                using var conn = await GetDbConnection();
                return await conn.QueryFirstAsync<Produto>(sql, new { Id });
            }

            public async Task<List<Produto>> ObterPorPedido(string PedidoId)
            {
                var sql = "SELECT Produto.*, Quantidade FROM PedidoProduto INNER JOIN Produto ON PedidoProduto.ProdutoId = Produto.Id WHERE PedidoProduto.PEDIDOID = @PedidoId";
                using var conn = await GetDbConnection();
                return (await conn.QueryAsync<Produto>(sql, new { PedidoId })).ToList();
            }

            public async Task IserirProdutoNoPedido(string ProdutoId, string PedidoId, double Quantidade)
            {
                var sql = "INSERT INTO PedidoProduto (PEDIDOID, PRODUTOID, QUANTIDADE) VALUES (@PedidoId, @ProdutoId, @Quantidade)";
                using var conn = await GetDbConnection();
                await conn.ExecuteAsync(sql, new { ProdutoId, PedidoId, Quantidade });
            }

            public async Task ExcluirProdutoDoPedido(string PedidoId)
            {
                using var conn = await GetDbConnection();
                await conn.ExecuteAsync("DELETE FROM PedidoProduto WHERE PEDIDOID = @PedidoId", new { PedidoId });
            }
        
    }
}
