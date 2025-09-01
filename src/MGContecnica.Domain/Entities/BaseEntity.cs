namespace MGContecnica.Domain.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.Now;
}