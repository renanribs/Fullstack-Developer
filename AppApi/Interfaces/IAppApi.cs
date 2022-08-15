using Entities;

namespace AppApi.Interfaces
{
    public interface IAppApi
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangesAsync();

        Task<Pedido[]> GetTodosPedidosAsync();

        Task<Cliente[]> GetTodosClientesAsync();

        Task<Produto[]> GetTodosProdutosAsync();

        Task<Produto> GetProdutosAsyncPorID(int id);
    }
}
