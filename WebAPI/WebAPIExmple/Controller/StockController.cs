using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controller;

[Route("stock")]
[ApiController]
public class StockController : ControllerBase {
    private readonly ApplicationDbContext _context;
    public StockController(ApplicationDbContext context) {
        _context = context;
    }

    [HttpGet] // Reading data
    public async Task<IActionResult> GetAll() {
        var stocks = await _context.Stocks.Select(s => s.ToStockDto()).ToListAsync(); // Select DTO melalui Mapper
        return Ok(stocks);
    }

    [HttpGet("{id}")] // Read data by Id
    public async Task<IActionResult> GetById([FromRoute] int id) {
        var stock = await _context.Stocks.FindAsync(id);
        if (stock != null) {
            return Ok(stock.ToStockDto());
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto) {
        var stockModel = stockDto.ToStockFromCreateDto();
        await _context.Stocks.AddAsync(stockModel);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto) {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);
        if (stockModel != null) {
            stockModel.Symbol = updateDto.Symbol;
            stockModel.Company = updateDto.Company;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industries = updateDto.Industries;
            stockModel.MarketCap = updateDto.MarketCap;
            await _context.SaveChangesAsync();
            return Ok(stockModel.ToStockDto());
        }
        return NotFound();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id) {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);
        if (stockModel != null) {
            _context.Stocks.Remove(stockModel); // Remove isnt async func
            await _context.SaveChangesAsync();
        }
        return NotFound();
    }

}