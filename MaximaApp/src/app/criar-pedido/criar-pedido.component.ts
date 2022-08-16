import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Cliente } from '../models/cliente.model';
import { Pedido } from '../models/pedido.model';
import { Produto } from '../models/produto.model';
import { BackendApi } from '../backend-api';
import { FreteApi } from '../frete-api';

@Component({
  selector: 'app-criar-pedido',
  templateUrl: './criar-pedido.component.html',
  styleUrls: ['./criar-pedido.component.css'],
})
export class CriarPedidoComponent implements OnInit {
  constructor(
    private api: BackendApi,
    private freteService: FreteApi,
    private toast: ToastrService,
    private router: Router
  ) {}

  pedido = new Pedido();
  clientes: Cliente[] = [];
  produtosApi: Produto[] = [];
  produtos: Produto[] = [];

  produtoAtual: Produto = new Produto();

  ngOnInit(): void {
    this.carregarProdutos();
    this.carregarClientes();
  }

  adicionarProduto() {
    this.produtoAtual.qntItem = 1;
    this.pedido.produtos.push(this.produtoAtual);
    this.onPedidoChanged();
  }

  carregarProdutos() {
    this.api.getProdutos().subscribe(
      (prod) => {
        this.produtosApi = prod;
        this.onPedidoChanged();
      },
      (_) => this.toast.error('Erro ao buscar produtos')
    );
  }

  carregarClientes() {
    this.api.getClientes().subscribe(
      (cli) => (this.clientes = cli),
      (_) => this.toast.error('Erro ao buscar clientes')
    );
  }

  onPedidoChanged() {
    this.produtos = this.produtosApi.filter(
      (p) => !this.pedido.produtos.find((pf) => pf.id == p.id)
    );
    this.freteService.calcularFrete(this.pedido).subscribe(
      (p) => {
        this.pedido.frete = p.frete;
        this.pedido.id = p.id;
      },
      (erro) => {
        console.log(erro);
        this.toast.error('Erro ao utilizar o serviço de cálculo de frete');
      }
    );
  }

  onProdutoQuantidadeChanged(ev: any, produto: Produto) {
    produto.qntItem = parseInt(ev.target.value);
    this.onPedidoChanged();
  }

  onProdutoRemoved(produto: Produto) {
    this.pedido.produtos = this.pedido.produtos.filter(
      (p) => p.id != produto.id
    );
    this.onPedidoChanged();
  }

  getTotalProdutos() {
    if (this.pedido.produtos.length > 0) {
      return this.pedido.produtos
        .map((p) => p.qntItem * p.precoUnitario)
        .reduce((prev, cur) => prev + cur);
    } else {
      return 0;
    }
  }

  getTotalPedido() {
    return this.getTotalProdutos() + this.pedido.frete;
  }

  onFinalizarPedido() {
    this.api.finalizarPedido(this.pedido).subscribe(
      (_) => {
        this.toast.success('Pedido salvo com sucesso');
        this.router.navigateByUrl('/pedidos');
      },
      (_) => this.toast.error('Erro ao salvar o pedido no servidor')
    );
  }

  onLimparCarrinho() {
    this.pedido.produtos = [];
    this.onPedidoChanged();
  }
}
