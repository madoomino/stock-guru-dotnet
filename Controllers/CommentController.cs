using Microsoft.AspNetCore.Mvc;
using StockGuru.Interfaces;
using StockGuru.Mappers;

namespace StockGuru.Controllers;

[Route("/api/comments")]
[ApiController]
public class CommentController(ICommentRepo commentRepo) : ControllerBase
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
}