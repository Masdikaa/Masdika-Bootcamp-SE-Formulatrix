public interface IProductService {
    Task ShowAllProductsAsync();
    Task AddNewProductAsync(string name, decimal price, int stock);
    Task UpdateProductAsync(int productId, string newName, decimal newPrice, int newStock);
    Task DeleteProductAsync(int productId);
}