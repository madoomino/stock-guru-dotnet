using System.ComponentModel.DataAnnotations;

namespace StockGuru.Dtos.Stock;

public class StockDto
{
  public int Id { get; set; }

  [Required]
  [MinLength(1, ErrorMessage = "Symbol must be at least 1 chars..")]
  [MaxLength(10, ErrorMessage = "Symbol must be less than 10 chars..")]
  public string Symbol { get; set; } = string.Empty;

  [Required]
  [MinLength(3, ErrorMessage = "CompanyName must be at least 3 chars..")]
  [MaxLength(10, ErrorMessage = "CompanyName must be less than 10 chars..")]
  public string CompanyName { get; set; } = string.Empty;

  [Required] [Range(1, 100_000_000)] public decimal Purchase { get; set; }
  [Required] [Range(0.1, 100)] public decimal LastDiv { get; set; }

  [Required]
  [MinLength(3, ErrorMessage = "Industry name must be at least 3 chars..")]
  [MaxLength(30, ErrorMessage = "Industry name must be less than 30 chars..")]
  public string Industry { get; set; } = string.Empty;

  [Required] [Range(1, 5_000_000_000)] public long MarketCap { get; set; }
  public List<Models.Comment>? Comments { get; set; }
}