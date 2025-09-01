namespace MGContecnica.Domain.Interfaces.Services;

public class ResumoFinanceiro
{
    public decimal SaldoTotal { get; set; }
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
}

public class RelatorioCategoria
{
    public string NomeCategoria { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public int QuantidadeTransacoes { get; set; }
}

public interface IRelatorioService
{
    Task<ResumoFinanceiro> GetResumoFinanceiroAsync(DateTime dataInicio, DateTime dataFim);
    Task<IEnumerable<RelatorioCategoria>> GetRelatorioPorCategoriaAsync(DateTime dataInicio, DateTime dataFim);
}