export interface Transacao {
  id: number;
  descricao: string;
  valor: number;
  data: string;
  categoriaId: number;
  observacoes?: string;
  nomeCategoria?: string;
  tipoCategoria?: TipoTransacao;
}

export interface CreateTransacao {
  descricao: string;
  valor: number;
  data: string;
  categoriaId: number;
  observacoes?: string;
}

export enum TipoTransacao {
  Receita = 1,
  Despesa = 2
}