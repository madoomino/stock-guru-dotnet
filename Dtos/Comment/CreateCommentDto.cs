using System.ComponentModel.DataAnnotations;

namespace StockGuru.Dtos.Comment;

public class CreateCommentDto
{
  [Required]
  [MinLength(4, ErrorMessage = "Content must be at least 4 chars..")]
  [MaxLength(400, ErrorMessage = "Content must be less than 400 chars..")]
  public string Title { get; set; } = string.Empty;

  [Required]
  [MinLength(4, ErrorMessage = "Content must be at least 4 chars..")]
  [MaxLength(400, ErrorMessage = "Content must be less than 400 chars..")]
  public string Content { get; set; } = string.Empty;
}