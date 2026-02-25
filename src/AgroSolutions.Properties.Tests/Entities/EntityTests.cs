using AgroSolutions.Properties.Service.Domain.Entities;

namespace AgroSolutions.Properties.Tests.Entities;

public class EntityBaseTests
{
    [Fact]
    public void EntityBase_NewGuid_ShouldGenerateGuid()
    {
        // Act
        var entity = new TestEntity();

        // Assert
        Assert.NotEqual(Guid.Empty, entity.Id);
    }

    [Fact]
    public void EntityBase_SetId_ShouldSetProvidedGuid()
    {
        // Arrange
        var expectedId = Guid.NewGuid();
        var entity = new TestEntity { Id = expectedId };

        // Assert
        Assert.Equal(expectedId, entity.Id);
    }

    private class TestEntity : EntityBase
    {
    }
}

public class PropriedadeEntityTests
{
    [Fact]
    public void PropriedadeEntity_ParameterizedConstructor_ShouldSetAllProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Fazenda Boa Vista";
        var endereco = "Rua dos Campos, 100";
        var areaTotal = 500.50m;
        var produtorId = Guid.NewGuid();

        // Act
        var entity = new PropriedadeEntity(id, nome, endereco, areaTotal, produtorId);

        // Assert
        Assert.Equal(id, entity.Id);
        Assert.Equal(nome, entity.Nome);
        Assert.Equal(endereco, entity.Endereco);
        Assert.Equal(areaTotal, entity.AreaTotal);
        Assert.Equal(produtorId, entity.ProdutorId);
    }

    [Fact]
    public void PropriedadeEntity_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var entity = new PropriedadeEntity();
        var nome = "Fazenda Santo Antonio";
        var endereco = "Estrada Rural, 200";
        var areaTotal = 1000.75m;
        var produtorId = Guid.NewGuid();

        // Act
        entity.Nome = nome;
        entity.Endereco = endereco;
        entity.AreaTotal = areaTotal;
        entity.ProdutorId = produtorId;

        // Assert
        Assert.Equal(nome, entity.Nome);
        Assert.Equal(endereco, entity.Endereco);
        Assert.Equal(areaTotal, entity.AreaTotal);
        Assert.Equal(produtorId, entity.ProdutorId);
    }

    [Fact]
    public void PropriedadeEntity_AddTalhao_ShouldAddToTalhoesList()
    {
        // Arrange
        var entity = new PropriedadeEntity();
        var talhao = new TalhaoEntity
        {
            Nome = "Talhão A",
            Area = 100m,
            Cultura = "Soja",
            PropriedadeId = entity.Id
        };

        // Act
        entity.Talhoes.Add(talhao);

        // Assert
        Assert.Single(entity.Talhoes);
        Assert.Equal("Talhão A", entity.Talhoes[0].Nome);
    }
}

public class TalhaoEntityTests
{
    [Fact]
    public void TalhaoEntity_ParameterizedConstructor_ShouldSetAllProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Talhão Norte";
        var area = 150.25m;
        var cultura = "Milho";
        var propriedadeId = Guid.NewGuid();

        // Act
        var entity = new TalhaoEntity(id, nome, area, cultura, propriedadeId);

        // Assert
        Assert.Equal(id, entity.Id);
        Assert.Equal(nome, entity.Nome);
        Assert.Equal(area, entity.Area);
        Assert.Equal(cultura, entity.Cultura);
        Assert.Equal(propriedadeId, entity.PropriedadeId);
    }

    [Fact]
    public void TalhaoEntity_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var entity = new TalhaoEntity();
        var nome = "Talhão Sul";
        var area = 200.50m;
        var cultura = "Trigo";
        var propriedadeId = Guid.NewGuid();

        // Act
        entity.Nome = nome;
        entity.Area = area;
        entity.Cultura = cultura;
        entity.PropriedadeId = propriedadeId;

        // Assert
        Assert.Equal(nome, entity.Nome);
        Assert.Equal(area, entity.Area);
        Assert.Equal(cultura, entity.Cultura);
        Assert.Equal(propriedadeId, entity.PropriedadeId);
    }
}
