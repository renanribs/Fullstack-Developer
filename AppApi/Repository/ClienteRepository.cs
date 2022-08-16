using Dapper;
using Entities;
using AppApi.Interfaces;
using AppApi.Services;
namespace AppApi.Repository
{
    public class ClienteRepository : DapperService, IClienteRepository
    {
        public ClienteRepository(IConfiguration config, IClienteService provider) : base(config)
        {
            if (Listar().Result.Count == 0)
            {
                provider.ListarClientes().ForEach(c => Inserir(c).Wait());
            }
        }

        public async Task Inserir(Cliente Cliente)
        {
            var sql = "INSERT INTO Cliente (ID, CODIGO, NOME) VALUES (@Id, @Codigo, @Nome)";
            using var conn = await GetDbConnection();
            await conn.ExecuteAsync(sql, Cliente);
        }

        public async Task Editar(Cliente Cliente)
        {
            var sql = "UPDATE Cliente SET CODIGO = @Codigo, NOME = @Nome WHERE ID = @Id";
            using var conn = await GetDbConnection();
            await conn.ExecuteAsync(sql, Cliente);
        }

        public async Task Excluir(Cliente Cliente)
        {
            using var conn = await GetDbConnection();
            await conn.ExecuteAsync("DELETE FROM Cliente WHERE ID = @Id", new { Cliente.Id });
        }

        public async Task<List<Cliente>> Listar()
        {
            var sql = "SELECT * FROM Cliente";
            using var conn = await GetDbConnection();
            return (await conn.QueryAsync<Cliente>(sql)).ToList();
        }

        public async Task<Cliente> Obter(string Id)
        {
            var sql = "SELECT * FROM Cliente WHERE ID = @Id";
            using var conn = await GetDbConnection();
            return await conn.QueryFirstAsync<Cliente>(sql, new { Id });
        }
    }
}