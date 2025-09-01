using Microsoft.AspNetCore.Mvc;
using MGContecnica.Application.DTOs;
using MGContecnica.Domain.Entities;
using MGContecnica.Domain.Interfaces.Services;

namespace MGContecnica.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDto>>> Get()
    {
        try
        {
            var categorias = await _categoriaService.GetAtivasAsync();
            
            var categoriasDto = categorias.Select(c => new CategoriaDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Tipo = c.Tipo,
                Ativo = c.Ativo
            });

            return Ok(categoriasDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaDto>> Post([FromBody] CreateCategoriaDto createDto)
    {
        try
        {
            var categoria = new Categoria
            {
                Nome = createDto.Nome,
                Tipo = createDto.Tipo
            };

            var categoriaCriada = await _categoriaService.CreateAsync(categoria);

            var categoriaDto = new CategoriaDto
            {
                Id = categoriaCriada.Id,
                Nome = categoriaCriada.Nome,
                Tipo = categoriaCriada.Tipo,
                Ativo = categoriaCriada.Ativo
            };

            return CreatedAtAction(nameof(Get), new { id = categoriaDto.Id }, categoriaDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}