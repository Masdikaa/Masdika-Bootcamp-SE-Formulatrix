using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { } // Akan diisi oleh builder service
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { set; get; }
}