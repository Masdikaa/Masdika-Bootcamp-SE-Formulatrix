using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class Program {

    public static async Task Main() {
        ServiceCollection services = new ServiceCollection();

        services.AddDbContext<StoreDbContext>(options => {
            options.UseSqlite("Data Source=OnlineStore.db");
        });

        services.AddScoped<IProductService, ProductService>();
        ServiceProvider serviceProvider = services.BuildServiceProvider();

        IProductService productService = serviceProvider.GetRequiredService<IProductService>();

        // await productService.AddNewProductAsync(name: "Galon Aqua", price: 18_000, stock: 98);
        await productService.ShowAllProductsAsync();

    }

}

/*
    * Simple SQLite CRUD with Entity Framework

    * Instalasi Package EF :
    *    - dotnet add package Microsoft.EntityFrameworkCore.Design
    *    - dotnet add package Microsoft.EntityFrameworkCore.Sqlite

    * Buat model entity

    * DBContext Configuration

    * Buat migrasi
    *    - dotnet tool install --global dotnet-ef
    *    - dotnet ef migrations add NamaMigrasi
    *    - dotnet ef database update

    * Service Layer untuk Implement CRUD

    dotnet add package Microsoft.Extensions.DependencyInjection
*/