using MGContecnica.Domain.Entities;
using MGContecnica.Domain.Enums;

namespace MGContecnica.Domain.Interfaces.Repositories;

public interface ICategoriaRepository : IBaseRepository<Categoria>
{
    Task<IEnumerable<Categoria>> GetAtivasAsync();
    Task<IEnumerable<Categoria>> GetByTipoAsync(TipoTransacao tipo);
}