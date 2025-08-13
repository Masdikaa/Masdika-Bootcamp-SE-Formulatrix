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
}