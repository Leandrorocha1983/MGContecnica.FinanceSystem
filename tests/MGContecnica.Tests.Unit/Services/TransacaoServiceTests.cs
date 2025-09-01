using FluentAssertions;
using MGContecnica.Application.Services;
using MGContecnica.Domain.Entities;
using MGContecnica.Domain.Interfaces.Repositories;
using Moq;

namespace MGContecnica.Tests.Unit.Services;

public class TransacaoServiceTests
{
    private readonly Mock<ITransacaoRepository> _transacaoRepositoryMock;
    private readonly Mock<ICategoriaRepository> _categoriaRepositoryMock;
    private readonly TransacaoService _transacaoService;

    public TransacaoServiceTests()
    {
        _transacaoRepositoryMock = new Mock<ITransacaoRepository>();
        _categoriaRepositoryMock = new Mock<ICategoriaRepository>();
        _transacaoService = new TransacaoService(_transacaoRepositoryMock.Object, _categoriaRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ComValorZero_DeveRetornarErro()
    {
        // Arrange
        var transacao = new Transacao
        {
            Descricao = "Teste",
            Valor = 0,
            Data = DateTime.Now,
            CategoriaId = 1
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _transacaoService.CreateAsync(transacao));
        exception.Message.Should().Be("Valor deve ser maior que zero");
    }
}