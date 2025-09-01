using MGContecnica.Domain.Entities;
using MGContecnica.Domain.Enums;

namespace MGContecnica.Domain.Interfaces.Services;

public interface ICategoriaService
{
    Task<IEnumerable<Categoria>> GetAllAsync();
    Task<Categoria?> GetByIdAsync(int id);
    Task<Categoria> CreateAsync(Categoria categoria);
    Task<Categoria> UpdateAsync(Categoria categoria);
    Task DeleteAsync(int id);
    Task<IEnumerable<Categoria>> GetAtivasAsync();
    Task<IEnumerable<Categoria>> GetByTipoAsync(TipoTransacao tipo);
}