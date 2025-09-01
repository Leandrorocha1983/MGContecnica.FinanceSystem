using Microsoft.AspNetCore.Mvc;
using MGContecnica.Application.DTOs;
using MGContecnica.Domain.Entities;
using MGContecnica.Domain.Enums;
using MGContecnica.Domain.Interfaces.Services;

namespace MGContecnica.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly ITransacaoService _transacaoService;

    public TransacoesController(ITransacaoService transacaoService)
    {
        _transacaoService = transacaoService;
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
            var transacoes = await _transacaoService.GetWithFiltrosAsync(dataInicio, dataFim, categoriaId, tipo);
            
            var transacoesDto = transacoes.Select(t => new TransacaoDto
            {
                Id = t.Id,
                Descricao = t.Descricao,
                Valor = t.Valor,
                Data = t.Data,
                CategoriaId = t.CategoriaId,
                Observacoes = t.Observacoes,
                NomeCategoria = t.Categoria?.Nome ?? "",
                TipoCategoria = t.Categoria?.Tipo ?? TipoTransacao.Despesa
            });

            return Ok(transacoesDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TransacaoDto>> Get(int id)
    {
        try
        {
            var transacao = await _transacaoService.GetByIdAsync(id);
            if (transacao == null)
                return NotFound("Transação não encontrada");

            var transacaoDto = new TransacaoDto
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

            return Ok(transacaoDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<TransacaoDto>> Post([FromBody] CreateTransacaoDto createDto)
    {
        try
        {
            var transacao = new Transacao
            {
                Descricao = createDto.Descricao,
                Valor = createDto.Valor,
                Data = createDto.Data,
                CategoriaId = createDto.CategoriaId,
                Observacoes = createDto.Observacoes
            };

            var transacaoCriada = await _transacaoService.CreateAsync(transacao);

            var transacaoDto = new TransacaoDto
            {
                Id = transacaoCriada.Id,
                Descricao = transacaoCriada.Descricao,
                Valor = transacaoCriada.Valor,
                Data = transacaoCriada.Data,
                CategoriaId = transacaoCriada.CategoriaId,
                Observacoes = transacaoCriada.Observacoes
            };

            return CreatedAtAction(nameof(Get), new { id = transacaoDto.Id }, transacaoDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TransacaoDto>> Put(int id, [FromBody] UpdateTransacaoDto updateDto)
    {
        try
        {
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

            var transacaoDto = new TransacaoDto
            {
                Id = transacaoAtualizada.Id,
                Descricao = transacaoAtualizada.Descricao,
                Valor = transacaoAtualizada.Valor,
                Data = transacaoAtualizada.Data,
                CategoriaId = transacaoAtualizada.CategoriaId,
                Observacoes = transacaoAtualizada.Observacoes
            };

            return Ok(transacaoDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _transacaoService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}