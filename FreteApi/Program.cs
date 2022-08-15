var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();


app.MapPost("/CalculaFrete/{QtdeItem}",  (int QtdeItem) =>
{
    Random random = new Random();
    var valorFrete = random.Next(5, 11);
    var valorTotalFrete = valorFrete  * QtdeItem;
    return valorTotalFrete;
});

app.UseSwaggerUI();
app.Run();