namespace Entities
{
    public class Pedido
    {
        public string Id { get; set; }
        public Cliente Cliente{ get; set; }
        public List<Produto> Produto { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public double Frete { get; set; }
    }
}

