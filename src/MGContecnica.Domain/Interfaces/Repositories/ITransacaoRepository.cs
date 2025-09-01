using MGContecnica.Domain.Models;
using MGContecnica.Domain.Entities;
using MGContecnica.Domain.Enums;

namespace MGContecnica.Domain.Interfaces.Repositories;

public interface ITransacaoRepository : IBaseRepository<Transacao>
{
    Task<IEnumerable<Transacao>> GetByPeriodoAsync(DateTime dataInicio, DateTime dataFim);
    Task<IEnumerable<Transacao>> GetByCategoriaAsync(int categoriaId);
    Task<IEnumerable<Transacao>> GetByTipoAsync(TipoTransacao tipo);
    Task<IEnumerable<Transacao>> GetWithFiltrosAsync(DateTime? dataInicio, DateTime? dataFim, int? categoriaId, TipoTransacao? tipo);
    Task<decimal> GetSaldoPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);
    Task<decimal> GetReceitasPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);
    Task<decimal> GetDespesasPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);
    Task<PagedResult<Transacao>> GetPagedAsync(int pageNumber, int pageSize, DateTime? dataInicio, DateTime? dataFim, int? categoriaId, TipoTransacao? tipo);
}