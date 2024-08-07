using Microsoft.EntityFrameworkCore;
using StockGuru.Data;
using StockGuru.Interfaces;
using StockGuru.Models;

namespace StockGuru.Repo;

public class CommentRepo(ApplicationDbContext context) : ICommentRepo
{
  public async Task<List<Comment>> GetAllCommentsAsync()
  {
    return await context.Comments.ToListAsync();
  }
}