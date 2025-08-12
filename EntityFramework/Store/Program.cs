public class Program {

    public static void Main() {

        StoreDbContext context = new StoreDbContext();

        // AddProduct(context, "Sampoerna", 28_000, 300);

        // DeleteProduct(context, "Sampoerna");

        // UpdateProduct(
        //     context: context,
        //     productName: "Gudang Garam",
        //     newName: "Gudang Garam Surya",
        //     newStock: 150,
        //     newPrice: 27000
        // );

        ShowProduct(context);

    }

    public static void AddProduct(StoreDbContext context, string name, decimal price, int stock) {

        using (context) {
            Console.WriteLine("Adding new product...");
            Product newProduct = new Product { Name = name, Price = price, Stock = stock };

            context.Products.Add(newProduct);
            context.SaveChanges();

            Console.WriteLine($"Success add {name}");
        }

    }

    public static void ShowProduct(StoreDbContext context) {
        Console.WriteLine("Showing products");

        List<Product> allProduct = context.Products.ToList();
        foreach (var p in allProduct) {
            Console.WriteLine($"ID: {p.Id}, Name: {p.Name}, Price: {p.Price}, Stock: {p.Stock}");
        }

        // Console.WriteLine("\nFiltering Product:");
        // var cheapProducts = context.Products
        //     .Where(p => p.Price < 10000000)
        //     .ToList();

        // foreach (var p in cheapProducts) {
        //     Console.WriteLine($"- {p.Name} ({p.Price})");
        // }

    }

    public static void UpdateProduct(
        StoreDbContext context,
        string productName,
        string newName,
        decimal newPrice,
        int newStock
    ) {
        using (context) {
            Console.WriteLine($"Updating {productName}");
            var productToUpdate = context.Products.FirstOrDefault(product => product.Name == productName);
            if (productToUpdate != null) {
                productToUpdate.Name = newName;
                productToUpdate.Price = newPrice;
                productToUpdate.Stock = newStock;
                context.SaveChanges();
                Console.WriteLine($"Success updating {productName}");
            } else {
                Console.WriteLine($"Can't find {productName}");
            }
        }
    }

    public static void DeleteProduct(StoreDbContext context, string name) {
        using (context) {
            Console.WriteLine("Deleting product...");
            Product? productToDelete = context.Products.FirstOrDefault(product => product.Name == name);
            if (productToDelete != null) {
                context.Products.Remove(productToDelete);
                context.SaveChanges();
                Console.WriteLine($"Success remove {name}");
            } else {
                Console.WriteLine($"Can't find {name}");
            }
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