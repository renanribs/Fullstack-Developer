using Entities;
using AppApi.Services;
namespace AppApi.Interfaces
{
    public interface IClienteService
    {
        public List<Cliente> ListarClientes();
    }
}
