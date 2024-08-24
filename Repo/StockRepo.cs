using Microsoft.EntityFrameworkCore;
using StockGuru.Data;
using StockGuru.Dtos.Stock;
using StockGuru.Helpers;
using StockGuru.Interfaces;
using StockGuru.Models;

namespace StockGuru.Repo;

public class StockRepo(ApplicationDbContext context) : IStockRepo
{
  public async Task<List<Stock>> GetStocksAsync(QueryObject queryObject)
  {
    var stocks = context.Stocks.Include(s => s.Comments).AsQueryable();

    if (!string.IsNullOrWhiteSpace(queryObject.CompanyName))
      stocks = stocks.Where(s => s.CompanyName.Contains(queryObject.CompanyName));

    if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
      stocks = stocks.Where(s => s.Symbol.Contains(queryObject.Symbol));

    if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
      if (queryObject.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
        stocks = queryObject.IsDescending
          ? stocks.OrderByDescending(s => s.Symbol)
          : stocks.OrderBy(s => s.Symbol);

    var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;

    return await stocks.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
  }

  public async Task<Stock?> GetStockByIdAsync(int id)
  {
    return await context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);
  }

  public async Task<Stock> CreateStockAsync(Stock stock)
  {
    await context.Stocks.AddAsync(stock);
    await context.SaveChangesAsync();
    return stock;
  }

  public async Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto stockRequestDto)
  {
    var stock = await context.Stocks.FindAsync(id);
    if (stock == null) return null;
    stock.CompanyName = stockRequestDto.CompanyName;
    stock.MarketCap = stockRequestDto.MarketCap;
    stock.LastDiv = stockRequestDto.LastDiv;
    stock.Industry = stockRequestDto.Industry;
    stock.Purchase = stockRequestDto.Purchase;
    stock.Symbol = stockRequestDto.Symbol;

    await context.SaveChangesAsync();
    return stock;
  }

  public async Task<Stock?> DeleteStockAsync(int id)
  {
    var stock = await context.Stocks.FindAsync(id);
    if (stock == null) return null;

    context.Stocks.Remove(stock);
    await context.SaveChangesAsync();
    return stock;
  }

  public async Task<bool> StockExists(int stockId)
  {
    return await context.Stocks.AnyAsync(s => s.Id == stockId);
  }
}