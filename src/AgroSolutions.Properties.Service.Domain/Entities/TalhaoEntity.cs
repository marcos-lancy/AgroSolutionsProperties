namespace AgroSolutions.Properties.Service.Domain.Entities;

public class TalhaoEntity : EntityBase
{
    public string Nome { get; set; } = string.Empty;
    public decimal Area { get; set; }
    public string Cultura { get; set; } = string.Empty;
    public Guid PropriedadeId { get; set; }
    public virtual  PropriedadeEntity? Propriedade { get; set; }

    public TalhaoEntity()
    {
    }

    public TalhaoEntity(
        Guid id,
        string nome,
        decimal area,
        string cultura,
        Guid propriedadeId)
    {
        Id = id;
        Nome = nome;
        Area = area;
        Cultura = cultura;
        PropriedadeId = propriedadeId;
    }
}
