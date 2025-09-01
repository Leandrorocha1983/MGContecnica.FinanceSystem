using Microsoft.EntityFrameworkCore;
using MGContecnica.Domain.Entities;

namespace MGContecnica.Infrastructure.Data.Context;

public class MGContecnicaDbContext : DbContext
{
    public MGContecnicaDbContext(DbContextOptions<MGContecnicaDbContext> options) : base(options)
    {
    }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração Categoria
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Tipo).IsRequired();
            entity.Property(e => e.Ativo).IsRequired();
            entity.Property(e => e.DataCriacao).IsRequired();
        });

        // Configuração Transacao
        modelBuilder.Entity<Transacao>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Valor).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.Data).IsRequired();
            entity.Property(e => e.DataCriacao).IsRequired();
            entity.Property(e => e.Observacoes).HasMaxLength(1000);

            entity.HasOne(e => e.Categoria)
                  .WithMany(c => c.Transacoes)
                  .HasForeignKey(e => e.CategoriaId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}