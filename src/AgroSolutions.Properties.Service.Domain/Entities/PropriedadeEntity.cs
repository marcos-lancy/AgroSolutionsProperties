namespace AgroSolutions.Properties.Service.Domain.Entities;

public class PropriedadeEntity : EntityBase
{
    public string Nome { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public decimal AreaTotal { get; set; }
    public Guid ProdutorId { get; set; }
    public virtual List<TalhaoEntity> Talhoes { get; set; } = new();

    public PropriedadeEntity()
    {
    }

    public PropriedadeEntity(
        Guid id,
        string nome,
        string endereco,
        decimal areaTotal,
        Guid produtorId)
    {
        Id = id;
        Nome = nome;
        Endereco = endereco;
        AreaTotal = areaTotal;
        ProdutorId = produtorId;
    }
}
