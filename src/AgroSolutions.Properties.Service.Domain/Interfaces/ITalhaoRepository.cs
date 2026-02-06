using AgroSolutions.Properties.Service.Domain.Entities;

namespace AgroSolutions.Properties.Service.Domain.Interfaces;

public interface ITalhaoRepository : IRepository<TalhaoEntity>
{
    Task<List<TalhaoEntity>> ObterPorPropriedadeIdAsync(Guid propriedadeId);
}
