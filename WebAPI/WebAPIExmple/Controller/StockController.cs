using Microsoft.AspNetCore.Mvc;

[Route("stock")]
[ApiController]
public class StockController : ControllerBase {

    private readonly IStockRepository _stockRepository;

    public StockController(IStockRepository stockRepository) {
        _stockRepository = stockRepository;
    }

    [HttpGet] // Reading data
    public async Task<IActionResult> GetAll() {
        var stocks = await _stockRepository.GetAllAsync();
        var stockDto = stocks.Select(s => s.ToStockDto());
        return Ok(stocks);
    }

    [HttpGet("{id}")] // Read data by Id
    public async Task<IActionResult> GetById([FromRoute] int id) {
        var stock = await _stockRepository.GetByIdAsync(id);
        if (stock != null) {
            return Ok(stock.ToStockDto());
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto) {
        var stockModel = stockDto.ToStockFromCreateDto();
        await _stockRepository.CreateAsync(stockModel);
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto) {
        var stockModel = await _stockRepository.UpdateAsync(id, updateDto);
        if (stockModel == null) {
            return NotFound();
        }
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id) {
        var stockModel = await _stockRepository.DeleteAsync(id);
        if (stockModel == null) {
            return NotFound();
        }
        return NoContent();
    }

}