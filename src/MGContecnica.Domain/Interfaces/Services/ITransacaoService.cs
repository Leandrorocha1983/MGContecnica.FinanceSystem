using MGContecnica.Domain.Models;
using MGContecnica.Domain.Entities;
using MGContecnica.Domain.Enums;

namespace MGContecnica.Domain.Interfaces.Services;

public interface ITransacaoService
{
    Task<IEnumerable<Transacao>> GetAllAsync();
    Task<Transacao?> GetByIdAsync(int id);
    Task<Transacao> CreateAsync(Transacao transacao);
    Task<Transacao> UpdateAsync(Transacao transacao);
    Task DeleteAsync(int id);
    Task<IEnumerable<Transacao>> GetWithFiltrosAsync(DateTime? dataInicio, DateTime? dataFim, int? categoriaId, TipoTransacao? tipo);
    Task<PagedResult<Transacao>> GetPagedAsync(int pageNumber, int pageSize, DateTime? dataInicio, DateTime? dataFim, int? categoriaId, TipoTransacao? tipo);
}