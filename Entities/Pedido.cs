namespace Entities
{
    public class Pedido
    {
        public string Id { get; set; }
        public Cliente Cliente { get; set; }
        public List<Produto> Produtos { get; set; }
        public double Frete { get; set; }
    }
}

