import { Cliente } from './cliente.model';
import { Produto } from './produto.model';

export class Pedido {
  id: string;
  cliente: Cliente;
  produtos: Produto[];
  data: Date;
  frete: number;
  constructor() {
    this.id = '';
    this.cliente = new Cliente();
    this.frete = 0;
    this.produtos = [];
    this.data = new Date();
  }
}
