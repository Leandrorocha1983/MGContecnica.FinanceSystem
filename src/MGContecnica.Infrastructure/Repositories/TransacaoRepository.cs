using Microsoft.EntityFrameworkCore;
using MGContecnica.Domain.Models;
using MGContecnica.Domain.Entities;
using MGContecnica.Domain.Enums;
using MGContecnica.Domain.Interfaces.Repositories;
using MGContecnica.Infrastructure.Data.Context;

namespace MGContecnica.Infrastructure.Repositories;

public class TransacaoRepository : BaseRepository<Transacao>, ITransacaoRepository
{
    public TransacaoRepository(MGContecnicaDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Transacao>> GetByPeriodoAsync(DateTime dataInicio, DateTime dataFim)
    {
        return await _dbSet
            .Include(t => t.Categoria)
            .Where(t => t.Data >= dataInicio && t.Data <= dataFim)
            .OrderByDescending(t => t.Data)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transacao>> GetByCategoriaAsync(int categoriaId)
    {
        return await _dbSet
            .Include(t => t.Categoria)
            .Where(t => t.CategoriaId == categoriaId)
            .OrderByDescending(t => t.Data)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transacao>> GetByTipoAsync(TipoTransacao tipo)
    {
        return await _dbSet
            .Include(t => t.Categoria)
            .Where(t => t.Categoria.Tipo == tipo)
            .OrderByDescending(t => t.Data)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transacao>> GetWithFiltrosAsync(DateTime? dataInicio, DateTime? dataFim, int? categoriaId, TipoTransacao? tipo)
    {
        var query = _dbSet.Include(t => t.Categoria).AsQueryable();

        if (dataInicio.HasValue)
            query = query.Where(t => t.Data >= dataInicio.Value);

        if (dataFim.HasValue)
            query = query.Where(t => t.Data <= dataFim.Value);

        if (categoriaId.HasValue)
            query = query.Where(t => t.CategoriaId == categoriaId.Value);

        if (tipo.HasValue)
            query = query.Where(t => t.Categoria.Tipo == tipo.Value);

        return await query.OrderByDescending(t => t.Data).ToListAsync();
    }

    public async Task<PagedResult<Transacao>> GetPagedAsync(int pageNumber, int pageSize, DateTime? dataInicio, DateTime? dataFim, int? categoriaId, TipoTransacao? tipo)
    {
        var query = _dbSet.Include(t => t.Categoria).AsQueryable();

        if (dataInicio.HasValue)
            query = query.Where(t => t.Data >= dataInicio.Value);
        
        if (dataFim.HasValue)
            query = query.Where(t => t.Data <= dataFim.Value);

        if (categoriaId.HasValue)
            query = query.Where(t => t.CategoriaId == categoriaId.Value);

        if (tipo.HasValue)
            query = query.Where(t => t.Categoria.Tipo == tipo.Value);

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderByDescending(t => t.Data)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Transacao>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<decimal> GetSaldoPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
    {
        var receitas = await GetReceitasPorPeriodoAsync(dataInicio, dataFim);
        var despesas = await GetDespesasPorPeriodoAsync(dataInicio, dataFim);
        return receitas - despesas;
    }

    public async Task<decimal> GetReceitasPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
    {
        return await _dbSet
            .Include(t => t.Categoria)
            .Where(t => t.Data >= dataInicio && t.Data <= dataFim && t.Categoria.Tipo == TipoTransacao.Receita)
            .SumAsync(t => t.Valor);
    }

    public async Task<decimal> GetDespesasPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
    {
        return await _dbSet
            .Include(t => t.Categoria)
            .Where(t => t.Data >= dataInicio && t.Data <= dataFim && t.Categoria.Tipo == TipoTransacao.Despesa)
            .SumAsync(t => t.Valor);
    }
}