import { TipoTransacao } from './transacao.model';

export interface Categoria {
  id: number;
  nome: string;
  tipo: TipoTransacao;
  ativo: boolean;
}

export interface CreateCategoria {
  nome: string;
  tipo: TipoTransacao;
}