using StockGuru.Models;

namespace StockGuru.Interfaces;

public interface ICommentRepo
{
  Task<List<Comment>> GetAllCommentsAsync();
}