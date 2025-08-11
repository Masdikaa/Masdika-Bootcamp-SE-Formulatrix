## Deskripsi

Simlating Online Store yang memilki method `CalculateFinalPrice(originalPrice, discountPercent)` yang memilki parameter

- Harga Awal
- Discount

dan akan mengembalikan harga akhir dengan ketentuan :

1. Discount > 100 : throw exception
2. Discount < 0 : throw exception
3. Jika FinalPrice < 0 : return = 0

## Step by step

Implementasi Red, Green, Refactor pattern

1. Red - Buat unit tesnya
   - Buat test bahwa `CalculateFinalPrice(100_000, 20)` harus return `80000`
   - Buat tes bahwa diskon > 100% atau < 0 harus lempar exception
   - Buat tes bahwa jika diskon membuat harga di bawah 0, return 0
2. Green - Implementasikan kode
   - Tulis kode yang membuat semua test lulus
3. Refactor
   - Memastikan kode rapi dan readable
   - Jalankan ulang unit test dan harus lolos tes
