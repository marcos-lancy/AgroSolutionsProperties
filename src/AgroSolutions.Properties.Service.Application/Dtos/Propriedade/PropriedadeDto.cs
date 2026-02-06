using AgroSolutions.Properties.Service.Application.Dtos.Talhao;

namespace AgroSolutions.Properties.Service.Application.Dtos.Propriedade;

public class PropriedadeDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public decimal AreaTotal { get; set; }
    public Guid ProdutorId { get; set; }
    public List<TalhaoDto> Talhoes { get; set; } = new();
}
