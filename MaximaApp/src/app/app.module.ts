import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { PedidosComponent } from './pedidos/pedidos.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { CriarPedidoComponent } from './criar-pedido/criar-pedido.component';
import { HttpClientModule } from '@angular/common/http';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { AppComponent } from './app.component';
import { BackendApi } from './backend-api';
import { FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { FreteApi } from './frete-api';
import { NgSelectModule } from '@ng-select/ng-select';

@NgModule({
  declarations: [
    AppComponent,
    PedidosComponent,
    NavBarComponent,
    CriarPedidoComponent,
  ],
  imports: [
    BrowserModule,
    MatTableModule,
    BsDropdownModule.forRoot(),
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NgSelectModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'pedidos', component: PedidosComponent },
      { path: 'novo-pedido', component: CriarPedidoComponent },
    ]),
    HttpClientModule,
    MatToolbarModule,
    MatIconModule,
    MatSidenavModule,
    MatListModule,
    MatButtonModule,
    MatMenuModule,
    MatTableModule,
    ToastrModule.forRoot(),
  ],
  providers: [BackendApi, FreteApi],
  bootstrap: [AppComponent],
})
export class AppModule {}
