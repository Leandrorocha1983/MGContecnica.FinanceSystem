using MGContecnica.Domain.Entities;
using MGContecnica.Domain.Enums;
using MGContecnica.Domain.Interfaces.Repositories;
using MGContecnica.Domain.Interfaces.Services;

namespace MGContecnica.Application.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    public async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        return await _categoriaRepository.GetAllAsync();
    }

    public async Task<Categoria?> GetByIdAsync(int id)
    {
        return await _categoriaRepository.GetByIdAsync(id);
    }

    public async Task<Categoria> CreateAsync(Categoria categoria)
    {
        if (string.IsNullOrWhiteSpace(categoria.Nome))
            throw new ArgumentException("Nome da categoria é obrigatório");

        categoria.Ativo = true;
        categoria.DataCriacao = DateTime.Now;
        
        return await _categoriaRepository.AddAsync(categoria);
    }

    public async Task<Categoria> UpdateAsync(Categoria categoria)
    {
        if (string.IsNullOrWhiteSpace(categoria.Nome))
            throw new ArgumentException("Nome da categoria é obrigatório");

        var categoriaExistente = await _categoriaRepository.GetByIdAsync(categoria.Id);
        if (categoriaExistente == null)
            throw new ArgumentException("Categoria não encontrada");

        categoriaExistente.Nome = categoria.Nome;
        categoriaExistente.Tipo = categoria.Tipo;
        categoriaExistente.Ativo = categoria.Ativo;

        return await _categoriaRepository.UpdateAsync(categoriaExistente);
    }

    public async Task DeleteAsync(int id)
    {
        var categoria = await _categoriaRepository.GetByIdAsync(id);
        if (categoria == null)
            throw new ArgumentException("Categoria não encontrada");

        await _categoriaRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Categoria>> GetAtivasAsync()
    {
        return await _categoriaRepository.GetAtivasAsync();
    }

    public async Task<IEnumerable<Categoria>> GetByTipoAsync(TipoTransacao tipo)
    {
        return await _categoriaRepository.GetByTipoAsync(tipo);
    }
}