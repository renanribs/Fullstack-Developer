using Entities;
namespace AppApi.Interfaces
{
    public interface IClienteRepository
    {
        public Task Inserir(Cliente Cliente);
        public Task Editar(Cliente Cliente);
        public Task Excluir(Cliente Cliete);
        public Task<List<Cliente>> Listar();
        public Task<Cliente> Obter(string Id);


    }
}
