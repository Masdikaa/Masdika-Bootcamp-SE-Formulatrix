namespace EventHandlerExample {
  /*
  Event
  Method yang dieksekusi secara otomatis sebagai respon terhadap suatu kejadian(event)

  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  +   Event                                               +    
  +   Menekan tombol bel                                  +
  +                                                       +
  +   Broadcaster                                         +
  +   Tombol bel yang ditekan                             +
  +                                                       +
  +   Subscriber                                          +                
  +   Tertarik dengan suara bel                           +
  +                                                       +                                                                        
  +   Event Handler                                       +
  +   Tindakan yang dilakukan setelah mendengar suara bel +
  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++

  - Broadcaster
    Class yang memilki delegate dan memicu kejadian

  - Subsriber
    Penerima notifikasi memutuskan kapan untuk mulai dan berhenti listening dengan 
    menambahkan (+=) atau menghapus (-=) method dari broadcaster
    Subscriber tidak mengetahui dan mengganggu subscriber lainya

  - Event digunakan untuk membungkus public delegate agar tidak bisa diinterferensi
    agar subscriber tidak bisa mengganggu subscriber lainya ataupun memicu event

  */

  // Define delegate 
  public delegate void ButtonClickHandler(string message);

  public class Button { // Class button sebagai publisher yang memilki event OnClick
    // Declaring Event
    public event ButtonClickHandler? OnClick;
    // [acces-modifier] event [delegate] [identifier];

    /*
      Kode didalam broadcaster memilki akses penuh dan PriceChanged hanya seperti delegate biasa,
      hanya broadcaster yang berhak memanggil invoke() -> OnClick?.Invoke(message);
    */

    /*
      Kode di luar broadcaster (Main/Class lainya) memilki akses yang terbatas dan hanya bisa melakukan 2 hal :
      - Subscribe : button.OnClick += HandleButtonClick;
      - Unsubscribe : button.OnClick -= HandleButtonClick;
    */

    // Method untuk trigger event
    public void Click() {
      Console.WriteLine("Button Clicked");
      OnClick?.Invoke("Button successfully clicked");
    }

  }

  public class Program {

    public static void Main() { // Main sebagai Subscriber dan mendaftarkan HandleButtonClick sebagai listener dari event OnClick

      // Object dari class button
      Button button = new Button();
      button.OnClick += HandleButtonClick; // Subscriber event OnClick kedalam HandleButtonClick

      button.Click(); // Trigger Event
      /*
        Mencetak "Button Clicked"
        Menjalankan OnClick?.Invoke("Button successfully clicked")
        OnClick memeriksa subscribernya dan HandleButtonClick sudah terdaftar, lalu OnClick memanggilnya
        Mengirim pesan "Button successfully clicked" sebagai argumen ke HandleButtonClick
        Method HandleButtonClick dieksekusi, menerima pesan/argument tersebut ke dalam parameter message
      */

      // Example from stock 
      Stock teslaStock = new Stock("TSLA");
      teslaStock.PriceChanged += Stock_PriceChanged; // Registering Stock_PriceChanged method

      Console.WriteLine("Update Price!");
      teslaStock.Price = 900.50m;

      Console.WriteLine("Update Price!");
      teslaStock.Price = 912.75m;

    }

    // Subscriber / Handler 
    static void HandleButtonClick(string message) {
      Console.WriteLine($"HANDLER : Receiving message from event \"{message}\"");
    }

    // Subscriber from Stock
    public static void Stock_PriceChanged(object? sender, PriceChangedEventArgs e) {
      // if (newPrice > oldPrice)
      // {
      //   Console.ForegroundColor = ConsoleColor.Green;
      //   Console.WriteLine($"Price increased from {oldPrice:C} to {newPrice:C}");
      // }
      // else
      // {
      //   Console.ForegroundColor = ConsoleColor.Red;
      //   Console.WriteLine($"Price decreased from {oldPrice:C} to {newPrice:C}");
      // }
      // Console.ResetColor();
      if (sender is Stock stock) {
        string message = $"Price for {stock} changed from {e.OldPrice:C} to {e.NewPrice:C}";

        // Memberi warna agar menarik
        if (e.NewPrice > e.OldPrice) Console.ForegroundColor = ConsoleColor.Green;
        else Console.ForegroundColor = ConsoleColor.Red;

        Console.WriteLine(message);
        Console.ResetColor();
      }
    }
  }
}