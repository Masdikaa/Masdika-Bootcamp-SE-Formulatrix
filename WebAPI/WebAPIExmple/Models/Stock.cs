using System.ComponentModel.DataAnnotations.Schema;

public class Stock {

    // Table contents
    public int Id { get; set; }
    public string Symbol { set; get; } = string.Empty;
    public string Company { set; get; } = string.Empty;
    [Column(TypeName = "decimal(18,2)")]
    public decimal Purchase { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal LastDiv { get; set; }
    public string Industries { get; set; } = string.Empty;
    public long MarketCap { get; set; }

    public List<Comment> Comments { get; set; } = new List<Comment>();
}