using Entities;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Context
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) => Database.EnsureCreated();

       public DbSet<Produto> Produto { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
    }
}
