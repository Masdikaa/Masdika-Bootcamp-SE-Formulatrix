using Microsoft.EntityFrameworkCore;

public class Program {

    public static async Task Main() {
        using (StoreDbContext context = new StoreDbContext()) {
            // await AddProductAsync(context, "Esse", 32_000, 32);
            await ShowProductAsync(context);
        }
    }

    public static async Task AddProductAsync(StoreDbContext context, string name, decimal price, int stock) {
        Console.WriteLine("Adding new product...");
        Product newProduct = new Product { Name = name, Price = price, Stock = stock };

        context.Products.Add(newProduct);
        await context.SaveChangesAsync();

        Console.WriteLine($"Success add {name}");
    }

    public static async Task ShowProductAsync(StoreDbContext context) {

        Console.WriteLine("Showing products");

        List<Product> allProduct = await context.Products.ToListAsync();
        foreach (var p in allProduct) {
            Console.WriteLine($"ID: {p.Id},\tName: {p.Name}, Price: {p.Price}, Stock: {p.Stock}");
        }

    }

    public static async Task UpdateProductAsync(
        StoreDbContext context,
        string productName,
        string newName,
        decimal newPrice,
        int newStock
    ) {

        Console.WriteLine($"Updating {productName}");
        var productToUpdate = await context.Products.FirstOrDefaultAsync(product => product.Name == productName);
        if (productToUpdate != null) {
            productToUpdate.Name = newName;
            productToUpdate.Price = newPrice;
            productToUpdate.Stock = newStock;
            await context.SaveChangesAsync();
            Console.WriteLine($"Success updating {productName}");
        } else {
            Console.WriteLine($"Can't find {productName}");
        }

    }

    public static async Task DeleteProductAsync(StoreDbContext context, string name) {
        Console.WriteLine("Deleting product...");
        Product? productToDelete = await context.Products.FirstOrDefaultAsync(product => product.Name == name);
        Thread.Sleep(100);
        if (productToDelete != null) {
            context.Products.Remove(productToDelete);
            await context.SaveChangesAsync();
            Console.WriteLine($"Success remove {name}");
        } else {
            Console.WriteLine($"Can't find {name}");
        }
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

    * Implement CRUD
*/