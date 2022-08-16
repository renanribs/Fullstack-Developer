import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Pedido } from './models/pedido.model';

const baseUrl = 'https://localhost:7183/api';

@Injectable()
export class FreteApi {
  constructor(private http: HttpClient) {}

  calcularFrete(pedido: Pedido) {
    let headers = new HttpHeaders();
    headers = headers.set('Content-Type', 'application/json');
    return this.http.post<Pedido>(baseUrl + '/Frete', JSON.stringify(pedido), {
      headers: headers,
    });
  }
}
