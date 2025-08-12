using Microsoft.EntityFrameworkCore;

public class ProductService : IProductService {

    private readonly StoreDbContext _context;

    public ProductService(StoreDbContext context) {
        _context = context;
    }

    public async Task AddNewProductAsync(string name, decimal price, int stock) {
        Product newProduct = new Product { Name = name, Price = price, Stock = stock };
        _context.Products.Add(newProduct);
        await _context.SaveChangesAsync();
        Console.WriteLine($"Success add {name}");
    }

    public async Task DeleteProductAsync(int productId) {
        Product productToDelete = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        if (productToDelete != null) {
            Console.WriteLine($"Deleting : {productToDelete.Name}");
            _context.Products.Remove(productToDelete);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Success deleting {productToDelete.Name}.");
        } else {
            Console.WriteLine($"Can't find product with ID {productId}");
        }
    }

    public async Task ShowAllProductsAsync() {
        List<Product> allProduct = await _context.Products.ToListAsync();
        foreach (Product p in allProduct) {
            Console.WriteLine($"ID: {p.Id},\tName: {p.Name}, Price: {p.Price}, Stock: {p.Stock}");
        }
    }

    public async Task UpdateProductAsync(int productId, string newName, decimal newPrice, int newStock) {
        Console.WriteLine($"Updating {productId}");
        Product productToUpdate = await _context.Products.FirstOrDefaultAsync(product => product.Id == productId);
        if (productToUpdate != null) {
            productToUpdate.Name = newName;
            productToUpdate.Price = newPrice;
            productToUpdate.Stock = newStock;
            await _context.SaveChangesAsync();
            Console.WriteLine($"Success updating {productToUpdate.Name}");
        } else {
            Console.WriteLine($"Can't find product with ID {productId}");
        }
    }
}