﻿

namespace Entities
{
    public class Produto
    {
        public string Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public double PrecoUnitario { get; set; }
        public double QtProduto { get; set; }
        public string? ImagemUrl { get; set; }
    }

}