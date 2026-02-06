using AgroSolutions.Properties.Service.Application.Dtos.Propriedade;
using AgroSolutions.Properties.Service.Application.Dtos.Talhao;
using AgroSolutions.Properties.Service.Application.Interfaces;
using AgroSolutions.Properties.Service.Domain.Exceptions.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace AgroSolutions.Properties.Service.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class PropriedadesController : MainController
{
    private readonly IPropriedadeAppService _propriedadeAppService;
    private readonly IUsuarioAutenticadoAppService _usuarioAutenticadoAppService;

    public PropriedadesController(
        IPropriedadeAppService propriedadeAppService,
        IUsuarioAutenticadoAppService usuarioAutenticadoAppService)
    {
        _propriedadeAppService = propriedadeAppService;
        _usuarioAutenticadoAppService = usuarioAutenticadoAppService;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cadastra uma nova propriedade")]
    [ProducesResponseType(typeof(PropriedadeDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Cadastrar([FromBody] CadastrarPropriedadeDto dto)
    {
        var produtorId = _usuarioAutenticadoAppService.ObterIdUsuarioAutenticado();
        var propriedade = await _propriedadeAppService.CadastrarAsync(produtorId, dto);
        return Created($"/propriedades/{propriedade.Id}", propriedade);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Lista todas as propriedades do produtor autenticado")]
    [ProducesResponseType(typeof(List<PropriedadeDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Listar()
    {
        var produtorId = _usuarioAutenticadoAppService.ObterIdUsuarioAutenticado();
        return Ok(await _propriedadeAppService.ObterPorProdutorIdAsync(produtorId));
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Obtém uma propriedade por ID")]
    [ProducesResponseType(typeof(PropriedadeDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id) 
        => Ok(await _propriedadeAppService.ObterPorIdAsync(id));

    [HttpPost("{propriedadeId}/talhoes")]
    [SwaggerOperation(Summary = "Cadastra um novo talhão em uma propriedade")]
    [ProducesResponseType(typeof(TalhaoDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> CadastrarTalhao(Guid propriedadeId, [FromBody] CadastrarTalhaoDto dto)
    {
        var talhao = await _propriedadeAppService.CadastrarTalhaoAsync(propriedadeId, dto);
        return Created($"/propriedades/{propriedadeId}/talhoes/{talhao.Id}", talhao);
    }

    [HttpGet("{propriedadeId}/talhoes")]
    [SwaggerOperation(Summary = "Lista todos os talhões de uma propriedade")]
    [ProducesResponseType(typeof(List<TalhaoDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ListarTalhoes(Guid propriedadeId) 
        => Ok(await _propriedadeAppService.ObterTalhoesPorPropriedadeIdAsync(propriedadeId));
}
