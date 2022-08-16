import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Cliente } from './models/cliente.model';
import { Produto } from './models/produto.model';
import { Pedido } from './models/pedido.model';

const URL = 'https://localhost:7258/api';

@Injectable()
export class BackendApi {
  constructor(private http: HttpClient) {}

  getClientes() {
    return this.http.get<Cliente[]>(URL + '/Cliente');
  }

  getProdutos() {
    return this.http.get<Produto[]>(URL + '/Produto');
  }

  getPedidos() {
    return this.http.get<Pedido[]>(URL + '/Pedido');
  }

  salvarPedido(pedido: Pedido) {
    let headers = new HttpHeaders();
    headers = headers.set('Content-Type', 'application/json');
    return this.http.post(URL + '/Pedido', JSON.stringify(pedido), {
      headers: headers,
    });
  }
}
