using StockGuru.Dtos.Comment;
using StockGuru.Models;

namespace StockGuru.Interfaces;

public interface ICommentRepo
{
  Task<List<Comment>> GetCommentsAsync();
  Task<Comment?> GetCommentByIdAsync(int id);
  Task<Comment> CreateCommentAsync(Comment comment);
  Task<Comment?> DeleteCommentAsync(int id);
  Task<Comment?> UpdateCommentAsync(int id, UpdateCommentDto commentDto);
  Task<bool> CommentExists(int id);
}