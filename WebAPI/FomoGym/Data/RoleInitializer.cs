using Microsoft.AspNetCore.Identity;

namespace FomoGym.Data;

public static class RoleInitializer {
    public static async Task InitializeAsync(IServiceProvider serviceProvider) {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roleNames = { "Admin", "User" };

        foreach (var roleName in roleNames) {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist) {
                // Buat role baru jika belum ada
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}