using Serilog;
using Serilog.Context;
public class Program {
    public static void Main() {
        Console.WriteLine("Serilog");

        //* Global configuration Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/myapp.log", rollingInterval: RollingInterval.Day)
            .Enrich.FromLogContext() // Enrichment
            .CreateLogger();

        var userName = "Masdika";
        var orderId = 123;
        var transactionId = Guid.NewGuid();

        Log.ForContext("Action", "UserLogin") //? Action konteks hanya pada 1 log menggunakan ForContext
          .Information($"Running app by : {userName}");

        //? Transaction ID dalam semua block using
        using (LogContext.PushProperty("TransactionId", transactionId)) {
            Log.Warning($"Connection lost when processing : {orderId}");

            try {
                throw new InvalidOperationException("Can't get product information.");
            } catch (Exception ex) {
                Log.Error(ex, "Processing Error");
            }
        }

        Log.Fatal("Can't connect to Database");
        Log.CloseAndFlush();

    }

}

/*
    * - Konfigurasi log dapat melalui appsettings.json yang Ini memungkinkan untuk mengubah level logging atau mengganti sinks tanpa harus mengompilasi ulang aplikasi
    * - Integrasi ILogger<T> dengan melakukan dependency injection
*/