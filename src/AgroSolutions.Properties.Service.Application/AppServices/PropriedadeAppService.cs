using AgroSolutions.Properties.Service.Domain.Entities;
using AgroSolutions.Properties.Service.Domain.Exceptions;
using AgroSolutions.Properties.Service.Domain.Interfaces;
using AgroSolutions.Properties.Service.Application.Dtos.Propriedade;
using AgroSolutions.Properties.Service.Application.Dtos.Talhao;
using AgroSolutions.Properties.Service.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace AgroSolutions.Properties.Service.Application.AppServices;

public class PropriedadeAppService : IPropriedadeAppService
{
    private readonly IPropriedadeRepository _propriedadeRepository;
    private readonly ITalhaoRepository _talhaoRepository;
    private readonly ILogger<PropriedadeAppService> _logger;

    public PropriedadeAppService(
        IPropriedadeRepository propriedadeRepository,
        ITalhaoRepository talhaoRepository,
        ILogger<PropriedadeAppService> logger)
    {
        _propriedadeRepository = propriedadeRepository;
        _talhaoRepository = talhaoRepository;
        _logger = logger;
    }

    public async Task<PropriedadeDto> CadastrarAsync(Guid produtorId, CadastrarPropriedadeDto dto)
    {
        _logger.LogInformation("Tentativa de cadastro de propriedade: {@Dto}", dto);

        var novaPropriedade = new PropriedadeEntity
        {
            Nome = dto.Nome,
            Endereco = dto.Endereco,
            AreaTotal = dto.AreaTotal,
            ProdutorId = produtorId
        };

        var registro = await _propriedadeRepository.AdicionarAsync(novaPropriedade);

        return new PropriedadeDto
        {
            Id = registro.Id,
            Nome = registro.Nome,
            Endereco = registro.Endereco,
            AreaTotal = registro.AreaTotal,
            ProdutorId = registro.ProdutorId,
            Talhoes = new List<TalhaoDto>()
        };
    }

    public async Task<List<PropriedadeDto>> ObterPorProdutorIdAsync(Guid produtorId)
    {
        var propriedades = await _propriedadeRepository.ObterPorProdutorIdAsync(produtorId);
        
        return propriedades.Select(p => new PropriedadeDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Endereco = p.Endereco,
            AreaTotal = p.AreaTotal,
            ProdutorId = p.ProdutorId,
            Talhoes = p.Talhoes.Select(t => new TalhaoDto
            {
                Id = t.Id,
                Nome = t.Nome,
                Area = t.Area,
                Cultura = t.Cultura,
                PropriedadeId = t.PropriedadeId
            }).ToList()
        }).ToList();
    }

    public async Task<PropriedadeDto?> ObterPorIdAsync(Guid id)
    {
        var propriedade = await _propriedadeRepository.ObterComTalhoesAsync(id);
        
        if (propriedade == null)
            throw new NotFoundException();

        return new PropriedadeDto
        {
            Id = propriedade.Id,
            Nome = propriedade.Nome,
            Endereco = propriedade.Endereco,
            AreaTotal = propriedade.AreaTotal,
            ProdutorId = propriedade.ProdutorId,
            Talhoes = propriedade.Talhoes.Select(t => new TalhaoDto
            {
                Id = t.Id,
                Nome = t.Nome,
                Area = t.Area,
                Cultura = t.Cultura,
                PropriedadeId = t.PropriedadeId
            }).ToList()
        };
    }

    public async Task<TalhaoDto> CadastrarTalhaoAsync(Guid propriedadeId, CadastrarTalhaoDto dto)
    {
        _logger.LogInformation("Tentativa de cadastro de talhão: {@Dto}", dto);

        var propriedade = await _propriedadeRepository.ObterPorIdAsync(propriedadeId);
        if (propriedade == null)
            throw new NotFoundException("Propriedade não encontrada.");

        var novoTalhao = new TalhaoEntity
        {
            Nome = dto.Nome,
            Area = dto.Area,
            Cultura = dto.Cultura,
            PropriedadeId = propriedadeId
        };

        var registro = await _talhaoRepository.AdicionarAsync(novoTalhao);

        return new TalhaoDto
        {
            Id = registro.Id,
            Nome = registro.Nome,
            Area = registro.Area,
            Cultura = registro.Cultura,
            PropriedadeId = registro.PropriedadeId
        };
    }

    public async Task<List<TalhaoDto>> ObterTalhoesPorPropriedadeIdAsync(Guid propriedadeId)
    {
        var talhoes = await _talhaoRepository.ObterPorPropriedadeIdAsync(propriedadeId);
        
        return talhoes.Select(t => new TalhaoDto
        {
            Id = t.Id,
            Nome = t.Nome,
            Area = t.Area,
            Cultura = t.Cultura,
            PropriedadeId = t.PropriedadeId
        }).ToList();
    }
}
