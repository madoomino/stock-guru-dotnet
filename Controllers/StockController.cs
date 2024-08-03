using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockGuru.Data;
using StockGuru.Dtos.Stock;
using StockGuru.Interfaces;
using StockGuru.Mappers;

namespace StockGuru.Controllers;

[Route("api/stocks")]
[ApiController]
public class StockController(ApplicationDbContext context, IStockRepo stockRepo) : ControllerBase
{
  private readonly ApplicationDbContext _context = context;

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var stocks = await stockRepo.GetAllAsync();
    var stocksDtos = stocks.Select(s => s.ToStockDto());
    return Ok(stocksDtos);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetStock([FromRoute] int id)
  {
    var stock = await _context.Stocks.FindAsync(id);
    if (stock == null) return NotFound();

    return Ok(stock.ToStockDto());
  }

  [HttpPost]
  public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto stockDto)
  {
    var stock = stockDto.ToStockFromCreateDto();
    await _context.AddAsync(stock);
    await _context.SaveChangesAsync();
    return CreatedAtAction(nameof(GetStock), new { id = stock.Id }, stock.ToStockDto());
  }

  [HttpPost("/many")]
  public async Task<IActionResult> CreateStocks([FromBody] CreateStockRequestDto[] stockDtos)
  {
    var stocks = stockDtos.Select(s => s.ToStockFromCreateDto()).ToList();
    await _context.AddRangeAsync(stocks);
    await _context.SaveChangesAsync();

    var stockDtosToReturn = stocks.Select(s => s.ToStockDto()).ToList();
    return CreatedAtAction(nameof(GetStock), null, stockDtosToReturn);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateStock([FromRoute] int id,
    UpdateStockRequestDto updateStockDto)
  {
    var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
    if (stock == null) return NotFound();

    stock.CompanyName = updateStockDto.CompanyName;
    stock.MarketCap = updateStockDto.MarketCap;
    stock.LastDiv = updateStockDto.LastDiv;
    stock.Industry = updateStockDto.Industry;
    stock.Purchase = updateStockDto.Purchase;
    stock.Symbol = updateStockDto.Symbol;

    await _context.SaveChangesAsync();
    return Ok(stock.ToStockDto());
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteStock([FromRoute] int id)
  {
    var stock = _context.Stocks.FirstOrDefault(s => s.Id == id);
    if (stock == null) return NotFound();

    _context.Stocks.Remove(stock);
    await _context.SaveChangesAsync();
    return NoContent();
  }
}