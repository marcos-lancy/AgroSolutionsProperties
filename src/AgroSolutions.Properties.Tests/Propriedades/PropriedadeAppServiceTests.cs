using AgroSolutions.Properties.Service.Application.AppServices;
using AgroSolutions.Properties.Service.Application.Dtos.Propriedade;
using AgroSolutions.Properties.Service.Application.Dtos.Talhao;
using AgroSolutions.Properties.Service.Domain.Entities;
using AgroSolutions.Properties.Service.Domain.Exceptions;
using AgroSolutions.Properties.Service.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace AgroSolutions.Properties.Tests.Propriedades;

public class PropriedadeAppServiceTests
{
    private readonly Mock<IPropriedadeRepository> _propriedadeRepositoryMock;
    private readonly Mock<ITalhaoRepository> _talhaoRepositoryMock;
    private readonly Mock<ILogger<PropriedadeAppService>> _loggerMock;
    private readonly PropriedadeAppService _service;

    public PropriedadeAppServiceTests()
    {
        _propriedadeRepositoryMock = new Mock<IPropriedadeRepository>();
        _talhaoRepositoryMock = new Mock<ITalhaoRepository>();
        _loggerMock = new Mock<ILogger<PropriedadeAppService>>();
        _service = new PropriedadeAppService(
            _propriedadeRepositoryMock.Object,
            _talhaoRepositoryMock.Object,
            _loggerMock.Object);
    }

    #region CadastrarAsync

    [Fact]
    public async Task CadastrarAsync_ValidDto_ShouldReturnPropriedadeDto()
    {
        // Arrange
        var produtorId = Guid.NewGuid();
        var dto = new CadastrarPropriedadeDto
        {
            Nome = "Fazenda Teste",
            Endereco = "Rua Teste, 100",
            AreaTotal = 500m
        };

        var savedEntity = new PropriedadeEntity
        {
            Id = Guid.NewGuid(),
            Nome = dto.Nome,
            Endereco = dto.Endereco,
            AreaTotal = dto.AreaTotal,
            ProdutorId = produtorId
        };

        _propriedadeRepositoryMock
            .Setup(x => x.AdicionarAsync(It.IsAny<PropriedadeEntity>()))
            .ReturnsAsync(savedEntity);

        // Act
        var result = await _service.CadastrarAsync(produtorId, dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(savedEntity.Id, result.Id);
        Assert.Equal(dto.Nome, result.Nome);
        Assert.Equal(dto.Endereco, result.Endereco);
        Assert.Equal(dto.AreaTotal, result.AreaTotal);
        Assert.Equal(produtorId, result.ProdutorId);
        Assert.NotNull(result.Talhoes);

        _propriedadeRepositoryMock.Verify(
            x => x.AdicionarAsync(It.IsAny<PropriedadeEntity>()),
            Times.Once);
    }

    [Fact]
    public async Task CadastrarAsync_ValidDto_ShouldCallRepositoryWithCorrectData()
    {
        // Arrange
        var produtorId = Guid.NewGuid();
        var dto = new CadastrarPropriedadeDto
        {
            Nome = "Fazenda Nova",
            Endereco = "Estrada Nova",
            AreaTotal = 1000m
        };

        PropriedadeEntity? capturedEntity = null;
        _propriedadeRepositoryMock
            .Setup(x => x.AdicionarAsync(It.IsAny<PropriedadeEntity>()))
            .Callback<PropriedadeEntity>(e => capturedEntity = e)
            .ReturnsAsync((PropriedadeEntity e) => e);

        // Act
        await _service.CadastrarAsync(produtorId, dto);

        // Assert
        Assert.NotNull(capturedEntity);
        Assert.Equal(dto.Nome, capturedEntity.Nome);
        Assert.Equal(dto.Endereco, capturedEntity.Endereco);
        Assert.Equal(dto.AreaTotal, capturedEntity.AreaTotal);
        Assert.Equal(produtorId, capturedEntity.ProdutorId);
    }

    #endregion

    #region ObterPorProdutorIdAsync

    [Fact]
    public async Task ObterPorProdutorIdAsync_WithProperties_ShouldReturnListOfPropriedadeDto()
    {
        // Arrange
        var produtorId = Guid.NewGuid();
        var propriedades = new List<PropriedadeEntity>
        {
            new PropriedadeEntity
            {
                Id = Guid.NewGuid(),
                Nome = "Fazenda 1",
                Endereco = "Endereco 1",
                AreaTotal = 100m,
                ProdutorId = produtorId,
                Talhoes = new List<TalhaoEntity>()
            },
            new PropriedadeEntity
            {
                Id = Guid.NewGuid(),
                Nome = "Fazenda 2",
                Endereco = "Endereco 2",
                AreaTotal = 200m,
                ProdutorId = produtorId,
                Talhoes = new List<TalhaoEntity>()
            }
        };

        _propriedadeRepositoryMock
            .Setup(x => x.ObterPorProdutorIdAsync(produtorId))
            .ReturnsAsync(propriedades);

        // Act
        var result = await _service.ObterPorProdutorIdAsync(produtorId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task ObterPorProdutorIdAsync_EmptyList_ShouldReturnEmptyList()
    {
        // Arrange
        var produtorId = Guid.NewGuid();
        var propriedades = new List<PropriedadeEntity>();

        _propriedadeRepositoryMock
            .Setup(x => x.ObterPorProdutorIdAsync(produtorId))
            .ReturnsAsync(propriedades);

        // Act
        var result = await _service.ObterPorProdutorIdAsync(produtorId);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    #endregion

    #region ObterPorIdAsync

    [Fact]
    public async Task ObterPorIdAsync_ExistingProperty_ShouldReturnPropriedadeDto()
    {
        // Arrange
        var propriedadeId = Guid.NewGuid();
        var propriedade = new PropriedadeEntity
        {
            Id = propriedadeId,
            Nome = "Fazenda Teste",
            Endereco = "Endereco Teste",
            AreaTotal = 500m,
            ProdutorId = Guid.NewGuid(),
            Talhoes = new List<TalhaoEntity>
            {
                new TalhaoEntity
                {
                    Id = Guid.NewGuid(),
                    Nome = "Talhao A",
                    Area = 100m,
                    Cultura = "Soja",
                    PropriedadeId = propriedadeId
                }
            }
        };

        _propriedadeRepositoryMock
            .Setup(x => x.ObterComTalhoesAsync(propriedadeId))
            .ReturnsAsync(propriedade);

        // Act
        var result = await _service.ObterPorIdAsync(propriedadeId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(propriedadeId, result.Id);
        Assert.Equal(propriedade.Nome, result.Nome);
        Assert.Single(result.Talhoes);
    }

    [Fact]
    public async Task ObterPorIdAsync_NonExistingProperty_ShouldThrowNotFoundException()
    {
        // Arrange
        var propriedadeId = Guid.NewGuid();

        _propriedadeRepositoryMock
            .Setup(x => x.ObterComTalhoesAsync(propriedadeId))
            .ReturnsAsync((PropriedadeEntity?)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(
            () => _service.ObterPorIdAsync(propriedadeId));
    }

    #endregion

    #region CadastrarTalhaoAsync

    [Fact]
    public async Task CadastrarTalhaoAsync_ExistingProperty_ShouldReturnTalhaoDto()
    {
        // Arrange
        var propriedadeId = Guid.NewGuid();
        var dto = new CadastrarTalhaoDto
        {
            Nome = "Talhao Norte",
            Area = 150m,
            Cultura = "Milho",
            PropriedadeId = propriedadeId
        };

        var propriedade = new PropriedadeEntity
        {
            Id = propriedadeId,
            Nome = "Fazenda Teste",
            Endereco = "Endereco Teste",
            AreaTotal = 500m,
            ProdutorId = Guid.NewGuid()
        };

        var savedTalhao = new TalhaoEntity
        {
            Id = Guid.NewGuid(),
            Nome = dto.Nome,
            Area = dto.Area,
            Cultura = dto.Cultura,
            PropriedadeId = propriedadeId
        };

        _propriedadeRepositoryMock
            .Setup(x => x.ObterPorIdAsync(propriedadeId))
            .ReturnsAsync(propriedade);

        _talhaoRepositoryMock
            .Setup(x => x.AdicionarAsync(It.IsAny<TalhaoEntity>()))
            .ReturnsAsync(savedTalhao);

        // Act
        var result = await _service.CadastrarTalhaoAsync(propriedadeId, dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dto.Nome, result.Nome);
        Assert.Equal(dto.Area, result.Area);
        Assert.Equal(dto.Cultura, result.Cultura);
        Assert.Equal(propriedadeId, result.PropriedadeId);
    }

    [Fact]
    public async Task CadastrarTalhaoAsync_NonExistingProperty_ShouldThrowNotFoundException()
    {
        // Arrange
        var propriedadeId = Guid.NewGuid();
        var dto = new CadastrarTalhaoDto
        {
            Nome = "Talhao Norte",
            Area = 150m,
            Cultura = "Milho",
            PropriedadeId = propriedadeId
        };

        _propriedadeRepositoryMock
            .Setup(x => x.ObterPorIdAsync(propriedadeId))
            .ReturnsAsync((PropriedadeEntity?)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(
            () => _service.CadastrarTalhaoAsync(propriedadeId, dto));
    }

    #endregion

    #region ObterTalhoesPorPropriedadeIdAsync

    [Fact]
    public async Task ObterTalhoesPorPropriedadeIdAsync_WithTalhoes_ShouldReturnList()
    {
        // Arrange
        var propriedadeId = Guid.NewGuid();
        var talhoes = new List<TalhaoEntity>
        {
            new TalhaoEntity
            {
                Id = Guid.NewGuid(),
                Nome = "Talhao A",
                Area = 100m,
                Cultura = "Soja",
                PropriedadeId = propriedadeId
            },
            new TalhaoEntity
            {
                Id = Guid.NewGuid(),
                Nome = "Talhao B",
                Area = 150m,
                Cultura = "Milho",
                PropriedadeId = propriedadeId
            }
        };

        _talhaoRepositoryMock
            .Setup(x => x.ObterPorPropriedadeIdAsync(propriedadeId))
            .ReturnsAsync(talhoes);

        // Act
        var result = await _service.ObterTalhoesPorPropriedadeIdAsync(propriedadeId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task ObterTalhoesPorPropriedadeIdAsync_EmptyList_ShouldReturnEmptyList()
    {
        // Arrange
        var propriedadeId = Guid.NewGuid();
        var talhoes = new List<TalhaoEntity>();

        _talhaoRepositoryMock
            .Setup(x => x.ObterPorPropriedadeIdAsync(propriedadeId))
            .ReturnsAsync(talhoes);

        // Act
        var result = await _service.ObterTalhoesPorPropriedadeIdAsync(propriedadeId);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    #endregion
}
