import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransacaoService } from '../../services/transacao.service';
import { CategoriaService } from '../../services/categoria.service';
import { Transacao } from '../../models/transacao.model';
import { Categoria } from '../../models/categoria.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
  transacoes: Transacao[] = [];
  categorias: Categoria[] = [];
  loading = false;
  error: string | null = null;

  constructor(
    private transacaoService: TransacaoService,
    private categoriaService: CategoriaService
  ) {}

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.loading = true;
    this.error = null;

    // Carregar categorias
    this.categoriaService.getCategorias().subscribe({
      next: (categorias) => {
        this.categorias = categorias;
        console.log('Categorias carregadas:', categorias);
      },
      error: (err) => {
        this.error = 'Erro ao carregar categorias: ' + err.message;
        console.error('Erro categorias:', err);
      }
    });

    // Carregar transações
    this.transacaoService.getTransacoes().subscribe({
      next: (transacoes) => {
        this.transacoes = transacoes;
        this.loading = false;
        console.log('Transações carregadas:', transacoes);
      },
      error: (err) => {
        this.error = 'Erro ao carregar transações: ' + err.message;
        this.loading = false;
        console.error('Erro transações:', err);
      }
    });
  }

  getTotalReceitas(): number {
    return this.transacoes
      .filter(t => t.tipoCategoria === 1)
      .reduce((sum, t) => sum + t.valor, 0);
  }

  getTotalDespesas(): number {
    return this.transacoes
      .filter(t => t.tipoCategoria === 2)
      .reduce((sum, t) => sum + t.valor, 0);
  }

  getSaldo(): number {
    return this.getTotalReceitas() - this.getTotalDespesas();
  }
}