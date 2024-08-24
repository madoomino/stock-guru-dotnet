using Microsoft.AspNetCore.Mvc;
using StockGuru.Dtos.Comment;
using StockGuru.Interfaces;
using StockGuru.Mappers;

namespace StockGuru.Controllers;

[Route("/api/comments")]
[ApiController]
public class CommentController(ICommentRepo commentRepo, IStockRepo stockRepo) : ControllerBase
{
  [HttpGet]
  public async Task<IActionResult> GetAllComments()
  {
    var comments = await commentRepo.GetCommentsAsync();
    var commentDtos = comments.Select(c => c.ToCommentDto());
    return Ok(commentDtos);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetCommentByIdAsync([FromRoute] int id)
  {
    var comment = await commentRepo.GetCommentByIdAsync(id);
    if (comment == null) return NotFound();

    return Ok(comment);
  }

  [HttpPost("{stockId}")]
  public async Task<IActionResult> CreateComment([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto)
  {
    var stockExists = await stockRepo.StockExists(stockId);
    if (!stockExists)
    {
      return BadRequest("Stock not found.");
    }

    var comment = commentDto.ToCommentFromCreateDto(stockId);
    await commentRepo.CreateCommentAsync(comment);
    return Ok(comment);
  }
}