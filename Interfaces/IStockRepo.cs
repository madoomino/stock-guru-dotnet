using StockGuru.Dtos.Stock;
using StockGuru.Models;

namespace StockGuru.Interfaces;

public interface IStockRepo
{
  Task<List<Stock>> GetStocksAsync();
  Task<Stock?> GetStockByIdAsync(int id);
  Task<Stock> CreateStockAsync(Stock stock);
  Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto stockRequestDto);
  Task<Stock?> DeleteStockAsync(int id);
  Task<bool> StockExists(int stockId);
}