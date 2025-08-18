# **Decorator**

Merupakan bagian dari Structural Design Pattern <br>
Digunakan untuk **menambahkan fungsionalitas baru** ke sebuah **objek** secara dinamis **tanpa mengubah struktur atau kode** dari kelas aslinya

Kasus Coffee Shop <br>

- Bayangkan memilki base object berupa Black Coffee yang memilki method `GetPrice()` dan `GetDescription()`

  ```
  var BlackCoffee blackCoffee = new BlackCoffee();
  blackCoffe.GetPrice();
  blackCoffe.GetDescription();
  ```

- Menambahkan fungsionalitas baru tanpa mengubah kode <br>
  Case jika seorang pelanggan ingin menambahkan susu dalam BlackCoffee (**Milk**) <br>

  - **Pelanggaran** <br>
    Membuka class `BlackCoffe` dan memodifiksai kode dengan menambahkan property `withMilk`, lalu mengubah `GetPrice()` dan `GetDescription` <br>
    Ini adalah pendekatan yang buruk karena akan melanggar **Open/Closed** principle dimana seharusnya sebuah class seharusnya **terbuka terhadap ekstensi** namun **tertutup terhadap modifikasi**. Seharusnya kode di dalam class yang sudah teruji tidak boleh diubah dan dimodifikasi. <br>
    Dengan memodifikasi class dengan menambahkan `withMilk` adalah sebuah **pelanggaran**. Jika kedepanya akan ada customer yang memesan `withSugar` maka kode akan dimodifikasi lagi.

  - **Menyebabkan Combinatorial Explosion** <br>
    Dengan terus menerus memodifiksai kode pada sebuah class akan menyebabkan **Ledakan Kombinasi** <br>
    Awalnya memang terlihat mudah dengan hanya menambahkan `withMilk` lalu datang anomali orang ngopi pake gula dan tambah lagi `withSugar`. <br>
    Dan secara tidak sadar yang terjadi adalah kode dalam method `GetPrice()` akan terus berubah

    ```csharp
    int Price = 5000;
    if (withMilk) Price = += 2000;
    if (withSugar) Price = += 1000;
    return Price;
    ```

    Belum lagi muncul anomali caramel coffe, dan psikopat kopi tambah matcha. Jika dilakukan terus menerus maka `GetPrice` akan dipenuhi dengan if statement yang bercabang cabang dan membuat asma lo kambuh.

    Ini juga menyebabkan pelanggaran terhadap **Single Responsibility Principle** dimana BlackCoffee yang harusnya cuman ngerti kopi hitam doang dituntut untuk mengerti susu gula dan karamel.

    Pendekatan diatas akan menyebabkan kode menjadi fragile dan tidak maintainable<br>
    Kode menjadi tidak fleksibel, bagaimana jika pelanggan ingin double susu ? <br>
    Dan akan sulit diwariskan yang menyebabkan **Class Explosion** jika menambahkan subclass pada setiap kombinasi

### **Contoh Kode Tanpa Decorator (`MonolithicCoffee`)**

Berikut adalah contoh bagaimana kelas kopi akan terlihat jika semua logika dicampur menjadi satu. Perhatikan bagaimana metode `GetPrice` dan `GetDescription` harus berisi banyak logika kondisional.

```csharp
public class MonolithicCoffee
{
    public bool WithMilk { get; set; }
    public bool WithSugar { get; set; }
    public bool WithCaramel { get; set; }

    public decimal GetPrice()
    {
        decimal price = 5000m;
        if (WithMilk) price += 2000m;
        if (WithSugar) price += 1000m;
        if (WithCaramel) price += 3000m;
        return price;
    }

    public string GetDescription()
    {
        string description = "Black Coffee";
        if (WithMilk) description += " + Milk";
        if (WithSugar) description += " + Sugar";
        if (WithCaramel) description += " + Caramel";
        return description;
    }
}
```

Ini adalah implementasi nyata dari masalah "Ledakan Kombinasi" dan pelanggaran SRP yang telah dijelaskan.

- **Decorator as Solution** <br>
  Inti dari solusi menggunakan Decorator adalah **Object Composition** dan **Delegation** <br>
  Membangun object akhir dengan cara membungkus object dasar dengan Decorator, di mana setiap lapisan menambahkan sedikit fungsionalitas lalu meneruskan "tugas" ke lapisan di bawahnya.

  1. Daripada memodifikasi class `BlackCoffe` secara terus menerus lebih baik untuk membuat class baru `MilkDecorator`. Dengan menerapkan hal ini berarti kode sudah terbuka terhadap ekstensi namun tertutup untuk modifikasi

  2. Daripada menambahkan banyak logika pada setiap penambahan atribut seperti if statement yang bercabang cabang, lebih baik kombinasi diatur dalam struktur object yang dibangun saat melakukan runtime. <br>
     Implementasi kode `new MilkDecorator(new SugarDecorator(new BlackCoffe()))` merupakan cara yang baik dalam menciptakan kombinasi. Logika harga dan deskripsi untuk susu ada pada `MilkDecorator` dan gula ada pada `SugarDecorator`. Mereka tidak akan saling mengganggu dan

  3. BlackCoffee tidak menjadi terlalu pintar dan menanggung semuanya. Prinsip ini ditegakkan dengan sempurna. Setiap class kini punya satu tanggung jawab yang jelas:
     - KopiBiasa: Hanya tahu cara membuat kopi dasar.
     - MilkDecorator: Hanya tahu cara menambahkan susu (harga dan deskripsi) ke objek kopi apapun yang dibungkusnya.
     - CaramelDecorator: Hanya tahu cara menambahkan karamel.

  Dengan pendekatan ini juga membuat kode menjadi lebih fleksibel dalam kasus ingin menambahkan 2 susu dalam kopi bisa dangan membungkusnya seperti `new MilkDecorator(new MilkDecorator(new BlackCoffe))`

  Problem terpecahkan dengan kalcer dan elegan

**Kesimpulan**

Decorator mengubah masalah dari "Bagaimana cara mengubah satu class untuk menangani semua kemungkinan?" menjadi "Bagaimana cara merakit sebuah objek dari potongan-potongan kecil yang masing-masing memiliki satu fungsi spesifik?".

Pendekatan ini menghasilkan sistem yang komponennya seperti balok-balok LEGO: setiap balok sederhana, tetapi bisa digabungkan secara dinamis untuk menciptakan struktur yang kompleks dan bervariasi.

**Aturan Fundamental Decorator**

Sebelum melihat kode, ada satu aturan emas yang harus selalu diingat: **Semua decorator (baik base maupun konkret) harus mengimplementasikan interface atau abstraksi yang sama dengan objek yang didekorasikannya.**

Dalam kasus ini, `BlackCoffee`, `CoffeeDecorator`, `MilkDecorator`, dan `SugarDecorator` semuanya harus mengimplementasikan `ICoffee`. Aturan inilah yang memastikan decorator dan objek asli bisa saling menggantikan (substitutable) dan bisa dibungkus secara berlapis.

---

**Code Implementation in CoffeShop Case** <br><br>
**ICoffe**

```csharp
public interface ICoffee
{
    decimal GetPrice();
    string GetDescription();
}
```

**BlackCoffee**

```csharp
public sealed class BlackCoffee : ICoffee
{
    public decimal GetPrice() => 5000m;
    public string GetDescription() => "Black Coffee";
}
```

**CoffeeDecorator** : Base Decorator

```csharp
public abstract class CoffeeDecorator : ICoffee
{
    protected readonly ICoffee Inner;
    protected CoffeeDecorator(ICoffee inner) => Inner = inner;

    public virtual decimal GetPrice() => Inner.GetPrice();
    public virtual string GetDescription() => Inner.GetDescription();
}
```

Menggunakan abstract class untuk mencegah instantiation secara langsung dan membuat class hanya sebagai **template**. Dengan menjadikannya abstract, dapat mencegah developer membuat instance `new CoffeeDecorator(...)` yang sebenarnya tidak berguna. Ini memaksa penggunaan decorator yang sesungguhnya seperti MilkDecorator

**MilkDecorator**

```csharp
public sealed class MilkDecorator : CoffeeDecorator
{
    public MilkDecorator(ICoffee inner) : base(inner) { }
    public override decimal GetPrice() => base.GetPrice() + 2000m;
    public override string GetDescription() => base.GetDescription() + " + Milk";
}
```

**SugarDecorator**

```csharp
public sealed class SugarDecorator : CoffeeDecorator
{
    public SugarDecorator(ICoffee inner) : base(inner) { }
    public override decimal GetPrice() => base.GetPrice() + 1000m;
    public override string GetDescription() => base.GetDescription() + " + Sugar";
}
```

**Main**

```csharp
var coffee = new MilkDecorator(new SugarDecorator(new BlackCoffee()));
Console.WriteLine(coffee.GetPrice());        // 8000
Console.WriteLine(coffee.GetDescription());  // "Black Coffee + Sugar + Milk"

// Catatan: Urutan pembungkusan (wrapping) sangat penting karena menentukan urutan eksekusi.
// Di sini, MilkDecorator adalah lapisan terluar. Saat GetDescription() dipanggil:
// 1. Eksekusi pemanggilan berjalan dari luar ke dalam (Milk -> Sugar -> BlackCoffee).
// 2. Hasil string kemudian dibangun dari dalam ke luar.
//    - BlackCoffee mengembalikan "Black Coffee".
//    - SugarDecorator menambahkan " + Sugar".
//    - MilkDecorator menambahkan " + Milk".
// Jika urutannya dibalik menjadi new SugarDecorator(new MilkDecorator(...)), hasilnya akan menjadi "Black Coffee + Milk + Sugar".
```

---

## **Implementasi pada Skenario Proyek Nyata**

Berikut adalah ulasan implementasi Decorator pada skenario proyek nyata, dengan menekankan pada 'pola urutan' yang telah dibahas.

### **Skenario: Service API untuk Data Pengguna**

Sebuah API perlu menyediakan data pengguna (user). Operasi ini melibatkan pemanggilan ke database yang bisa lambat.

**Komponen Inti (Base Component):**
Ini adalah logika bisnis murni. Tugasnya hanya satu: mengambil data dari database.

```csharp
// Kontrak/Interface
public interface IUserService
{
    User GetUser(int id);
}

// Implementasi konkret yang lambat
public class UserService : IUserService
{
    public User GetUser(int id)
    {
        // Simulasi panggilan lambat ke database
        Console.WriteLine($"DATABASE: Mengambil user {id}...");
        Thread.Sleep(2000);
        return new User { Id = id, Name = "John Doe" };
    }
}
```

### **Kebutuhan Fungsionalitas Tambahan**

1.  **Caching:** Untuk mempercepat respons pada panggilan berulang.
2.  **Logging:** Untuk mencatat setiap permintaan dan durasinya sebagai data audit.

### **Implementasi Decorator**

Dibuat dua decorator, di mana masing-masing mengimplementasikan `IUserService`.

**1. Caching Decorator:**

```csharp
public class CachingUserServiceDecorator : IUserService
{
    private readonly IUserService _innerService;
    private readonly Dictionary<int, User> _cache = new();

    public CachingUserServiceDecorator(IUserService inner) => _innerService = inner;

    public User GetUser(int id)
    {
        if (_cache.TryGetValue(id, out var user))
        {
            Console.WriteLine($"CACHE: Hit! Mengembalikan user {id} dari cache.");
            return user;
        }

        user = _innerService.GetUser(id);
        _cache[id] = user;
        Console.WriteLine($"CACHE: Miss! Menyimpan user {id} ke cache.");
        return user;
    }
}
```

**2. Logging Decorator:**

```csharp
public class LoggingUserServiceDecorator : IUserService
{
    private readonly IUserService _innerService;

    public LoggingUserServiceDecorator(IUserService inner) => _innerService = inner;

    public User GetUser(int id)
    {
        Console.WriteLine($"LOG: Memulai request untuk user {id}.");
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        var user = _innerService.GetUser(id);

        stopwatch.Stop();
        Console.WriteLine($"LOG: Request selesai dalam {stopwatch.ElapsedMilliseconds}ms.");
        return user;
    }
}
```

### **Merakit Semuanya dengan Pola Urutan yang Benar**

Di sinilah **Pola "Bawang" (Core to Cross-Cutting)** diterapkan. Urutan yang logis adalah `Logging(Caching(UserService))`.

```csharp
// Di file Program.cs atau di container Dependency Injection

// 1. Mulai dari lapisan terdalam: logika bisnis inti.
IUserService coreService = new UserService();

// 2. Bungkus dengan lapisan Caching.
IUserService withCache = new CachingUserServiceDecorator(coreService);

// 3. Bungkus dengan lapisan Logging.
IUserService finalService = new LoggingUserServiceDecorator(withCache);

// --- Client code hanya menggunakan finalService ---
Console.WriteLine("--- Panggilan Pertama ---");
finalService.GetUser(123);

Console.WriteLine("\n--- Panggilan Kedua ---");
finalService.GetUser(123);
```

### **Alur Eksekusi**

**Panggilan Pertama (Cache Miss):**

1.  `LoggingDecorator` (Luar): Mencatat "Memulai request..." -> Memanggil `CachingDecorator`.
2.  `CachingDecorator` (Tengah): Gagal menemukan di cache -> Memanggil `UserService`.
3.  `UserService` (Dalam): Mengambil data dari DB (lambat 2 detik) -> Mengembalikan data.
4.  `CachingDecorator` (Tengah): Menerima data, **menyimpannya ke cache** -> Mengembalikan data.
5.  `LoggingDecorator` (Luar): Menerima data, mencatat "Request selesai dalam ~2000ms".

**Panggilan Kedua (Cache Hit):**

1.  `LoggingDecorator` (Luar): Mencatat "Memulai request..." -> Memanggil `CachingDecorator`.
2.  `CachingDecorator` (Tengah): **Berhasil menemukan di cache!** -> Langsung mengembalikan data. Panggilan berhenti di sini.
3.  `LoggingDecorator` (Luar): Menerima data, mencatat "Request selesai dalam <5ms".

Dengan cara ini, berhasil dibangun sebuah service yang fungsionalitasnya kaya, di mana setiap tanggung jawab terisolasi di kelasnya sendiri, dan dirakit dengan urutan yang logis untuk mencapai efisiensi dan perilaku yang benar.

---

## **Perbandingan: Implementasi Tanpa Decorator**

Untuk memahami sepenuhnya kekuatan Decorator, sangat berguna untuk melihat bagaimana masalah yang sama akan diselesaikan tanpanya. Pendekatan umum adalah dengan menempatkan semua logika ke dalam satu kelas.

### **Contoh: Kelas `MonolithicUserService`**

Kelas ini akan menangani akses database, caching, dan logging sekaligus.

```csharp
public class MonolithicUserService : IUserService
{
    // Semua tanggung jawab ada di sini
    private readonly DatabaseContext _db;
    private readonly Dictionary<int, User> _cache = new();

    public MonolithicUserService(DatabaseContext db) => _db = db;

    public User GetUser(int id)
    {
        // --- Tanggung Jawab Logging (Mulai) ---
        Console.WriteLine($"LOG: Memulai request untuk user {id}.");
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        // --- Tanggung Jawab Caching (Pengecekan) ---
        if (_cache.TryGetValue(id, out var user))
        {
            Console.WriteLine($"CACHE: Hit! Mengembalikan user {id} dari cache.");
            // Logika logging akhir untuk kasus cache hit
            stopwatch.Stop();
            Console.WriteLine($"LOG: Request selesai dalam {stopwatch.ElapsedMilliseconds}ms.");
            return user;
        }

        // --- Tanggung Jawab Akses Database ---
        Console.WriteLine($"DATABASE: Mengambil user {id}...");
        Thread.Sleep(2000); // Simulasi latensi
        user = new User { Id = id, Name = "John Doe" }; // Ambil data

        // --- Tanggung Jawab Caching (Penyimpanan) ---
        _cache[id] = user;
        Console.WriteLine($"CACHE: Miss! Menyimpan user {id} ke cache.");

        // --- Tanggung Jawab Logging (Akhir) ---
        stopwatch.Stop();
        Console.WriteLine($"LOG: Request selesai dalam {stopwatch.ElapsedMilliseconds}ms.");

        return user;
    }
}
```

### **Masalah dengan Pendekatan Ini**

Sekilas, kode ini mungkin terlihat lebih sederhana karena hanya ada satu kelas. Namun, pendekatan ini memiliki kelemahan signifikan yang akan muncul seiring berkembangnya proyek:

1.  **Pelanggaran Single Responsibility Principle (SRP):** Kelas `MonolithicUserService` memiliki terlalu banyak tanggung jawab: logika bisnis (akses DB), logika caching, dan logika logging. Seharusnya, setiap kelas hanya memiliki satu alasan untuk berubah.

2.  **Pelanggaran Open/Closed Principle (OCP):** Kelas ini tertutup untuk ekstensi yang mudah. Jika ada kebutuhan untuk menambahkan fungsionalitas baru (misalnya, otorisasi sebelum mengambil data), maka kelas ini **harus dimodifikasi**. Hal ini meningkatkan risiko memasukkan bug ke dalam logika yang sudah ada.

3.  **Kaku dan Tidak Fleksibel:** Tidak mungkin untuk memiliki versi service yang hanya melakukan logging tanpa caching, atau sebaliknya, tanpa mengubah kode atau menambahkan `if` statement (feature flags) yang lebih rumit. Kombinasi fungsionalitas terikat secara permanen.

4.  **Sulit Diuji (Hard to Test):** Melakukan unit test untuk logika akses database secara terisolasi menjadi sulit, karena logika caching dan logging akan selalu ikut terpanggil. Sulit untuk melakukan mock pada satu bagian tanpa terpengaruh oleh bagian lainnya.

5.  **Kode Sulit Dibaca:** Metode `GetUser` menjadi panjang dan logikanya saling terkait. Seiring waktu, metode seperti ini cenderung menjadi lebih besar dan lebih sulit untuk dipelihara.

Perbandingan ini menunjukkan bahwa Decorator Pattern, meskipun pada awalnya memperkenalkan lebih banyak kelas, pada akhirnya menghasilkan desain yang jauh lebih bersih, fleksibel, dan dapat dipelihara dalam jangka panjang.

---

## **Ringkasan: Kapan dan Mengapa Menggunakan Decorator?**

Gunakan Decorator Pattern ketika:

1.  **Ingin Menambah Fungsionalitas Secara Dinamis:** Ketika ada kebutuhan untuk menambahkan tanggung jawab atau perilaku baru ke sebuah objek saat runtime tanpa memengaruhi objek lain dari kelas yang sama.

2.  **Sebagai Alternatif Fleksibel untuk Inheritance:** Ketika menggunakan subclassing (pewarisan) tidak praktis karena akan menghasilkan ledakan jumlah kelas (class explosion) untuk setiap kombinasi fungsionalitas.

3.  **Menjaga Kepatuhan pada Prinsip Open/Closed:** Ketika ada kebutuhan untuk memperluas fungsionalitas sebuah kelas tanpa harus memodifikasi kode sumbernya yang sudah ada dan teruji.

4.  **Memisahkan Tanggung Jawab (Separation of Concerns):** Ketika sebuah fungsionalitas tambahan (misalnya, logging, caching, authorization) dapat dianggap sebagai tanggung jawab terpisah yang tidak seharusnya berada di dalam kelas bisnis inti.

---

## **Ringkasan: Kelebihan dan Kekurangan**

### **Kelebihan (Advantages)**

- **Fleksibilitas Tinggi:** Fungsionalitas dapat ditambahkan atau bahkan dihapus (dengan tidak membungkusnya) saat runtime. Kombinasi fitur menjadi tidak terbatas.
- **Mendukung Prinsip Desain SOLID:** Secara alami mendukung **Open/Closed Principle** (menambah fitur tanpa modifikasi) dan **Single Responsibility Principle** (setiap decorator punya satu tugas spesifik).
- **Menghindari Ledakan Kelas:** Jauh lebih skalabel daripada inheritance untuk mengelola banyak variasi fitur.
- **Komposisi Objek:** Mendorong pembuatan objek yang kompleks dari objek-objek yang lebih kecil dan lebih sederhana, yang lebih mudah dipahami dan diuji secara terpisah.

### **Kekurangan (Disadvantages)**

- **Banyak Objek Kecil:** Dapat menyebabkan proyek memiliki banyak kelas kecil yang terlihat mirip (`MilkDecorator`, `SugarDecorator`, dll.), yang berpotensi membingungkan developer baru.
- **Kompleksitas Saat Instansiasi:** Membuat objek bisa menjadi rumit karena melibatkan rantai konstruktor yang panjang, misalnya `new A(new B(new C(...)))`. Dalam aplikasi besar, ini sering dikelola oleh Factory atau Builder Pattern.
- **Sulit Mengakses Komponen Asli:** Setelah objek dibungkus berlapis-lapis, sulit untuk mendapatkan kembali referensi ke objek komponen asli (paling dalam) tanpa membongkar lapisannya secara manual.
- **Urutan Pembungkusan Penting:** Seperti yang telah dibahas, urutan penyusunan decorator sangat penting dan dapat menyebabkan perilaku yang tidak terduga jika tidak dirancang dengan hati-hati.
