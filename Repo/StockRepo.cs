using Microsoft.EntityFrameworkCore;
using StockGuru.Data;
using StockGuru.Interfaces;
using StockGuru.Models;

namespace StockGuru.Repo;

public class StockRepo(ApplicationDbContext context) : IStockRepo
{
  public async Task<List<Stock>> GetAllAsync()
  {
    return await context.Stocks.ToListAsync();
  }
}