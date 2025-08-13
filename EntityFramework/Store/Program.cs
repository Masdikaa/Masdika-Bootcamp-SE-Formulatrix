using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class Program {

    public static async Task Main() {
        ServiceCollection services = new ServiceCollection();

        services.AddDbContext<StoreDbContext>(options => {
            options.UseSqlite("Data Source=OnlineStore.db");
        });

        services.AddScoped<IProductService, ProductService>();
        ServiceProvider serviceProvider = services.BuildServiceProvider();

        IProductService productService = serviceProvider.GetRequiredService<IProductService>();

        // await productService.AddNewProductAsync(name: "Galon Aqua", price: 18_000, stock: 98);
        await productService.ShowAllProductsAsync();

    }

}

/*
    * Simple SQLite CRUD with Entity Framework

    * Instalasi Package EF :
    *    - dotnet add package Microsoft.EntityFrameworkCore.Design
    *    - dotnet add package Microsoft.EntityFrameworkCore.Sqlite
    *    - dotnet add package Microsoft.EntityFrameworkCore.Tools

    * Buat Model/Entity -> Representasi dari dataabase

    * DBContext Configuration -> Koneksi ke database

    * Buat migrasi -> Instruksi terhadap database
    *    - dotnet tool install --global dotnet-ef
    *    - dotnet ef migrations add NamaMigrasi
    *    - dotnet ef database update

    * Service Layer untuk Implement CRUD -> Business Logic, daripada memanggil DbContext secara langsung

    dotnet add package Microsoft.Extensions.DependencyInjection
*/

/*
    ? Clean Architecture
      Separation of Concern untuk menghasilkan system yang Independent dan Testable

    ? The Dependency Rule
      Layer terluar hanya boleh mengenali layer di dalamnya dan tidak mengetahui detailnya

    ? Layer dalam clean architecture
    *  Domain Layer (The Core)
       Berisi enterprise-wide rules yang mencakup Entities, Value object, Repositories Interface
       Tidak memilki ketergantungan pada layer lain dan tidak mengetahui tentang DB, API, ataupun UI
    *  Application Layer (Use Case)
       Berisi application-specific rules yang mengoordinasikan dan mengarahkan aliran data untuk menjalankan sebuah use case/feature
       Ex : Create Product, Shopping Basket
       Layer mencakup Service / UseCase / Handlers, Data Transfer Object(DTOs), Interface untuk outer layer
       Layer ini hanya bergantung pada Domain Layer. Ia tidak tahu siapa yang memanggilnya (Web API atau Console App) dan tidak tahu bagaimana data disimpan atau bagaimana email dikirim. 
    *  Infrastructure Layer 
       Layer ini adalah tempat implementasi teknis dari kontrak kontrak yang didefinisikan di lapisan dalam
       Menyediakan implementasi konkret untuk semua hal yang berhubungan dengan dunia luar
       Layer ini bergantung pada Application dan Domain untuk mengetahui interface apa yang harus diimplementasikan
       Layer dalam tidak bergantung pada lapisan ini. Inilah yang memungkinkan untuk menukar teknologi dengan mudah
    *  Presentation Layer (Framework and Drivers)
       Bertanggung jawab untuk berinteraksi dengan pengguna atau sistem lain. Bisa berupa apa saja dari Web API hingga Desktop App
*/