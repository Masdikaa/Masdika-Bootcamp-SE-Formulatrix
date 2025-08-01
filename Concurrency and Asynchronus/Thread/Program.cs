namespace Thread;

using System;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Xml.Serialization;

public class Program() {

    static bool _done;
    static object _locker = new object(); // Object yang didedikasikan untuk membuat locking

    public static void Main() {

        // Membuat Thread
        Thread t = new Thread(WriteY);
        t.Name = "Write Y Thread"; // Memberikan nama pada thread

        t.Start(); // Eksekusi Thread
        Console.WriteLine("Thread State " + t.ThreadState);
        t.Join(); // Main akan mengunggu t selesai

        Console.WriteLine("\nIs t Running : " + t.IsAlive);
        Console.WriteLine("Thread State " + t.ThreadState);
        Console.WriteLine("Code running on thread : " + t.Name);

        for (int i = 0; i < 500; i++) Console.Write(" X "); // Akan dijalankan secara paralel pada core yang berbeda

        Console.WriteLine("\nThread State " + t.ThreadState);

        // Thread safety
        new Thread(Go).Start(); //Running go in new Thread
        Go(); // Running Go in Main Thread

        // Passing data to a Thread
        // Lamdha Expression 
        Thread passData = new Thread(() => Print("Hello from t!")); // Memanggil method print untuk mendapatkan data
        passData.Start();
        // Delegate ParameterizedThreadStart
        Thread parameterizedThreadStart = new Thread(PrintAsParameter); // Menjalankan method yang membutuhkan parameter
        parameterizedThreadStart.Start("Thread Message"); // Define PrintAsParameter param in start

        // Foreground task dan Background task
        // Foreground menjaga aplikasi tetap berjalan, thread secara default akan menjadi foreground task
        Thread worker = new Thread(DoWork);
        worker.IsBackground = true; // Mengubah thread menjadi background
        worker.Start();

    }

    static void WriteY() {
        for (int i = 0; i < 500; i++) {
            Console.Write(" Y ");
            Thread.Sleep(10); // Menunda thread dalam durasi tertentu
        }
    }

    static void Go() {
        lock (_locker) // Only one thread can enter this block at a time for _locker
        {
            if (!_done) { Console.WriteLine("Done"); _done = true; } // Mengubah value dari sebuah variabel
            // Akan berbahaya jika terdapat 2 thread yang secara bersamaan mengubah nilai dari _done
        }
    }

    static void Print(string message) => Console.WriteLine(message);

    static void PrintAsParameter(object messageObj) {
        // Memerlukan casting untuk mendapatkan tipe data asli
        string message = (string)messageObj;
        Console.WriteLine(message);
    }

    static void DoWork() {
        Console.WriteLine("DoWork");
    }



}

/*
    Dalam processor 1 core, thread akan dijalankan bergantian untuk memberikan ilusi paralel
    Dalam processor MultiCore, thread akan benar benar dijalankan secara paralel
*/