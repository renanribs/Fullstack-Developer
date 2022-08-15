using Entities;
using System;

namespace FreteApi.Services
{
    public class CalculaFrete : ICalculaFrete
    {
        public Pedido CalcularFrete(Pedido qntitens)
        {
            var valorFrete = new Random().Next(5, 11);
            qntitens.Frete = valorFrete * qntitens.Produtos.Count;
            return qntitens;
        }

       
    }
}
