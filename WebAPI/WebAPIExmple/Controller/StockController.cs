using Microsoft.AspNetCore.Mvc;

namespace Controller;

[Route("stock")]
[ApiController]
public class StockController : ControllerBase {
    private readonly ApplicationDbContext _context;
    public StockController(ApplicationDbContext context) {
        _context = context;
    }

    [HttpGet] // Reading data
    public IActionResult GetAll() {
        var stocks = _context.Stocks.Select(s => s.ToStockDto()).ToList(); // Select DTO melalui Mapper
        return Ok(stocks);
    }

    [HttpGet("{id}")] // Read data by Id
    public IActionResult GetById([FromRoute] int id) {
        var stock = _context.Stocks.Find(id);
        if (stock != null) {
            return Ok(stock.ToStockDto());
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateStockRequestDto stockDto) {
        var stockModel = stockDto.ToStockFromCreateDto();
        _context.Stocks.Add(stockModel);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto) {
        var stockModel = _context.Stocks.FirstOrDefault(stock => stock.Id == id);
        if (stockModel != null) {
            stockModel.Symbol = updateDto.Symbol;
            stockModel.Company = updateDto.Company;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industries = updateDto.Industries;
            stockModel.MarketCap = updateDto.MarketCap;
            _context.SaveChanges();
            return Ok(stockModel.ToStockDto());
        }
        return NotFound();
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete([FromRoute] int id) {
        var stockModel = _context.Stocks.FirstOrDefault(stock => stock.Id == id);
        if (stockModel != null) {
            _context.Stocks.Remove(stockModel);
            _context.SaveChanges();
        }
        return NotFound();
    }

}