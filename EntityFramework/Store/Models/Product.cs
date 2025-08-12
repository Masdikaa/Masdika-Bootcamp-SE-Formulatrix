using System.ComponentModel.DataAnnotations;

public class Product {
    public int Id { get; set; }
    public decimal Price { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty; // Best practice dari nullable
    public int Stock { get; set; }
}