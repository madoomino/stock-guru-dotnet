using Microsoft.AspNetCore.Mvc;
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
    if (!ModelState.IsValid) return BadRequest(ModelState);

    var stocks = await stockRepo.GetStocksAsync();
    var stocksDtos = stocks.Select(s => s.ToStockDto());
    return Ok(stocksDtos);
  }

  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetStock([FromRoute] int id)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    var stock = await stockRepo.GetStockByIdAsync(id);
    if (stock == null) return NotFound();

    return Ok(stock.ToStockDto());
  }

  [HttpPost]
  public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto stockDto)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    var stock = stockDto.ToStockFromCreateDto();
    await stockRepo.CreateStockAsync(stock);
    return CreatedAtAction(nameof(GetStock), new { id = stock.Id }, stock.ToStockDto());
  }

  [HttpPut("{id:int}")]
  public async Task<IActionResult> UpdateStock([FromRoute] int id,
    UpdateStockRequestDto updateStockDto)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    var updatedStock = await stockRepo.UpdateStockAsync(id, updateStockDto);
    if (updatedStock == null) return NotFound();
    return Ok(updatedStock.ToStockDto());
  }

  [HttpDelete("{id:int}")]
  public async Task<IActionResult> DeleteStock([FromRoute] int id)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    var stock = await stockRepo.DeleteStockAsync(id);
    if (stock == null) return BadRequest("Stock not found.");
    return NoContent();
  }
}