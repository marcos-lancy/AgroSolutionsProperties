namespace AgroSolutions.Properties.Service.Application.Interfaces;

public interface IUsuarioAutenticadoAppService
{
    Guid ObterIdUsuarioAutenticado();
    string ObterEmailUsuarioAutenticado();
}
