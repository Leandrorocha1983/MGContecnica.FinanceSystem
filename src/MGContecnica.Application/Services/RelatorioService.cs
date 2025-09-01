using MGContecnica.Domain.Interfaces.Repositories;
using MGContecnica.Domain.Interfaces.Services;

namespace MGContecnica.Application.Services;

public class RelatorioService : IRelatorioService
{
    private readonly ITransacaoRepository _transacaoRepository;

    public RelatorioService(ITransacaoRepository transacaoRepository)
    {
        _transacaoRepository = transacaoRepository;
    }

    public async Task<ResumoFinanceiro> GetResumoFinanceiroAsync(DateTime dataInicio, DateTime dataFim)
    {
        var totalReceitas = await _transacaoRepository.GetReceitasPorPeriodoAsync(dataInicio, dataFim);
        var totalDespesas = await _transacaoRepository.GetDespesasPorPeriodoAsync(dataInicio, dataFim);
        var saldoTotal = totalReceitas - totalDespesas;

        return new ResumoFinanceiro
        {
            SaldoTotal = saldoTotal,
            TotalReceitas = totalReceitas,
            TotalDespesas = totalDespesas,
            DataInicio = dataInicio,
            DataFim = dataFim
        };
    }

    public async Task<IEnumerable<RelatorioCategoria>> GetRelatorioPorCategoriaAsync(DateTime dataInicio, DateTime dataFim)
    {
        var transacoes = await _transacaoRepository.GetByPeriodoAsync(dataInicio, dataFim);
        
        var relatorio = transacoes
            .GroupBy(t => new { t.CategoriaId, t.Categoria.Nome })
            .Select(g => new RelatorioCategoria
            {
                NomeCategoria = g.Key.Nome,
                Valor = g.Sum(t => t.Valor),
                QuantidadeTransacoes = g.Count()
            })
            .OrderByDescending(r => r.Valor)
            .ToList();

        return relatorio;
    }
}