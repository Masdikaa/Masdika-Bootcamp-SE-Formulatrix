using System.Globalization;

namespace DelegateExample {
    /* 
        Variabel untuk menyimpan referensi terhadap sebuah method yang memungkinkan untuk 
        memanggil method tanpa tahu secara pasti method mana yang akan dipanggil

        Kontrak dalam delegate : Hanya bisa merujuk ke method yang 
        1. return type sama
        2. input parameter sama (jumlah & type)

        Ex:
        Delegate a dengan return type int dan 1 parameter int maka hanya bisa merujuk ke method
        yang juga memilki int return type dan 1 int parameter

        Keunggulan:
        - Fleksibel
        - Callback and Event Handling
        - Passing method as a parameter (Plug-in)
    */

    // Penempatan setara class di dalam namespace 
    delegate void ShowMessage(string msg); // Kontrak : menyimpan void method dengan 1 parameter string
    delegate double ConvertDelegate(double n); // Menyimpan double return method dengan 1 parameter double
    delegate void SomeMulticastDelegate();
    delegate TResult CurrencyConverter<TArg, TResult>(TArg arg); // TResult -> Placeholder untuk return type, TArgs -> Placeholder untuk parameter

    // delegate<TInput, TOutput>

    delegate void D1();
    delegate void D2();


    class Program() {

        public static void Main() {

            BasicDelegate();
            PlugInDelegate();
            MulticastDelegate();
            GenericDelegate();
            FuncDelegate();
            ActionDelegate();
            DelegateComatibility();

        }

        public static void BasicDelegate() {
            // Invoke / memanggil delegate
            ShowMessage statusMessageHandler = DangerMessage; // DangerMessage static class

            statusMessageHandler("You are in danger");
            statusMessageHandler.Invoke("Stay away"); // Menggunakan method Invoke()

            SafeCondition safeCondition = new SafeCondition(); // Instansiasi object dari class SafeCondition
            statusMessageHandler = safeCondition.SafeMessage;
            statusMessageHandler.Invoke("You are entering safe zone");
        }

        public static void DangerMessage(string message) {
            Console.WriteLine($"DANGER - {message}");
        }

        public static void PlugInDelegate() {
            // Plug-in Delegate
            // Delegate sebagai parameter dalam method
            ConvertDelegate celciusToFahrenheitHandler = TemperatureConversion.CelciusToFahrenheit;
            ShowConvertionResult("Celcius to Fahrenheit", 98, celciusToFahrenheitHandler);
            // Method ShowConvertionResult akan memanggil method CelciusToFahrenheit dari static class TemperatureConversion
        }

        // Method untuk plug-in delegate
        public static void ShowConvertionResult(string msg, double temperature, ConvertDelegate converter) {
            double result = converter(temperature);
            Console.WriteLine(msg);
            Console.WriteLine($"Temperature 1 = {temperature}\nTemperature 2 = {result}");
        } // ShowConvertionResult adalah Higher-order Function karena menerima method sebagai argument melalui delegate

        public static void MulticastDelegate() {
            SomeMulticastDelegate multicast = SendEmail;
            multicast += SendSMS; // Menambahkan 2 method dalam 1 Delegate
            multicast.Invoke(); // Memanggil kedua method secara berurutan
        }

        public static void SendEmail() => Console.WriteLine("Sending Email...."); // Lambda expression

        public static void SendSMS() => Console.WriteLine("Sending SMS....");

        public static void GenericDelegate() {
            CurrencyConverter<double, string> currencyFormatter = FormatToRupiah;
            // double input, string output
            string formattedPrice = currencyFormatter(1_500_000);
            Console.WriteLine(formattedPrice);
        }

        // Method yang melakukan return dari double ke dalam format rupiah
        public static string FormatToRupiah(double amount) => amount.ToString("C", CultureInfo.CreateSpecificCulture("id-ID"));// TArgs untuk generic delegate
        public static int CountCharacters(string text) => text.Length; // TResult 

        // Fun and Action Delegate
        public static void FuncDelegate() {
            // Func -> melakukan return sebuah nilai
            // Action -> tidak melakukan return (void)
            // Func <TOutput> -> tidak ada parameter -> memiliki return TOutput
            // Func <TInput, TOutput> -> satu parameter(TInput), dan satu return(TOutput)
            // Func <TInput1, TInput2, TOutput>

            // Ex:
            // Jika menggunakan delegate -> public static int getStringLength(string text) => text.Length;
            Func<string, int> getStringLength = CountCharacters;
            int stringLength = getStringLength("Masdika");
            Console.WriteLine($"Length : {stringLength}"); // Ouput 7
        }

        public static void ActionDelegate() {
            // Action -> tidak ada return (void)
            Action<string> logger = LogMessage;
            logger.Invoke("Hello There!");
        }

        public static void LogMessage(string Message) {
            Console.WriteLine($"Message : {Message}");
        }

        public static void DelegateComatibility() {
            D1 delegate1 = Method;
            // D2 delegate2 = delegate1; Error | tidak bisa di ubah secara implisit
            D2 delegate2 = new D2(delegate1); // object D2 baru yang membungkus D1, bisa karena D1 memilki kontrak yang sama

            // 2 Jenis compatibility    Contravariance & Covariance
            // Contravariance | Parameter Compatibility
            // Jika parameter Method lebih general daripada parameter pada Delegate 
            StringAction sa = new StringAction(ActOnObject);
            sa("hello"); // Possible untuk dilakukan karena tipe data object lebih general daripada string

            // Covariance | Return type Compatibility
            // Jika return type method lebih spesifik daripada delegate
            ObjectRetriever obj = RetrieveString; // ObjectRetriever dapat menampung method RetrieveString karena return trype dari delegate lebih general
            Object result = obj;
        }

        public static void Method() {
            // Empty
        }

        public static void ActOnObject(object o) => Console.WriteLine(o); // Method dengan parameter object 
        delegate void StringAction(string s); // Delegate meminta string

        public static string RetrieveString() => "hello"; // Return spesifik string
        delegate object ObjectRetriever(); // Delegate bisa menampung method RetrieveString karena return type delegate lebih general

    }

    public class SafeCondition {

        public void SafeMessage(string message) {
            Console.WriteLine($"SAFE - {message}");
        }

    }

    public static class TemperatureConversion {
        public static double CelciusToFahrenheit(double celciusTemperature) {
            return (celciusTemperature * 9 / 5) + 32;
        }

        public static double FahrenheitToCelcius(double fahrenheitTemperature) {
            return (fahrenheitTemperature - 32) * 5 / 9;
        }

    }
}