using Microsoft.EntityFrameworkCore;

public class StoreDbContext : DbContext {

    // Property untuk table product
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        // base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite("Data Source=OnlineStore.db"); // Use SQLite as db provider
    }

}