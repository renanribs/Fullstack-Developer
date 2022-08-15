using Entities;
using AppApi.Interfaces;

namespace AppApi.Services
{
    public class ClienteService : BuscarApiMaximaService, IClienteService
    {
        public List<Cliente> ListarClientes()
        {
            return ListarDaApiMaxima<Cliente>("fullstack/cliente");
        }
    }
}
