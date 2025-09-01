using MGContecnica.Domain.Models;
using MGContecnica.Domain.Entities;
using MGContecnica.Domain.Enums;
using MGContecnica.Domain.Interfaces.Repositories;
using MGContecnica.Domain.Interfaces.Services;

namespace MGContecnica.Application.Services;

public class TransacaoService : ITransacaoService
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly ICategoriaRepository _categoriaRepository;

    public TransacaoService(ITransacaoRepository transacaoRepository, ICategoriaRepository categoriaRepository)
    {
        _transacaoRepository = transacaoRepository;
        _categoriaRepository = categoriaRepository;
    }

    public async Task<IEnumerable<Transacao>> GetAllAsync()
    {
        return await _transacaoRepository.GetAllAsync();
    }

    public async Task<Transacao?> GetByIdAsync(int id)
    {
        return await _transacaoRepository.GetByIdAsync(id);
    }

    public async Task<Transacao> CreateAsync(Transacao transacao)
    {
        await ValidateTransacaoAsync(transacao);
        
        transacao.DataCriacao = DateTime.Now;
        return await _transacaoRepository.AddAsync(transacao);
    }

    public async Task<Transacao> UpdateAsync(Transacao transacao)
    {
        var transacaoExistente = await _transacaoRepository.GetByIdAsync(transacao.Id);
        if (transacaoExistente == null)
            throw new ArgumentException("Transação não encontrada");

        await ValidateTransacaoAsync(transacao);

        transacaoExistente.Descricao = transacao.Descricao;
        transacaoExistente.Valor = transacao.Valor;
        transacaoExistente.Data = transacao.Data;
        transacaoExistente.CategoriaId = transacao.CategoriaId;
        transacaoExistente.Observacoes = transacao.Observacoes;

        return await _transacaoRepository.UpdateAsync(transacaoExistente);
    }

    public async Task DeleteAsync(int id)
    {
        var transacao = await _transacaoRepository.GetByIdAsync(id);
        if (transacao == null)
            throw new ArgumentException("Transação não encontrada");

        await _transacaoRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Transacao>> GetWithFiltrosAsync(DateTime? dataInicio, DateTime? dataFim, int? categoriaId, TipoTransacao? tipo)
    {
        return await _transacaoRepository.GetWithFiltrosAsync(dataInicio, dataFim, categoriaId, tipo);
    }

    public async Task<PagedResult<Transacao>> GetPagedAsync(int pageNumber, int pageSize, DateTime? dataInicio, DateTime? dataFim, int? categoriaId, TipoTransacao? tipo)
    {
        return await _transacaoRepository.GetPagedAsync(pageNumber, pageSize, dataInicio, dataFim, categoriaId, tipo);
    }

    private async Task ValidateTransacaoAsync(Transacao transacao)
    {
        // Validação: Descrição obrigatória
        if (string.IsNullOrWhiteSpace(transacao.Descricao))
            throw new ArgumentException("Descrição é obrigatória");

        // Validação: Valor deve ser maior que zero
        if (transacao.Valor <= 0)
            throw new ArgumentException("Valor deve ser maior que zero");

        // Validação: Data não pode ser futura
        if (transacao.Data > DateTime.Now)
            throw new ArgumentException("Data não pode ser futura");

        // Validação: Categoria deve existir e estar ativa
        var categoria = await _categoriaRepository.GetByIdAsync(transacao.CategoriaId);
        if (categoria == null)
            throw new ArgumentException("Categoria não encontrada");
        
        if (!categoria.Ativo)
            throw new ArgumentException("Categoria deve estar ativa");
    }
}