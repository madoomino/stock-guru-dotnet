using Microsoft.EntityFrameworkCore;
using StockGuru.Data;
using StockGuru.Dtos.Comment;
using StockGuru.Interfaces;
using StockGuru.Models;

namespace StockGuru.Repo;

public class CommentRepo(ApplicationDbContext context) : ICommentRepo
{
  public async Task<List<Comment>> GetCommentsAsync()
  {
    var comments = await context.Comments.ToListAsync();
    return comments;
  }

  public async Task<Comment?> GetCommentByIdAsync(int id)
  {
    return await context.Comments.FindAsync(id);
  }


  public async Task<Comment> CreateCommentAsync(Comment comment)
  {
    await context.Comments.AddAsync(comment);
    await context.SaveChangesAsync();
    return comment;
  }
}