using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockGuru.Data;
using StockGuru.Dtos.Stock;
using StockGuru.Interfaces;
using StockGuru.Mappers;

namespace StockGuru.Controllers;

[Route("api/stocks")]
[ApiController]
public class StockController(IStockRepo stockRepo) : ControllerBase
{
  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var stocks = await stockRepo.GetStocksAsync();
    var stocksDtos = stocks.Select(s => s.ToStockDto());
    return Ok(stocksDtos);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetStock([FromRoute] int id)
  {
    var stock = await stockRepo.GetStockByIdAsync(id);
    if (stock == null) return NotFound();

    return Ok(stock.ToStockDto());
  }

  [HttpPost]
  public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto stockDto)
  {
    var stock = stockDto.ToStockFromCreateDto();
    await stockRepo.CreateStockAsync(stock);
    return CreatedAtAction(nameof(GetStock), new { id = stock.Id }, stock.ToStockDto());
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateStock([FromRoute] int id,
    UpdateStockRequestDto updateStockDto)
  {
    var updatedStock = await stockRepo.UpdateStockAsync(id, updateStockDto);
    if (updatedStock == null) return NotFound();
    return Ok(updatedStock.ToStockDto());
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteStock([FromRoute] int id)
  {
    var stock = await stockRepo.DeleteStockAsync(id);
    if (stock == null) return NotFound();
    return NoContent();
  }
}