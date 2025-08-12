using Microsoft.EntityFrameworkCore;

public class StoreDbContext : DbContext {

    // Property untuk table product
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlite("Data Source=OnlineStore.db"); // Use SQLite as db provider
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Beras Sania 5kg", Price = 68000, Stock = 50 },
            new Product { Id = 2, Name = "Minyak Goreng Bimoli 2L", Price = 35000, Stock = 80 },
            new Product { Id = 3, Name = "Indomie Goreng", Price = 3000, Stock = 500 },
            new Product { Id = 4, Name = "Telur Ayam 1kg", Price = 27000, Stock = 40 },
            new Product { Id = 5, Name = "Gula Pasir Gulaku 1kg", Price = 16000, Stock = 100 },
            new Product { Id = 6, Name = "Susu UHT Ultra Milk Coklat 1L", Price = 18000, Stock = 60 },
            new Product { Id = 7, Name = "Kopi Kapal Api Special 165g", Price = 15000, Stock = 120 },
            new Product { Id = 8, Name = "Teh Celup Sariwangi", Price = 8000, Stock = 150 },
            new Product { Id = 9, Name = "Saus Sambal ABC", Price = 12000, Stock = 90 },
            new Product { Id = 10, Name = "Kecap Manis Bango", Price = 22000, Stock = 70 },

            // Alat Tulis Kantor (ATK)
            new Product { Id = 11, Name = "Buku Tulis Sinar Dunia 38 Lbr", Price = 5000, Stock = 250 },
            new Product { Id = 12, Name = "Pensil 2B Faber-Castell", Price = 3000, Stock = 300 },
            new Product { Id = 13, Name = "Pulpen Pilot G-2", Price = 25000, Stock = 100 },
            new Product { Id = 14, Name = "Penghapus Staedtler", Price = 4000, Stock = 180 },
            new Product { Id = 15, Name = "Kertas HVS A4 70gr Rim", Price = 55000, Stock = 60 },
            new Product { Id = 16, Name = "Stabilo Boss Original", Price = 11000, Stock = 80 },
            new Product { Id = 17, Name = "Tipe-X Cair Kenko", Price = 7000, Stock = 120 },

            // Kebersihan & Rumah Tangga
            new Product { Id = 18, Name = "Sabun Mandi Lifebuoy", Price = 4500, Stock = 200 },
            new Product { Id = 19, Name = "Shampo Pantene Anti Dandruff", Price = 28000, Stock = 90 },
            new Product { Id = 20, Name = "Pasta Gigi Pepsodent", Price = 13000, Stock = 150 },
            new Product { Id = 21, Name = "Deterjen Rinso Anti Noda 1.8kg", Price = 45000, Stock = 70 },
            new Product { Id = 22, Name = "Pewangi Pakaian Molto", Price = 15000, Stock = 110 },
            new Product { Id = 23, Name = "Pembersih Lantai Super Pell", Price = 14000, Stock = 100 },
            new Product { Id = 24, Name = "Sabun Cuci Piring Sunlight", Price = 17000, Stock = 130 },
            new Product { Id = 25, Name = "Tisu Wajah Paseo", Price = 19000, Stock = 80 },
            new Product { Id = 26, Name = "Pengharum Ruangan Glade", Price = 25000, Stock = 60 },

            // Elektronik & Aksesoris
            new Product { Id = 27, Name = "Lampu LED Philips 10W", Price = 30000, Stock = 100 },
            new Product { Id = 28, Name = "Baterai ABC Alkaline AA", Price = 15000, Stock = 200 },
            new Product { Id = 29, Name = "Kabel USB-C Anker Powerline", Price = 150000, Stock = 40 },
            new Product { Id = 30, Name = "Mouse Logitech M185", Price = 180000, Stock = 30 },
            new Product { Id = 31, Name = "Headset Rexus F22", Price = 120000, Stock = 50 },
            new Product { Id = 32, Name = "Stop Kontak Uticon 4 Lubang", Price = 65000, Stock = 70 },

            // Minuman & Snack
            new Product { Id = 33, Name = "Coca-Cola Kaleng", Price = 7000, Stock = 300 },
            new Product { Id = 34, Name = "Air Mineral Aqua 600ml", Price = 3500, Stock = 400 },
            new Product { Id = 35, Name = "Chitato Sapi Panggang", Price = 11000, Stock = 250 },
            new Product { Id = 36, Name = "Oreo Original", Price = 9000, Stock = 180 },
            new Product { Id = 37, Name = "Silverqueen Cashew", Price = 14000, Stock = 150 },
            new Product { Id = 38, Name = "Pocari Sweat 500ml", Price = 8000, Stock = 220 },

            // Kesehatan
            new Product { Id = 39, Name = "Masker Medis Sensi", Price = 25000, Stock = 100 },
            new Product { Id = 40, Name = "Hand Sanitizer Dettol", Price = 20000, Stock = 120 },
            new Product { Id = 41, Name = "Tolak Angin Cair Sido Muncul", Price = 4000, Stock = 300 },
            new Product { Id = 42, Name = "Vitamin C IPI", Price = 10000, Stock = 200 },
            new Product { Id = 43, Name = "Plester Hansaplast", Price = 8000, Stock = 150 },

            // Lain-lain
            new Product { Id = 44, Name = "Payung Lipat", Price = 75000, Stock = 50 },
            new Product { Id = 45, Name = "Jas Hujan Axio", Price = 250000, Stock = 30 },
            new Product { Id = 46, Name = "Sandal Jepit Swallow", Price = 15000, Stock = 180 },
            new Product { Id = 47, Name = "Gembok Solid", Price = 50000, Stock = 80 },
            new Product { Id = 48, Name = "Lem Super Glue Alteco", Price = 7000, Stock = 140 },
            new Product { Id = 49, Name = "Korek Api Gas", Price = 3000, Stock = 400 },
            new Product { Id = 50, Name = "Gas LPG 3kg", Price = 22000, Stock = 60 }
        );
    } // dotnet ef migrations add AddProductSeedData - Migrasi Seed

}