export class Produto {
  id: string;
  codigo: string;
  nome: string;
  precoUnitario: number;
  qntItem: number;
  imagemUrl: string;
  constructor() {
    this.id = '';
    this.codigo = '';
    this.nome = '';
    this.precoUnitario = 0;
    this.qntItem = 0;
    this.imagemUrl = '';
  }
}
