namespace Entities
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public decimal ValorTotal { get; set; }
        public int QtdeItem { get; set; }
        public Cliente Cliente { get; }
        public List<Produto> Produto { get; set; }
        public double Frete { get; set; }
    }
}

