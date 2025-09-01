using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MGContecnica.Application.DTOs;
using MGContecnica.Domain.Entities;
using MGContecnica.Domain.Enums;
using MGContecnica.Domain.Interfaces.Services;
using MGContecnica.Domain.Models;

namespace MGContecnica.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly ITransacaoService _transacaoService;
    private readonly ILogger<TransacoesController> _logger;

    public TransacoesController(ITransacaoService transacaoService, ILogger<TransacoesController> logger)
    {
        _transacaoService = transacaoService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransacaoDto>>> Get(
        [FromQuery] DateTime? dataInicio,
        [FromQuery] DateTime? dataFim,
        [FromQuery] int? categoriaId,
        [FromQuery] TipoTransacao? tipo)
    {
        try
        {
            _logger.LogInformation("Buscando transações com filtros - DataInicio: {DataInicio}, DataFim: {DataFim}, CategoriaId: {CategoriaId}, Tipo: {Tipo}", 
                dataInicio, dataFim, categoriaId, tipo);

            var transacoes = await _transacaoService.GetWithFiltrosAsync(dataInicio, dataFim, categoriaId, tipo);
            var transacoesDto = transacoes.Select(MapToDto);

            return Ok(transacoesDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar transações com filtros");
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TransacaoDto>> Get(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("ID deve ser maior que zero");
            }

            _logger.LogInformation("Buscando transação por ID: {Id}", id);

            var transacao = await _transacaoService.GetByIdAsync(id);
            if (transacao == null)
            {
                _logger.LogWarning("Transação não encontrada - ID: {Id}", id);
                return NotFound("Transação não encontrada");
            }

            return Ok(MapToDto(transacao));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar transação por ID: {Id}", id);
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    [HttpPost]
    public async Task<ActionResult<TransacaoDto>> Post([FromBody] CreateTransacaoDto createDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Criando nova transação - Descrição: {Descricao}, Valor: {Valor}", 
                createDto.Descricao, createDto.Valor);

            var transacao = new Transacao
            {
                Descricao = createDto.Descricao,
                Valor = createDto.Valor,
                Data = createDto.Data,
                CategoriaId = createDto.CategoriaId,
                Observacoes = createDto.Observacoes
            };

            var transacaoCriada = await _transacaoService.CreateAsync(transacao);
            var transacaoDto = MapToDto(transacaoCriada);

            _logger.LogInformation("Transação criada com sucesso - ID: {Id}", transacaoCriada.Id);

            return CreatedAtAction(nameof(Get), new { id = transacaoDto.Id }, transacaoDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar transação");
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TransacaoDto>> Put(int id, [FromBody] UpdateTransacaoDto updateDto)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("ID deve ser maior que zero");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Atualizando transação - ID: {Id}", id);

            // Verificar se a transação existe
            var transacaoExistente = await _transacaoService.GetByIdAsync(id);
            if (transacaoExistente == null)
            {
                _logger.LogWarning("Tentativa de atualizar transação inexistente - ID: {Id}", id);
                return NotFound("Transação não encontrada");
            }

            var transacao = new Transacao
            {
                Id = id,
                Descricao = updateDto.Descricao,
                Valor = updateDto.Valor,
                Data = updateDto.Data,
                CategoriaId = updateDto.CategoriaId,
                Observacoes = updateDto.Observacoes
            };

            var transacaoAtualizada = await _transacaoService.UpdateAsync(transacao);
            var transacaoDto = MapToDto(transacaoAtualizada);

            _logger.LogInformation("Transação atualizada com sucesso - ID: {Id}", id);

            return Ok(transacaoDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar transação - ID: {Id}", id);
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("ID deve ser maior que zero");
            }

            _logger.LogInformation("Excluindo transação - ID: {Id}", id);

            // Verificar se a transação existe
            var transacaoExistente = await _transacaoService.GetByIdAsync(id);
            if (transacaoExistente == null)
            {
                _logger.LogWarning("Tentativa de excluir transação inexistente - ID: {Id}", id);
                return NotFound("Transação não encontrada");
            }

            await _transacaoService.DeleteAsync(id);
            
            _logger.LogInformation("Transação excluída com sucesso - ID: {Id}", id);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir transação - ID: {Id}", id);
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    [HttpGet("paged")]
    public async Task<ActionResult<PagedResult<TransacaoDto>>> GetPaged(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] DateTime? dataInicio = null,
        [FromQuery] DateTime? dataFim = null,
        [FromQuery] int? categoriaId = null,
        [FromQuery] TipoTransacao? tipo = null)
    {
        try
        {
            // Validação dos parâmetros de paginação
            if (pageNumber < 1)
            {
                return BadRequest("Número da página deve ser maior que zero");
            }

            if (pageSize < 1 || pageSize > 100)
            {
                return BadRequest("Tamanho da página deve estar entre 1 e 100");
            }

            _logger.LogInformation("Buscando transações paginadas - Página: {PageNumber}, Tamanho: {PageSize}, DataInicio: {DataInicio}, DataFim: {DataFim}, CategoriaId: {CategoriaId}, Tipo: {Tipo}", 
                pageNumber, pageSize, dataInicio, dataFim, categoriaId, tipo);

            var result = await _transacaoService.GetPagedAsync(pageNumber, pageSize, dataInicio, dataFim, categoriaId, tipo);
            
            var pagedDto = new PagedResult<TransacaoDto>
            {
                Items = result.Items.Select(MapToDto),
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };

            _logger.LogInformation("Retornadas {Count} transações de {Total} - Página {Page} de {TotalPages}", 
                pagedDto.Items.Count(), pagedDto.TotalCount, pagedDto.PageNumber, pagedDto.TotalPages);

            return Ok(pagedDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar transações paginadas - Página: {PageNumber}, Tamanho: {PageSize}", pageNumber, pageSize);
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    // Método privado para centralizar o mapeamento
    private static TransacaoDto MapToDto(Transacao transacao)
    {
        return new TransacaoDto
        {
            Id = transacao.Id,
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Data = transacao.Data,
            CategoriaId = transacao.CategoriaId,
            Observacoes = transacao.Observacoes,
            NomeCategoria = transacao.Categoria?.Nome ?? "",
            TipoCategoria = transacao.Categoria?.Tipo ?? TipoTransacao.Despesa
        };
    }
}