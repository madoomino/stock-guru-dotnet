using StockGuru.Models;

namespace StockGuru.Interfaces;

public interface ICommentRepo
{
  Task<List<Comment>> GetCommentsAsync();
  Task<Comment?> GetCommentByIdAsync(int id);
}