using System.ComponentModel.DataAnnotations;

public class Product() {
    public int Id { get; set; }
    public decimal? Price { get; set; }
    [Required]
    public string? Name { get; set; }
    public int? Stock { get; set; }
}