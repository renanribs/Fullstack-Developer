using Entities;
using AppApi.Interfaces;

namespace AppApi.Services
{
    public class ProdutoService : BuscarApiMaximaService , IProdutoService
    {
        public List<Produto> ListarProdutos()
        {
            return ListarDaApiMaxima<Produto>("fullstack/produto");
        }
    }
}
