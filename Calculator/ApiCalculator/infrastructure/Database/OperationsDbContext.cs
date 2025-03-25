using ApiCalculator.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCalculator.infrastructure.Database;

public sealed class OperationsDbContext : DbContext
{
    public OperationsDbContext(DbContextOptions<OperationsDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Operation> Operations { get; set; }
}