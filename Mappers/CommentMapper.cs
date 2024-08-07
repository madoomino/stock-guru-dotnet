using StockGuru.Dtos.Comment;
using StockGuru.Models;

namespace StockGuru.Mappers;

public static class CommentMapper
{
  public static CommentDto ToCommentDto(this Comment comment)
  {
    return new CommentDto()
    {
      Id = comment.Id,
      Title = comment.Title,
      Content = comment.Content,
      CreatedAt = comment.CreatedAt,
      StockId = comment.StockId
    };
  }
}