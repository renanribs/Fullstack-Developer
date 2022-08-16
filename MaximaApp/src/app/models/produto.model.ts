export class Produto {
  id: string;
  codigo: string;
  nome: string;
  precoUnitario: number;
  quantidade: number;
  imagemUrl: string;
  constructor() {
    this.id = '';
    this.codigo = '';
    this.nome = '';
    this.precoUnitario = 0;
    this.quantidade = 0;
    this.imagemUrl = '';
  }
}
