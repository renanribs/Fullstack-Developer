import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Pedido } from '../models/pedido.model';
import { BackendApi } from '../backend-api';

@Component({
  selector: 'app-pedidos',
  templateUrl: './pedidos.component.html',
  styleUrls: ['./pedidos.component.css'],
})
export class PedidosComponent implements OnInit {
  pedidos: Pedido[] = [];
  displayedColumns = ['id', 'total', 'num', 'frete'];

  constructor(private api: BackendApi, private toast: ToastrService) {}

  ngOnInit(): void {
    this.api.getPedidos().subscribe(
      (ped) => {
        console.log(ped);
        this.pedidos = ped;
      },
      (_) => {
        this.toast.error('Erro');
      }
    );
  }

  getTotal(pedido: Pedido): number {
    return pedido.produtos
      .map((v) => v.precoUnitario * v.qntItem)
      .reduce((prev, curr) => prev + curr);
  }
}
