# Web API

### Create Project by Console

Creating ASP.NET Core Web API Template menggunakan command `dotnet new webpapi -o api`

### Running Project

`dotnet watch run` / `dotnet run`

### Folder Structure

- `Properties` folder untuk menyimpan spesifik settings untuk project
- `launchSettings.json` -> File ini hanya digunakan untuk pengembangan di komputer lokal Anda dan tidak akan ikut di-publish ke server produksi
- `appsettings.json` -> Base Configuration untuk semua env (Development, Staging, Production)
- `appsettings.Development.json` -> Override base configuration untuk use case yang lebih spesifik

### Program Entry Point

1. Create and configure **WebApplicationBuilder**

   ```
   var builder = WebApplication.CreateBuilder(args);

   builder.Services.AddEndpointsApiExplorer();
   builder.Services.AddSwaggerGen()
   ```

2. Configure Pipeline

   ```
   var app = builder.Build();

   if (app.Environment.IsDevelopment()) {
       app.UseSwagger();
       app.UseSwaggerUI();
   }

   app.UseHttpsRedirection();
   ```

3. Define API EndPoint

   ```
   var summaries = new[] {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild" "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
   };

   app.MapGet("/weatherforecast", () => {
      var forecast = Enumerable.Range(1, 5).(index =>
         new WeatherForecast
         (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
         ))
        .ToArray();
      return forecast;
   })
   .WithName("GetWeatherForecast")
   .WithOpenApi();
   ```

### Creating Models

Create folder **Models** in project directory and add Entity file<br>
Example :

```
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
```

```
public class Comment {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public int? StockId { get; set; } //* Navigation Property
    public Stock? Stock { get; set; }
}
```

### ORM with Entity Framework

- Install Entity Framework<br>
  `dotnet add package Microsoft.EntityFrameworkCore.Design`<br>
  `dotnet add package Microsoft.EntityFrameworkCore.Sqlite`<br>
  `dotnet add package Microsoft.EntityFrameworkCore.Tools`<br>
  `dotnet tool install --global dotnet-ef`
- Create folder **Data** and add `ApplicationDbContext.cs` > Communicate to database

  ```
   using Microsoft.EntityFrameworkCore;

   public class ApplicationDbContex : DbContext {
   public ApplicationDbContex(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
      public DbSet<Stock> Stock { get; set; }
      public DbSet<Comment> Comment { set; get; }
   }
  ```

- Inject `DbContextOptions` from Program Builder Service

  ```
   builder.Services.AddEndpointsApiExplorer();
   builder.Services.AddSwaggerGen();
   builder.Services.AddDbContext<ApplicationDbContext>();

   var app = builder.Build();
  ```

- Run Migration<br>
  `dotnet ef migrations add Init`<br>
  `dotnet ef database update`

### Controller

- Create **Controller** folder and add `StockController.cs`<br>

  ```
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
        List<Stock> stocks = _context.Stocks.ToList();
        return Ok(stocks);
     }

     [HttpGet("{id}")] // Read data by Id
     public IActionResult GetById([FromRoute] int id) {
        var stock = _context.Stocks.Find(id);
        if (stock != null) {
           return Ok(stock);
        }
        return NotFound();
     }
  }
  ```

- Add `StockController` to Program.cs
  `builder.Services.AddControllers();` bfore builder build
  `app.MapControllers();` before app.run

- Run project `dotnet watch run`

### DTOs (Data Transfer Object)

Object untuk komunikasi antar layer<br>
Response Request format<br>
Example case in username and password<br>

- Create **Dtos** folder then create **Stock** and **Comment** folder inside **Dtos**
- Create class `StockDto.cs` inside Dtos/Stock

  ```
  public class StockDto {
    public int Id { get; set; }
    public string Symbol { set; get; } = string.Empty;
    public string Company { set; get; } = string.Empty;
    public decimal Purchase { get; set; }
    public decimal LastDiv { get; set; }
    public string Industries { get; set; } = string.Empty;
    public long MarketCap { get; set; }
    }
  ```

- Create **Mapper** folder and add `StockMapper.cs` : Mapper untuk memetakan antar object (Stock menjadi StockDto)<br>
  Bisa menggunakan Automapper
  ```
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
  ```
- Modify StockController

  ```
    [HttpGet]
    public IActionResult GetAll() {
        var stocks = _context.Stocks.ToList().Select(s => s.ToStockDto());
        return Ok(stocks);
    }


    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id) {
        var stock = _context.Stocks.Find(id);
        if (stock != null) {
            return Ok(stock.ToStockDto());
        }
        return NotFound();
    }

  ```

  Jalankan dan List Comment sudah tidak ada<br>
  Before :

  ```
  {
  "id": 1,
  "symbol": "TSLA",
  "company": "Tesla",
  "purchase": 100,
  "lastDiv": 1000000000,
  "industries": "Space Exploration",
  "marketCap": 1000000000,
  "comments": []
  }
  ```

  After :

  ```
  {
  "id": 1,
  "symbol": "TSLA",
  "company": "Tesla",
  "purchase": 100,
  "lastDiv": 1000000000,
  "industries": "Space Exploration",
  "marketCap": 1000000000
  }
  ```

# +

List and Detail End Point Concept
