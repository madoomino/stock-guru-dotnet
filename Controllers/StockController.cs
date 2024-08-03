using Microsoft.AspNetCore.Mvc;
using StockGuru.Data;
using StockGuru.Dtos.Stock;
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
    if (stock == null) return NotFound();

    return Ok(stock.ToStockDto());
  }

  [HttpPost]
  public IActionResult CreateStock([FromBody] CreateStockRequestDto stockDto)
  {
    var stock = stockDto.ToStockFromCreateDto();
    _context.Add(stock);
    _context.SaveChanges();
    return CreatedAtAction(nameof(GetStock), new { id = stock.Id }, stock.ToStockDto());
  }

  [HttpPost("/many")]
  public IActionResult CreateStocks([FromBody] CreateStockRequestDto[] stockDtos)
  {
    var stocks = stockDtos.Select(s => s.ToStockFromCreateDto()).ToList();
    _context.AddRange(stocks);
    _context.SaveChanges();

    var stockDtosToReturn = stocks.Select(s => s.ToStockDto()).ToList();
    return CreatedAtAction(nameof(GetStock), null, stockDtosToReturn);
  }

  [HttpPut("{id}")]
  public IActionResult UpdateStock([FromRoute] int id, UpdateStockRequestDto updateStockDto)
  {
    var stock = _context.Stocks.FirstOrDefault(s => s.Id == id);
    if (stock == null) return NotFound();

    stock.CompanyName = updateStockDto.CompanyName;
    stock.MarketCap = updateStockDto.MarketCap;
    stock.LastDiv = updateStockDto.LastDiv;
    stock.Industry = updateStockDto.Industry;
    stock.Purchase = updateStockDto.Purchase;
    stock.Symbol = updateStockDto.Symbol;

    _context.SaveChanges();
    return Ok(stock.ToStockDto());
  }

  [HttpDelete("{id}")]
  public IActionResult DeleteStock([FromRoute] int id)
  {
    var stock = _context.Stocks.FirstOrDefault(s => s.Id == id);
    if (stock == null) return NotFound();

    _context.Stocks.Remove(stock);
    _context.SaveChanges();
    return NoContent();
  }
}