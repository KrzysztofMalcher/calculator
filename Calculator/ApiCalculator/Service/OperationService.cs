using ApiCalculator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiCalculator.infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ApiCalculator.Service;

public class OperationService
{
    private readonly OperationsDbContext _context;

    public OperationService(OperationsDbContext context)
    {
        _context = context;
    }
    
    public async Task AddOperation(Operation operation)
    {
        _context.Operations.Add(operation);
        await _context.SaveChangesAsync();
    }
    
    public async Task<List<Operation>> GetAllOperations()
    {
        return await _context.Operations.ToListAsync();
    }
    
    public async Task<Operation?> GetOperationById(int id)
    {
        return await _context.Operations.FindAsync(id);
    }
}
