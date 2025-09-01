using Microsoft.EntityFrameworkCore;
using MGContecnica.Domain.Entities;
using MGContecnica.Domain.Enums;
using MGContecnica.Domain.Interfaces.Repositories;
using MGContecnica.Infrastructure.Data.Context;

namespace MGContecnica.Infrastructure.Repositories;

public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(MGContecnicaDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Categoria>> GetAtivasAsync()
    {
        return await _dbSet.Where(c => c.Ativo).ToListAsync();
    }

    public async Task<IEnumerable<Categoria>> GetByTipoAsync(TipoTransacao tipo)
    {
        return await _dbSet.Where(c => c.Tipo == tipo && c.Ativo).ToListAsync();
    }
}