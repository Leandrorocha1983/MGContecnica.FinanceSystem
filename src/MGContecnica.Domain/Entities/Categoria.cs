using MGContecnica.Domain.Enums;

namespace MGContecnica.Domain.Entities;

public class Categoria : BaseEntity
{
    public string Nome { get; set; } = string.Empty;
    public TipoTransacao Tipo { get; set; }
    public bool Ativo { get; set; } = true;
    
    public virtual ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
}