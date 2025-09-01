namespace MGContecnica.Domain.Entities;

public class Transacao : BaseEntity
{
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public int CategoriaId { get; set; }
    public string? Observacoes { get; set; }
    
    public virtual Categoria Categoria { get; set; } = null!;
}