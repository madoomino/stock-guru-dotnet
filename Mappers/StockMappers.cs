using StockGuru.Dtos.Stock;
using StockGuru.Models;

namespace StockGuru.Mappers;

public static class StockMappers
{
  public static StockDto ToStockDto(this Stock stockModel)
  {
    return new StockDto
    {
      Id = stockModel.Id,
      Symbol = stockModel.Symbol,
      CompanyName = stockModel.CompanyName,
      MarketCap = stockModel.MarketCap,
      Industry = stockModel.Industry,
      LastDiv = stockModel.LastDiv,
      Purchase = stockModel.Purchase
    };
  }
}