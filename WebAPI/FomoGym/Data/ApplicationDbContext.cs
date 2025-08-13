using FomoGym.Models;
using Microsoft.EntityFrameworkCore;

namespace FomoGym.Data;

public class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Staff> Staff { get; set; }
    public DbSet<Member> Members { get; set; }
}