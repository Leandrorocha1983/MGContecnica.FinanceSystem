using MGContecnica.Domain.Enums;

namespace MGContecnica.Application.DTOs;

public class CategoriaDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public TipoTransacao Tipo { get; set; }
    public bool Ativo { get; set; }
}

public class CreateCategoriaDto
{
    public string Nome { get; set; } = string.Empty;
    public TipoTransacao Tipo { get; set; }
}