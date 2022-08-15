using AppApi.Context;
using Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<Context>(
    option => option.UseNpgsql("HOST=localhost;Port=5432;Pooling=true;Database=testemaxima;User Id=postgres;Password=postgres"));

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.MapPost("AdicionarPedido", async (Pedido pedido, Context context) =>
{
    context.Pedido.Add(pedido);
    await context.SaveChangesAsync();
}
    );

app.MapPost("ExcluirPedido/{id}", async (int id, Context context) =>
{
    var deletarPedido = await context.Pedido.FirstOrDefaultAsync(p => p.PedidoId == id);
    if(deletarPedido != null)
    {
        context.Pedido.Remove(deletarPedido);
        await context.SaveChangesAsync();
    }
    
});

app.MapGet("ListarPedidos", async (Context context) =>
{
    return await context.Pedido.ToListAsync();

});

app.MapGet("ObterPedido/{id}", async (int id, Context contexto) =>
{
    return await contexto.Pedido.FirstOrDefaultAsync(p => p.PedidoId == id);
});

app.MapGet("ListarClientes", async (Context context) =>
{
    return await context.Cliente.ToListAsync();

});

app.MapGet("ListarProdutos", async (Context context) =>
{
    return await context.Produto.ToListAsync();

});



app.UseSwaggerUI();
app.Run();
