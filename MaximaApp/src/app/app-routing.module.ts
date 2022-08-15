import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CriarPedidoComponent } from './criar-pedido/criar-pedido.component';
import { PedidosComponent } from './pedidos/pedidos.component';

const routes: Routes = [
  { path: 'ConsultaPedidos', component: PedidosComponent },
  { path: 'NovoPedido', component: CriarPedidoComponent },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
