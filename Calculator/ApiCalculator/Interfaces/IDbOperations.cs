using Microsoft.EntityFrameworkCore;

namespace ApiCalculator.Interfaces;

public interface IDbOperations<TDbContext, TItem, T> where TItem : class where TDbContext : DbContext
{
    // private readonly TDbContext _context;
    // public void AddItem(TItem item);
    // public List<TItem> GetAllItems();
    // public TItem GetItemById(T id);
}