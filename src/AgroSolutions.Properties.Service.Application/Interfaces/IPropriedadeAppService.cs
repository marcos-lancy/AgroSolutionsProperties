using AgroSolutions.Properties.Service.Application.Dtos.Propriedade;
using AgroSolutions.Properties.Service.Application.Dtos.Talhao;

namespace AgroSolutions.Properties.Service.Application.Interfaces;

public interface IPropriedadeAppService
{
    Task<PropriedadeDto> CadastrarAsync(Guid produtorId, CadastrarPropriedadeDto dto);
    Task<List<PropriedadeDto>> ObterPorProdutorIdAsync(Guid produtorId);
    Task<PropriedadeDto?> ObterPorIdAsync(Guid id);
    Task<TalhaoDto> CadastrarTalhaoAsync(Guid propriedadeId, CadastrarTalhaoDto dto);
    Task<List<TalhaoDto>> ObterTalhoesPorPropriedadeIdAsync(Guid propriedadeId);
}
