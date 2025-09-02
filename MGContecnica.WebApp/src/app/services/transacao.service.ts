import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Transacao, CreateTransacao, TipoTransacao } from '../models/transacao.model';

@Injectable({
  providedIn: 'root'
})
export class TransacaoService {
  private apiUrl = '/api/transacoes';

  constructor(private http: HttpClient) { }

  getTransacoes(
    dataInicio?: string,
    dataFim?: string,
    categoriaId?: number,
    tipo?: TipoTransacao
  ): Observable<Transacao[]> {
    let params: any = {};
    if (dataInicio) params.dataInicio = dataInicio;
    if (dataFim) params.dataFim = dataFim;
    if (categoriaId) params.categoriaId = categoriaId;
    if (tipo) params.tipo = tipo;

    return this.http.get<Transacao[]>(this.apiUrl, { params });
  }

  getTransacao(id: number): Observable<Transacao> {
    return this.http.get<Transacao>(`${this.apiUrl}/${id}`);
  }

  createTransacao(transacao: CreateTransacao): Observable<Transacao> {
    return this.http.post<Transacao>(this.apiUrl, transacao);
  }

  updateTransacao(id: number, transacao: CreateTransacao): Observable<Transacao> {
    return this.http.put<Transacao>(`${this.apiUrl}/${id}`, transacao);
  }

  deleteTransacao(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}