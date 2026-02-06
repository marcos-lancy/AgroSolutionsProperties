namespace AgroSolutions.Properties.Service.Application.Dtos.Propriedade;

public class CadastrarPropriedadeDto
{
    public string Nome { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public decimal AreaTotal { get; set; }
}
