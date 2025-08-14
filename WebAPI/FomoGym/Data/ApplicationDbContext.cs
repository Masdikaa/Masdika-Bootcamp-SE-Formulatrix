using FomoGym.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FomoGym.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser> {

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Member> Members { get; set; }

}