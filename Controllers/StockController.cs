using Microsoft.AspNetCore.Mvc;
using StockGuru.Data;
using StockGuru.Mappers;

namespace StockGuru.Controllers;

[Route("api/stocks")]
[ApiController]
public class StockController(ApplicationDbContext context) : ControllerBase
{
  private readonly ApplicationDbContext _context = context;

  [HttpGet]
  public IActionResult GetAll()
  {
    var stocks = _context.Stocks.ToList().Select(s => s.ToStockDto());
    return Ok(stocks);
  }

  [HttpGet("{id}")]
  public IActionResult GetStock([FromRoute] int id)
  {
    var stock = _context.Stocks.Find(id);
    if (stock == null)
    {
      return NotFound();
    }
    return Ok(stock.ToStockDto());
  }

}
