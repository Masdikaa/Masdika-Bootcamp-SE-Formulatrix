# **Panduan Komprehensif Design Pattern**

## 1. Fondasi Design Pattern

**Design Pattern** merepresentasikan sebuah blueprint solusi yang teruji dan dapat digunakan kembali untuk menyelesaikan masalah-masalah yang umum terjadi dalam konteks desain perangkat lunak berorientasi objek. Ini adalah sebuah konsep strategis untuk mengatasi tantangan arsitektural.

**Manfaat Utama:**

- **Komunikasi Efektif:** Penggunaan kosakata yang sama memungkinkan para developer berkomunikasi secara efisien mengenai solusi arsitektural.
- **Keandalan Teknis:** Penerapan solusi yang telah terbukti dapat mengurangi risiko teknis yang mungkin timbul.
- **Kualitas Kode:** Dihasilkannya kode yang lebih fleksibel, dapat digunakan kembali (reusable), dan lebih mudah dipelihara (maintainable).

## 2. Prinsip-Prinsip Desain Fundamental

Penguasaan design pattern dimulai dari pemahaman dan praktik prinsip-prinsip inti berikut.

### a. Prinsip Encapsulation

Encapsulation adalah praktik membungkus data dan logika internal sebuah objek, dan hanya mengekspos fungsionalitas esensial melalui public interface. Tujuannya adalah mengurangi ketergantungan antar komponen.

```csharp
// Detail internal (misalnya, bagaimana saldo dihitung) disembunyikan.
public class BankAccount
{
    private decimal _balance;

    public void Deposit(decimal amount)
    {
        if (amount > 0) _balance += amount;
    }

    public decimal GetBalance()
    {
        return _balance;
    }
}
```

### b. Dependensi pada Abstraction

Kode sebaiknya memiliki dependensi pada abstraction (`interface` atau `abstract class`), bukan pada concrete implementation. Hal ini memberikan kebebasan untuk menukar implementasi di masa depan tanpa mengubah kode yang menggunakannya.

```csharp
// Dependensi sebaiknya pada `IMessageService`, bukan `EmailService` secara spesifik.
public interface IMessageService { void Send(string message); }
public class EmailService : IMessageService { public void Send(string message) { /* ... */ } }

public class Notifier
{
    private readonly IMessageService _service;
    // Berbagai layanan (Email, SMS, Push) dapat disuntikkan selama mengimplementasikan IMessageService.
    public Notifier(IMessageService service) { _service = service; }
}
```

### c. Composition over Inheritance

Pemberian kemampuan pada sebuah objek (composition) lebih diutamakan daripada pewarisan (inheritance). Composition menciptakan sistem yang lebih fleksibel dan menghindari hirarki kelas yang kaku.

```csharp
// Kemampuan terbang diberikan melalui composition, bukan inheritance.
public interface IFlyable { void Fly(); }
public class FlyingAbility : IFlyable { public void Fly() { /* ... */ } }

public class Duck
{
    private readonly IFlyable _flyable;
    // Objek Duck "memiliki" kemampuan, bukan "adalah turunan" dari sesuatu yang bisa terbang.
    public Duck() { _flyable = new FlyingAbility(); }
    public void PerformFly() { _flyable.Fly(); }
}
```

## 3. Tiga Kategori Utama Design Pattern

### a. Creational Patterns

Berfokus pada mekanisme object creation yang fleksibel.

- **Singleton:** Memastikan sebuah class hanya memiliki satu instance.
- **Factory Method:** Memungkinkan subclass mengubah tipe objek yang akan dibuat.
- **Abstract Factory:** Memfasilitasi pembuatan family of objects terkait.
- **Builder:** Memisahkan konstruksi objek kompleks dari representasinya.

**Usecase Aplikatif:**
Kebutuhan akan satu koneksi database atau satu manajer konfigurasi untuk seluruh aplikasi mendorong penggunaan **Singleton Pattern** untuk menciptakan satu titik akses global dan efisiensi sumber daya.

### b. Structural Patterns

Mengatur cara class dan objek digabungkan untuk membentuk struktur yang lebih besar.

- **Adapter:** Menyatukan interface yang tidak kompatibel.
- **Decorator:** Menambahkan fungsionalitas pada objek secara dinamis.
- **Facade:** Menyederhanakan interface dari sebuah sistem yang kompleks.
- **Composite:** Menyusun objek dalam struktur pohon.

**Usecase Aplikatif:**
Keperluan untuk beralih dari API pembayaran lama ke yang baru merupakan skenario untuk **mengimplementasikan Adapter Pattern**. Adapter ini "membungkus" API baru sehingga sisa aplikasi tidak perlu diubah, mengisolasi perubahan dan mempercepat migrasi.

### c. Behavioral Patterns

Mengelola algoritma dan distribusi tanggung jawab antar objek.

- **Observer:** Memungkinkan sebuah objek memberi notifikasi ke banyak objek lain tentang perubahannya.
- **Strategy:** Mendefinisikan sekelompok algoritma dan membuatnya dapat dipertukarkan.
- **Command:** Mengubah sebuah request menjadi objek mandiri.
- **Chain of Responsibility:** Meneruskan sebuah request melalui serangkaian handler.

**Usecase Aplikatif:**
Keinginan untuk menerapkan berbagai aturan diskon (persentase, potongan harga, dll.) di keranjang belanja menjadi dasar **penerapan Strategy Pattern**. Setiap aturan diskon diimplementasikan sebagai 'strategi' terpisah yang dapat diubah saat runtime.

## 4. Wawasan Profesional

### a. Kebijaksanaan dalam Penerapan Pattern

Penerapan pattern harus didasari oleh kebutuhan nyata untuk menyelesaikan masalah yang spesifik. Perlu dihindari adanya _over-engineering_, yaitu memaksakan penggunaan pattern pada solusi yang sebenarnya sederhana.

### b. Mengenal Anti-Patterns

Anti-pattern adalah solusi umum yang terbukti tidak efektif atau bahkan kontra-produktif. Mengenalinya sama pentingnya dengan mengetahui pattern itu sendiri. Contoh: `God Object` (kelas yang memiliki terlalu banyak tanggung jawab) atau `Spaghetti Code` (kode dengan alur yang sangat sulit dilacak).

## 5. Kesimpulan dan Langkah Lanjutan

Design pattern adalah toolkit konseptual yang esensial. Kunci penguasaan adalah melalui praktik, dengan secara aktif mengidentifikasi masalah dalam desain perangkat lunak dan mengevaluasi apakah sebuah pattern dapat menyelesaikannya secara elegan dan efisien.

## 6. Mendalami Creational Patterns

Creational Patterns menyediakan berbagai mekanisme penciptaan objek yang meningkatkan fleksibilitas dan penggunaan kembali kode. Fokus utamanya adalah untuk memisahkan (decouple) sistem dari cara objeknya dibuat, disusun, dan direpresentasikan. Sistem menjadi hanya perlu tahu tentang interface dari objek, bukan implementasi konkretnya.

---

### a. Singleton Pattern

**Tujuan:** Memastikan sebuah class hanya memiliki satu instance dan menyediakan satu titik akses global ke instance tersebut.

**Problem:** Diperlukan kontrol ketat terhadap sebuah resource yang sifatnya tunggal, seperti koneksi database, file logger, atau manajer konfigurasi aplikasi. Membuat banyak instance dari resource ini akan menyebabkan konflik, pemborosan sumber daya, dan state yang tidak konsisten.

**Solusi:** Pattern ini menyembunyikan constructor dari class tersebut dan mendefinisikan sebuah static method atau property yang mengembalikan satu-satunya instance yang ada. Instance ini dibuat saat pertama kali diakses.

**Contoh Implementasi C# (Thread-Safe):**
Implementasi modern dan aman menggunakan `Lazy<T>` untuk memastikan instance hanya dibuat sekali (lazily) bahkan dalam lingkungan multi-threaded.

```csharp
public sealed class ApplicationConfig
{
    // Menggunakan Lazy<T> untuk lazy initialization yang thread-safe.
    private static readonly Lazy<ApplicationConfig> _lazyInstance =
        new Lazy<ApplicationConfig>(() => new ApplicationConfig());

    // Properti konfigurasi
    public string AppName { get; private set; }
    public int MaxConnections { get; private set; }

    // Constructor dibuat private agar tidak bisa di-instantiate dari luar.
    private ApplicationConfig()
    {
        // Simulasi memuat konfigurasi dari file atau sumber lain.
        Console.WriteLine("Initializing ApplicationConfig instance...");
        AppName = "MyAwesomeApp";
        MaxConnections = 100;
    }

    // Titik akses global ke satu-satunya instance.
    public static ApplicationConfig Instance => _lazyInstance.Value;
}

// Cara Penggunaan:
// ApplicationConfig config = new ApplicationConfig(); // Error: Constructor tidak bisa diakses.
var config = ApplicationConfig.Instance;
Console.WriteLine($"App Name: {config.AppName}");
```

**Kelebihan:**
- **Akses Terkontrol:** Menjamin hanya ada satu instance yang bisa diakses secara global.
- **Lazy Initialization:** Instance baru dibuat saat benar-benar dibutuhkan, menghemat sumber daya jika tidak pernah digunakan.
- **State Global:** Menyediakan tempat terpusat untuk menyimpan state yang berlaku di seluruh aplikasi.

**Kekurangan:**
- **Pelanggaran Single Responsibility Principle:** Class Singleton bertanggung jawab atas logikanya sendiri sekaligus atas proses penciptaan dan siklus hidupnya.
- **Menyulitkan Unit Testing:** Sulit untuk membuat mock atau stub dari sebuah Singleton karena state-nya global dan persisten.
- **Potensi Anti-Pattern:** Jika digunakan secara berlebihan, dapat menyembunyikan dependensi dan membuat kode sulit dipelihara, mirip seperti global variable.

---

### b. Factory Method Pattern

**Tujuan:** Mendefinisikan sebuah interface untuk membuat sebuah objek, namun membiarkan subclass yang memutuskan class mana yang akan di-instantiate.

**Problem:** Sebuah class (misalnya, `DocumentManager`) perlu membuat objek (misalnya, `Document`), tetapi ia tidak bisa mengantisipasi tipe dokumen spesifik (seperti `PdfDocument` atau `WordDocument`) yang harus dibuat. Hard-coding kelas konkret akan membuat `DocumentManager` menjadi kaku dan sulit diperluas.

**Solusi:** `DocumentManager` (Creator) mendeklarasikan sebuah "factory method" abstrak yang harus diimplementasikan oleh subclass-nya. Subclass seperti `PdfDocumentManager` akan meng-override factory method ini untuk mengembalikan instance `PdfDocument`. Dengan demikian, `DocumentManager` hanya bekerja dengan interface `Document`, tanpa perlu tahu detail kelas konkretnya.

**Contoh Implementasi C#:**

```csharp
// 1. Product Interface
public interface IDocument
{
    void Open();
    void Close();
}

// 2. Concrete Products
public class PdfDocument : IDocument
{
    public void Open() => Console.WriteLine("Opening PDF document.");
    public void Close() => Console.WriteLine("Closing PDF document.");
}

public class WordDocument : IDocument
{
    public void Open() => Console.WriteLine("Opening Word document.");
    public void Close() => Console.WriteLine("Closing Word document.");
}

// 3. Creator (Abstract Class)
public abstract class DocumentCreator
{
    // Factory Method yang harus di-override oleh subclass
    public abstract IDocument CreateDocument();

    // Logika lain yang tidak peduli tipe dokumen
    public void NewDocument()
    {
        IDocument doc = CreateDocument();
        doc.Open();
    }
}

// 4. Concrete Creators
public class PdfDocumentCreator : DocumentCreator
{
    public override IDocument CreateDocument()
    {
        return new PdfDocument();
    }
}

public class WordDocumentCreator : DocumentCreator
{
    public override IDocument CreateDocument()
    {
        return new WordDocument();
    }
}

// Cara Penggunaan:
// Bergantung pada kebutuhan, kita bisa memilih creator yang sesuai.
DocumentCreator creator = new PdfDocumentCreator();
creator.NewDocument(); // Output: Opening PDF document.

creator = new WordDocumentCreator();
creator.NewDocument(); // Output: Opening Word document.
```

**Kelebihan:**
- **Menghindari Coupling Erat:** Kode client tidak terikat pada kelas produk konkret, meningkatkan fleksibilitas.
- **Mendukung Open/Closed Principle:** Mudah untuk menambahkan tipe produk baru tanpa mengubah kode creator yang sudah ada. Cukup buat kelas produk dan kelas creator baru.
- **Kode Lebih Terpusat:** Logika pembuatan objek terpusat di dalam factory method, membuat kode lebih rapi.

**Kekurangan:**
- **Kompleksitas Bertambah:** Memerlukan pembuatan hirarki kelas baru (creator dan subclass-nya), yang bisa jadi berlebihan untuk kasus sederhana.
- **Membutuhkan Subclass:** Implementasi pattern ini memaksa penggunaan subclass hanya untuk mengubah cara sebuah objek dibuat.

---

## 7. Mendalami Structural Patterns

Structural Patterns berfokus pada cara class dan objek disusun untuk membentuk struktur yang lebih besar namun tetap fleksibel dan efisien. Tujuannya adalah untuk menyederhanakan hubungan antar entitas dan memastikan bahwa perubahan pada satu bagian sistem tidak memerlukan perubahan di seluruh sistem.

---

### a. Adapter Pattern

**Tujuan:** Mengizinkan objek dengan interface yang tidak kompatibel untuk dapat bekerja sama.

**Problem:** Sebuah sistem yang sudah ada (client) didesain untuk bekerja dengan interface tertentu. Kemudian, muncul kebutuhan untuk mengintegrasikan sebuah komponen atau library baru (adaptee) yang fungsionalitasnya dibutuhkan, namun memiliki interface yang berbeda. Mengubah kode client secara keseluruhan untuk menyesuaikan dengan interface baru akan sangat mahal dan berisiko.

**Solusi:** Dibuatlah sebuah class perantara yang disebut "Adapter". Adapter ini mengimplementasikan interface yang diharapkan oleh client, namun di dalamnya ia "membungkus" (wraps) dan mendelegasikan panggilan ke objek adaptee, melakukan penerjemahan data atau format yang diperlukan.

**Contoh Implementasi C#:**
Sistem kita memiliki interface `ILogger`, namun kita ingin menggunakan library logging baru yang menyediakan class `ThirdPartyLogger` dengan metode yang berbeda.

```csharp
// 1. Interface yang diharapkan oleh sistem kita (Client)
public interface ILogger
{
    void Log(string message);
}

// 2. Class client yang sudah ada
public class AppService
{
    private readonly ILogger _logger;
    public AppService(ILogger logger) { _logger = logger; }
    public void DoWork() { _logger.Log("Work is being done."); }
}

// 3. Adaptee (Library baru yang tidak kompatibel)
public class ThirdPartyLogger
{
    public void WriteLogEntry(string entry)
    {
        Console.WriteLine($"ThirdPartyLogger writes: {entry}");
    }
}

// 4. Adapter
public class LoggerAdapter : ILogger
{
    private readonly ThirdPartyLogger _thirdPartyLogger;
    public LoggerAdapter(ThirdPartyLogger thirdPartyLogger)
    {
        _thirdPartyLogger = thirdPartyLogger;
    }

    // Menerjemahkan panggilan dari metode Log() ke WriteLogEntry()
    public void Log(string message)
    {
        _thirdPartyLogger.WriteLogEntry(message);
    }
}

// Cara Penggunaan:
// Kita bisa menyuntikkan library baru ke sistem kita melalui Adapter.
var newLogger = new ThirdPartyLogger();
var adapter = new LoggerAdapter(newLogger);
var app = new AppService(adapter);
app.DoWork(); // Output: ThirdPartyLogger writes: Work is being done.
```

**Kelebihan:**
- **Separation of Concerns:** Memisahkan logika client dari logika integrasi dengan library pihak ketiga.
- **Mendukung Open/Closed Principle:** Dapat mengintegrasikan komponen baru tanpa mengubah kode client yang sudah ada.
- **Meningkatkan Reusability:** Adapter yang sama dapat digunakan oleh beberapa client.

**Kekurangan:**
- **Menambah Kompleksitas:** Memperkenalkan satu lapisan class tambahan untuk setiap adaptasi yang dibutuhkan.

---

### b. Decorator Pattern

**Tujuan:** Menambahkan fungsionalitas atau tanggung jawab baru ke sebuah objek secara dinamis tanpa harus mengubah kode class aslinya.

**Problem:** Diperlukan cara untuk memperluas kemampuan sebuah objek, namun menggunakan inheritance (subclassing) tidak praktis. Mungkin karena ada terlalu banyak kombinasi fungsionalitas (menyebabkan ledakan jumlah subclass) atau karena fungsionalitas tersebut perlu ditambahkan dan dihapus saat runtime.

**Solusi:** Dibuatlah serangkaian class "Decorator" yang membungkus objek asli. Decorator ini memiliki interface yang sama dengan objek yang dibungkusnya. Ketika sebuah metode dipanggil, decorator akan menambahkan perilakunya sendiri, kemudian mendelegasikan panggilan ke objek yang dibungkusnya. Decorator bisa ditumpuk satu sama lain.

**Contoh Implementasi C#:**
Sebuah sistem notifikasi dasar hanya mengirim pesan ke konsol. Kita ingin menambahkan kemampuan untuk mengirim notifikasi via SMS dan Slack tanpa mengubah class notifikasi dasar.

```csharp
// 1. Component Interface
public interface INotifier
{
    void Send(string message);
}

// 2. Concrete Component
public class ConsoleNotifier : INotifier
{
    public void Send(string message)
    {
        Console.WriteLine($"CONSOLE: {message}");
    }
}

// 3. Base Decorator (Abstract)
public abstract class NotifierDecorator : INotifier
{
    protected readonly INotifier _wrappedNotifier;
    protected NotifierDecorator(INotifier notifier) { _wrappedNotifier = notifier; }

    // Mendelegasikan panggilan ke objek yang dibungkus
    public virtual void Send(string message)
    {
        _wrappedNotifier.Send(message);
    }
}

// 4. Concrete Decorators
public class SmsNotifierDecorator : NotifierDecorator
{
    public SmsNotifierDecorator(INotifier notifier) : base(notifier) { }

    public override void Send(string message)
    {
        base.Send(message); // Kirim notifikasi asli dulu
        Console.WriteLine($"SMS: {message}"); // Tambahkan fungsionalitas baru
    }
}

public class SlackNotifierDecorator : NotifierDecorator
{
    public SlackNotifierDecorator(INotifier notifier) : base(notifier) { }

    public override void Send(string message)
    {
        base.Send(message);
        Console.WriteLine($"SLACK: {message}");
    }
}

// Cara Penggunaan:
INotifier notifier = new ConsoleNotifier();

// Bungkus notifier dasar dengan decorator SMS
notifier = new SmsNotifierDecorator(notifier);

// Bungkus lagi dengan decorator Slack
notifier = new SlackNotifierDecorator(notifier);

notifier.Send("System will restart in 10 minutes.");
// Output:
// CONSOLE: System will restart in 10 minutes.
// SMS: System will restart in 10 minutes.
// SLACK: System will restart in 10 minutes.
```

**Kelebihan:**
- **Fleksibilitas Tinggi:** Fungsionalitas dapat ditambah dan dihapus saat runtime.
- **Menghindari Ledakan Subclass:** Alternatif yang jauh lebih baik daripada inheritance untuk menambahkan banyak variasi fitur.
- **Sesuai Single Responsibility Principle:** Setiap decorator hanya memiliki satu tanggung jawab tambahan.

**Kekurangan:**
- **Banyak Objek Kecil:** Dapat menghasilkan banyak class kecil yang terlihat mirip, membuat desain sulit dipahami.
- **Sulit Menghapus Wrapper:** Mungkin sulit untuk menghapus decorator tertentu dari tumpukan wrapper.
- **Urutan Decorator Penting:** Perilaku sistem bisa berubah drastis jika decorator tidak disusun dalam urutan yang benar.


## 8. Mendalami Behavioral Patterns
*(Penjelasan mendalam, contoh kasus, implementasi, pro & kontra akan ditambahkan di sini)*

