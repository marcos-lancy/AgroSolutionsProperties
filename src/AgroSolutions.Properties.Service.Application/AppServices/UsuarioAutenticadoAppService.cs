using AgroSolutions.Properties.Service.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AgroSolutions.Properties.Service.Application.AppServices;

public class UsuarioAutenticadoAppService : IUsuarioAutenticadoAppService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsuarioAutenticadoAppService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid ObterIdUsuarioAutenticado()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            throw new UnauthorizedAccessException("Usuário não autenticado.");

        return userId;
    }

    public string ObterEmailUsuarioAutenticado()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value
            ?? _httpContextAccessor.HttpContext?.User?.FindFirst("email")?.Value
            ?? throw new UnauthorizedAccessException("Usuário não autenticado.");
    }
}
