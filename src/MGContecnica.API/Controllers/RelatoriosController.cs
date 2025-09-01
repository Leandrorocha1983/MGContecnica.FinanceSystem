using Microsoft.AspNetCore.Mvc;
using MGContecnica.Domain.Interfaces.Services;

namespace MGContecnica.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RelatoriosController : ControllerBase
{
    private readonly IRelatorioService _relatorioService;

    public RelatoriosController(IRelatorioService relatorioService)
    {
        _relatorioService = relatorioService;
    }

    [HttpGet("resumo")]
    public async Task<ActionResult<ResumoFinanceiro>> GetResumo(
        [FromQuery] DateTime dataInicio,
        [FromQuery] DateTime dataFim)
    {
        try
        {
            var resumo = await _relatorioService.GetResumoFinanceiroAsync(dataInicio, dataFim);
            return Ok(resumo);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("por-categoria")]
    public async Task<ActionResult<IEnumerable<RelatorioCategoria>>> GetPorCategoria(
        [FromQuery] DateTime dataInicio,
        [FromQuery] DateTime dataFim)
    {
        try
        {
            var relatorio = await _relatorioService.GetRelatorioPorCategoriaAsync(dataInicio, dataFim);
            return Ok(relatorio);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}