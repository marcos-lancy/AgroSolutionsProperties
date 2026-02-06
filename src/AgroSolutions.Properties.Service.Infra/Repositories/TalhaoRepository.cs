using AgroSolutions.Properties.Service.Domain.Entities;
using AgroSolutions.Properties.Service.Domain.Interfaces;
using AgroSolutions.Properties.Service.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AgroSolutions.Properties.Service.Infra.Repositories;

public class TalhaoRepository(AppDbContext context) : Repository<TalhaoEntity>(context), ITalhaoRepository
{
    public async Task<List<TalhaoEntity>> ObterPorPropriedadeIdAsync(Guid propriedadeId)
    {
        return await _dbSet
            .Where(t => t.PropriedadeId == propriedadeId)
            .ToListAsync();
    }
}
