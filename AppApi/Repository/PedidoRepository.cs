using Dapper;
using Entities;
using AppApi.Interfaces;
using AppApi.Services;

namespace AppApi.Repository
{
    public class PedidoRepository : DapperService, IPedidoRepository
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IClienteRepository clienteRepository;

        public PedidoRepository(IConfiguration config, IProdutoRepository produtoRepository, IClienteRepository clienteRepository) : base(config)
        {
            this.produtoRepository = produtoRepository;
            this.clienteRepository = clienteRepository;
        }

        public async Task Incluir(Pedido Pedido)
        {
            if (string.IsNullOrEmpty(Pedido.Id))
            {
                Pedido.Id = Guid.NewGuid().ToString();
            }

            using (var conn = await GetDbConnection())
            {
                var sql = "INSERT INTO Pedido (ID, DATA, FRETE, CLIENTEID) VALUES (@Id, @Data, @Frete, @ClienteId)";
                await conn.ExecuteAsync(sql, new { Pedido.Id, Pedido.Frete, ClienteId = Pedido.Cliente.Id });
            }

            foreach (var produto in Pedido.Produtos)
            {
                await produtoRepository.IserirProdutoNoPedido(produto.Id, Pedido.Id, produto.QntItem);
            }
        }

        public async Task Editar(Pedido Pedido)
        {
            await produtoRepository.ExcluirProdutoDoPedido(Pedido.Id);

            using (var conn = await GetDbConnection())
            {
                var sql = "UPDATE Pedido SET DATA = @Data, FRETE = @Frete, CLIENTEID = @ClienteId WHERE ID = @Id";
                await conn.ExecuteAsync(sql, new { Pedido.Id, Pedido.Frete, ClienteId = Pedido.Cliente.Id });
            }

            foreach (var produto in Pedido.Produtos)
            {
                await produtoRepository.IserirProdutoNoPedido(produto.Id, Pedido.Id, produto.QntItem);
            }
        }

        public async Task Excluir(Pedido Pedido)
        {
            using var conn = await GetDbConnection();
            await conn.ExecuteAsync("DELETE FROM Pedido WHERE ID = @Id", new { Pedido.Id });
        }

        public async Task<List<Pedido>> Listar()
        {
            var sql = "SELECT * FROM Pedido";
            using var conn = await GetDbConnection();
            var pedidos = (await conn.QueryAsync<Pedido>(sql)).ToList();

            foreach (var pedido in pedidos)
            {
                string clienteId = await conn.QuerySingleAsync<string>("SELECT CLIENTEID FROM Pedido WHERE ID = @Id", pedido);
                pedido.Cliente = await clienteRepository.Obter(clienteId) ?? new Cliente();
                pedido.Produtos = await produtoRepository.ObterPorPedido(pedido.Id);
            }

            return pedidos;
        }

        public async Task<Pedido?> Obter(string Id)
        {
            var sql = "SELECT * FROM Pedido WHERE ID = @Id";
            using var conn = await GetDbConnection();
            return await conn.QueryFirstAsync<Pedido>(sql, new { Id });
        }
    }
}
