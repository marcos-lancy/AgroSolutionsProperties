using AgroSolutions.Properties.Service.Domain.Entities;

namespace AgroSolutions.Properties.Service.Domain.Interfaces;

public interface IPropriedadeRepository : IRepository<PropriedadeEntity>
{
    Task<List<PropriedadeEntity>> ObterPorProdutorIdAsync(Guid produtorId);
    Task<PropriedadeEntity?> ObterComTalhoesAsync(Guid id);
}
