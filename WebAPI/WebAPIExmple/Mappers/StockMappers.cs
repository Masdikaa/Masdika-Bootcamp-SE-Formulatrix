public static class StocksMapper {
    public static StockDto ToStockDto(this Stock stockModel) {
        return new StockDto {
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            Company = stockModel.Company,
            MarketCap = stockModel.MarketCap,
            Purchase = stockModel.Purchase,
            Industries = stockModel.Industries,
            LastDiv = stockModel.LastDiv
        };
    }
}