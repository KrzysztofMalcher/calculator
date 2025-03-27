using ApiCalculator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiCalculator.infrastructure.Database;
using ApiCalculator.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiCalculator.Service;

public class OperationService : IDbOperations<OperationsDbContext, Operation>
{
    private readonly OperationsDbContext _context;

    public OperationService(OperationsDbContext context)
    {
        _context = context;
    }
    
    public async Task AddItem(Operation operation)
    {
        _context.Operations.Add(operation);
        await _context.SaveChangesAsync();
    }
    
    public async Task<List<Operation>> GetAllItems()
    {
        return await _context.Operations.ToListAsync();
    }
    
    public async Task<Operation?> GetItemById(int id)
    {
        return await _context.Operations.FindAsync(id);
    }
}
