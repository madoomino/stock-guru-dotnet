using Microsoft.AspNetCore.Mvc;
using StockGuru.Dtos.Comment;
using StockGuru.Interfaces;
using StockGuru.Mappers;
using StockGuru.Models;

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
  public async Task<IActionResult> CreateComment([FromRoute] int stockId,
    CreateCommentDto commentDto)
  {
    var stockExists = await stockRepo.StockExists(stockId);
    if (!stockExists) return BadRequest("Stock not found.");

    var comment = commentDto.ToCommentFromCreateDto(stockId);
    await commentRepo.CreateCommentAsync(comment);
    return Ok(comment);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateCommentAsync([FromRoute] int id,
    UpdateCommentDto commentDto)
  {
    var comment = await commentRepo.UpdateCommentAsync(id, commentDto);
    if (comment == null) return BadRequest("Comment not found.");
    return Ok(comment.ToCommentDto());
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteCommentAsync([FromRoute] int id)
  {
    var deletedComment = await commentRepo.DeleteCommentAsync(id);
    if (deletedComment == null) return BadRequest("Comment not found.");
    return NoContent();
  }
}