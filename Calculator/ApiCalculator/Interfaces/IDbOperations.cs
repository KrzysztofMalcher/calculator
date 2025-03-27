using Microsoft.EntityFrameworkCore;

namespace ApiCalculator.Interfaces;

public interface IDbOperations<TDbContext, TItem> where TItem : class where TDbContext : DbContext
{
    public Task AddItem(TItem item);
    public Task<List<TItem>> GetAllItems();
    public Task<TItem?> GetItemById(int id);
}