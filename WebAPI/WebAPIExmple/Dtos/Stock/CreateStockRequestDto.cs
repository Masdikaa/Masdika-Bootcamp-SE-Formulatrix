public class CreateStockRequestDto {
    public string Symbol { set; get; } = string.Empty;
    public string Company { set; get; } = string.Empty;
    public decimal Purchase { get; set; }
    public decimal LastDiv { get; set; }
    public string Industries { get; set; } = string.Empty;
    public long MarketCap { get; set; }
}