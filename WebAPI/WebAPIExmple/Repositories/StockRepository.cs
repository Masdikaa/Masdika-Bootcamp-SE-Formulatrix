using Microsoft.EntityFrameworkCore;

public class StockRepository : IStockRepository {

    private readonly ApplicationDbContext _context;

    public StockRepository(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<Stock> CreateAsync(Stock stockModel) {
        await _context.Stocks.AddAsync(stockModel);
        await _context.SaveChangesAsync();
        return stockModel;
    }

    public async Task<Stock?> DeleteAsync(int id) {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);
        if (stockModel == null) {
            return null;
        }
        _context.Stocks.Remove(stockModel);
        await _context.SaveChangesAsync();
        return stockModel;
    }

    public Task<List<Stock>> GetAllAsync() {
        return _context.Stocks.ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id) {
        var stockModel = await _context.Stocks.FindAsync(id);
        return stockModel;
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto) {
        var existingStock = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);
        if (existingStock == null) {
            return null;
        }
        existingStock.Symbol = stockDto.Symbol;
        existingStock.Company = stockDto.Company;
        existingStock.Purchase = stockDto.Purchase;
        existingStock.LastDiv = stockDto.LastDiv;
        existingStock.Industries = stockDto.Industries;
        existingStock.MarketCap = stockDto.MarketCap;
        await _context.SaveChangesAsync();
        return existingStock;
    }
}