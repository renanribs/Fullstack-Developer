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
            var db = "INSERT INTO Cliente (ID, CODIGO, NOME) VALUES (@Id, @Codigo, @Nome)";
            using var conn = await GetDbConnection();
            await conn.ExecuteAsync(db, Cliente);
        }

        public async Task Editar(Cliente Cliente)
        {
            var db = "UPDATE Cliente SET CODIGO = @Codigo, NOME = @Nome WHERE ID = @Id";
            using var conn = await GetDbConnection();
            await conn.ExecuteAsync(db, Cliente);
        }

        public async Task Excluir(Cliente Cliente)
        {
            using var conn = await GetDbConnection();
            await conn.ExecuteAsync("DELETE FROM Cliente WHERE ID = @Id", new { Cliente.Id });
        }

        public async Task<List<Cliente>> Listar()
        {
            var db = "SELECT * FROM Cliente";
            using var conn = await GetDbConnection();
            return (await conn.QueryAsync<Cliente>(db)).ToList();
        }

        public async Task<Cliente> Obter(string Id)
        {
            var db = "SELECT * FROM Cliente WHERE ID = @Id";
            using var conn = await GetDbConnection();
            return await conn.QueryFirstAsync<Cliente>(db, new { Id });
        }
    }
}