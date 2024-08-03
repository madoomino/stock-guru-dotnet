using StockGuru.Models;

namespace StockGuru.Interfaces;

public interface IStockRepo
{
  Task<List<Stock>> GetAllAsync();
}