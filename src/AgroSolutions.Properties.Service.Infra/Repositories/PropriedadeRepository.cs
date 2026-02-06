using AgroSolutions.Properties.Service.Domain.Entities;
using AgroSolutions.Properties.Service.Domain.Interfaces;
using AgroSolutions.Properties.Service.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AgroSolutions.Properties.Service.Infra.Repositories;

public class PropriedadeRepository(AppDbContext context) : Repository<PropriedadeEntity>(context), IPropriedadeRepository
{
    public async Task<List<PropriedadeEntity>> ObterPorProdutorIdAsync(Guid produtorId)
    {
        return await _dbSet
            .Include(p => p.Talhoes)
            .Where(p => p.ProdutorId == produtorId)
            .ToListAsync();
    }

    public async Task<PropriedadeEntity?> ObterComTalhoesAsync(Guid id)
    {
        return await _dbSet
            .Include(p => p.Talhoes)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
