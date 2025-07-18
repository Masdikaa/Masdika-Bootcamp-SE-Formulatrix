// ‼️ ToDoToLearn 👌
namespace InOutDelegate {

    using System;

    public class Media {
        public string? Title { get; set; }
    }

    public class Audio : Media {
        public int DurationSeconds { get; set; }
    }

    class Program {
        // Contravariance (in)
        public static void PrintMediaTitle(Media m) {
            Console.WriteLine($"Processing Media Title: {m.Title}");
        }

        // Covariance (out)
        public static Audio GetAudioSample() {
            return new Audio { Title = "Sample Track.mp3", DurationSeconds = 180 };
        }

        public static void Main() {
            Console.WriteLine("--- Contoh Contravariance (in) ---");

            // `Action<in T>`: Tipe inputnya bersifat contravariant.
            // Delegate ini mengharapkan method yang menerima 'Audio' (spesifik).
            // Kita bisa menugaskannya method 'PrintMediaTitle' yang menerima 'Media' (umum).
            Action<Audio> processAudioAction = PrintMediaTitle; // <-- INI VALID

            // Saat dipanggil, objek 'Audio' yang spesifik dikirimkan.
            processAudioAction(new Audio { Title = "Podcast Episode 1" });

            Console.WriteLine("\nPenjelasan: Method yang bisa memproses Media apa pun, pasti bisa memproses Audio.");

            Console.WriteLine("\n" + new string('-', 50) + "\n");

            Console.WriteLine("--- Contoh Covariance (out) ---");

            // `Func<out TResult>`: Tipe outputnya bersifat covariant.
            // Delegate ini berjanji akan mengembalikan 'Media' (umum).
            // Kita bisa menugaskannya method 'GetAudioSample' yang mengembalikan 'Audio' (spesifik).
            Func<Media> createMediaFunc = GetAudioSample; // <-- INI VALID

            // Saat dipanggil, hasilnya adalah objek 'Audio' yang bisa disimpan dalam variabel 'Media'.
            Media myMedia = createMediaFunc();

            Console.WriteLine($"Created media with title: {myMedia.Title}");
            Console.WriteLine($"Is the result an Audio object? {myMedia is Audio}");

            Console.WriteLine("\nPenjelasan: Method yang mengembalikan Audio, telah memenuhi janji untuk mengembalikan sebuah Media.");
        }
    }
}