using System.Reflection;

namespace ExceptionHandling {
    /* Exception Handling
       Exception : situasi tidak terduga saat eksekusi program
       Try : kode yang berpotensi memilki Exception
       Catch : Handling Exception dalam Try block
       Finally : Cleanup code yang akan tetap dijalankan

    */
    public class Program {
        public static void Main() {

            BasicExceptionHandling();
            ExceptionFilter();
            UsingStatementAndIDisposable();
            ThrowingException();

        }

        public static void BasicExceptionHandling() {
            Console.WriteLine("=============================== Basic Exception Start ================================");
            int Calc(int x) => 10 / x; // Jika x diisi dengan 0 maka akan terjadi Exception

            // Try selalu diikuti oleh setidaknya 1 catch atau finally atau bahkan keduanya
            try {
                Console.Write("Input X : ");
                int x = Convert.ToInt32(Console.ReadLine());
                int y = Calc(x); // Tidak ada runtime error
                Console.WriteLine($"Y = {y}");
            } catch (DivideByZeroException ex) { // Menangkap spesifik exception Jika x = 0
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: x cannot be zero"); // Print exception
                Console.ResetColor();
                Console.WriteLine(ex);
            } catch (FormatException ex) { // Jika format x tidak benar
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: invalid value for x"); // Print exception
                Console.ResetColor();
                Console.WriteLine(ex);
            } catch (Exception ex) { // Menangkap semua posible error
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error"); // Print exception
                Console.ResetColor();
                Console.WriteLine(ex);
            }

            // Menggunakan exception handling tidak akan menghentikan program namun tetap akan eksekusi program hingga selesai
            Console.WriteLine("\nProgram completed.\n");
            Console.WriteLine("=============================== Basic Exception End ==================================");
        }

        public static void ExceptionFilter() {
            Console.WriteLine("=============================== Exception Filter Start ===============================");

            try {
                OrderProcess("Priority");
            } catch (Exception ex) when (ex.Message.Contains("Priority")) { // Akan dijalankan jika menerima error dan memilki pesan Priority
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("SPECIAL SERVICE : There was a problem with an Priority order! Contact the manager immediately!");
                Console.ResetColor();
            } catch (Exception ex) { // Mengangkap exception biasa 
                Console.WriteLine($"GENERAL SERVICE : There was a problem in order - {ex.Message}");
            }

            try {
                OrderProcess("Standard");
            } catch (Exception ex) when (ex.Message.Contains("Penting")) {
                Console.WriteLine("Blok ini tidak akan pernah dijalankan.");
            } catch (Exception ex) {
                Console.WriteLine($"GENERAL SERVICE : There was a problem in order - {ex.Message}");
            }

            Console.WriteLine("\nProgram completed.\n");
            Console.WriteLine("=============================== Exception Filter End =================================");
        }

        public static void OrderProcess(string orderType) {
            if (string.IsNullOrEmpty(orderType)) return;
            throw new Exception($"Error: Can't proceed {orderType} order! ");
        }

        public static void UsingStatementAndIDisposable() {
            Console.WriteLine("========================= UsingStatementAndIDisposable Start =========================");
            /*
                Banyak class yang handling unmanaged resource seperti Jaringan, File Handling, koneksi
                Dengan mengimplementasikan IDisposable interface yang hanya memilki 1 method Dispose()
                Keyword using dalam C# memastikan method Dispose() akan dipanggil dan di eksekusi
            */

            // using (StreamReader reader = File.OpenText("file.txt")) {
            //     // Memanggil obj reader 
            //     Console.WriteLine(reader.ReadToEnd());
            // } // Calling reader.Dispose()

            /* 
                Dari kode di atas yang sebenarnya terjadi adalah :
                StreamReader reader = File.OpenText("file.txt");
                try {
                    Console.WriteLine(reader.ReadToEnd());
                } finally {
                    if (reader != null) {
                        ((IDisposable)reader).Dispose();
                    }
                }

                Atau menggunakan deklarasi baru dari C# 8 
                if (File.Exists("file.txt")) {
                    using var reader = File.OpenText("file.txt"); // Tanpa blok {}
                    Console.WriteLine(reader.ReadLine());
                } // reader.Dispose()


            */
            if (File.Exists("file.txt")) {
                using var reader = File.OpenText("file.txt"); // Tanpa blok {}
                Console.WriteLine(reader.ReadLine());
            } // reader.Dispose()

            Console.WriteLine("\nProgram completed.\n");
            Console.WriteLine("========================= UsingStatementAndIDisposable End ===========================");
        }

        public static void ThrowingException() {
            Console.WriteLine("======================== Throw Exception Start ==========================");
            try {
                string word = null;
                DisplayWord(word);
            } catch (ArgumentNullException ex) {
                Console.WriteLine($"Exception : {ex.Message}");
            }

            try {
                var result = Foo();
                Console.WriteLine($"Foo = {result}");
            } catch (NotImplementedException ex) {
                Console.WriteLine($"Foo : Error - {ex.Message}");
            }

            try {
                string sentence = null;
                ProperCase(sentence);
            } catch (ArgumentException ex) {
                Console.WriteLine($"ProperCase : Error - {ex.Message}");
            }

            Console.WriteLine("\nProgram completed.\n");
            Console.WriteLine("========================= Throw Exception End ===========================");
        }

        public static void DisplayWord(string word) {
            if (word == null) // Jika parameter == null maka akan melempar exception 
                throw new ArgumentNullException(nameof(word), "Word cannot be null...");
            // melempar ArgumentNullException baru dengan pesan 
            Console.WriteLine(word);

            /*
                .NET 6+ Helper
                ArgumentNullException.ThrowIfNull(name);
                Console.WriteLine(name);
            */
        }

        public static string Foo() => throw new NotImplementedException(); // Expression body method

        // Jika value null, lempar exception. Jika tidak, lanjutkan pengecekan.
        public static string ProperCase(string value) => value == null ? throw new ArgumentException("Value cannot be null.") :
        value == "" ? "" :
        char.ToUpper(value[0]) + value.Substring(1);

    }
}