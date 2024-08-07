using Microsoft.AspNetCore.Mvc;
using StockGuru.Data;
using StockGuru.Interfaces;
using StockGuru.Mappers;
using StockGuru.Models;

namespace StockGuru.Controllers;

[Route("/api/comments")]
[ApiController]
public class CommentController(ICommentRepo commentRepo) : ControllerBase
{
  [HttpGet]
  public async Task<IActionResult> GetAllComments()
  {
    var comments = await commentRepo.GetAllCommentsAsync();
    var commentDtos = comments.Select(c => c.ToCommentDto());
    return Ok(commentDtos);
  }
}