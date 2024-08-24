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

  public async Task<Comment?> DeleteCommentAsync(int id)
  {
    var comment = await context.Comments.FindAsync(id);
    if (comment == null) return null;

    context.Comments.Remove(comment);
    await context.SaveChangesAsync();
    return comment;
  }

  public async Task<Comment?> UpdateCommentAsync(int id, UpdateCommentDto commentDto)
  {
    var comment = await context.Comments.FindAsync(id);
    if (comment == null) return null;

    comment.Title = commentDto.Title;
    comment.Content = commentDto.Content;

    await context.SaveChangesAsync();
    return comment;
  }

  public async Task<bool> CommentExists(int id)
  {
    return await context.Comments.AnyAsync(c => c.Id == id);
  }
}