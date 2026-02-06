namespace AgroSolutions.Properties.Service.Application.Dtos.Talhao;

public class CadastrarTalhaoDto
{
    public string Nome { get; set; } = string.Empty;
    public decimal Area { get; set; }
    public string Cultura { get; set; } = string.Empty;
    public Guid PropriedadeId { get; set; }
}
