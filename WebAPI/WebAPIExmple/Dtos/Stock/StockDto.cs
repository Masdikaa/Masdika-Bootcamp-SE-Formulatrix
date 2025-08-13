public class StockDto {
    public int Id { get; set; }
    public string Symbol { set; get; } = string.Empty;
    public string Company { set; get; } = string.Empty;
    public decimal Purchase { get; set; }
    public decimal LastDiv { get; set; }
    public string Industries { get; set; } = string.Empty;
    public long MarketCap { get; set; }
    // public List<Comment> Comments { get; set; } = new List<Comment>(); tidak akan menggunakan comment
}