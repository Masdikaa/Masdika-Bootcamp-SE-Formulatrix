# **Debug**

Tindakan sistematis untuk menemukan dan memperbaiki **Bug** (Kesalahan/Cacat) <br>
Sebuah **Bug** adalah segala sesuatu yang menyebabkan program berjalan tidak sesuai dengan ekspektasi (Output salah, Crash, Strange Behavior)<br><br>

Nama "_debugging_" sendiri punya cerita unik yang sering dikaitkan dengan Grace Hopper, seorang pionir dalam dunia komputer. Pada tahun 1947, timnya menemukan
bahwa komputer Mark II Aiken Relay Calculator tidak berfungsi dengan benar karena ada seekor ngengat (dalam bahasa Inggris: bug) yang terjebak di dalam salah
satu relaynya. Mereka kemudian menempelkan ngengat tersebut di buku catatan dengan tulisan: "First actual case of bug being found." Sejak saat itu, istilah
"bug" digunakan untuk kesalahan program dan "debugging" untuk proses memperbaikinya.

## **Debugging Procedure**

1. **Memproduksi masalah** : Memastikan bahwa bisa secara konsisten terjadi lagi
2. **Isolasi sumber masalah** : Mempersempit lokasi dalam code dimana permasalahan kemungkinan besar berasal
3. **Analisa kesalahan** : Memahami penyebab terjadinya kesalahan. Logika salah ?, Nilai tidak sesuai ?
4. **Memperbaiki masalah** : Mengubah dan menyesuaikan code untuk mengatasi masalah tersebut
5. **Uji perbaikan** : Memastikan bahwa perbaikan benar benar mengatasi masalah dan tidak menimbulkan masalah baru

## **Debugging Rules**

Merupakan serangkaian prinsip dan best practice yang membantu untuk proses pencarian dan perbaikan bug secara sistematis dan logis<br>
Tujuannya adalah untuk menghindari kepanikan, tebakan acak, dan perubahan kode yang tidak terkontrol, yang seringkali justru
memperburuk masalah atau membuang-buang waktu. Aturan-aturan ini membentuk kerangka kerja mental untuk memecahkan masalah yang kompleks.

1. **Understand the System** <br>
   Sebelum mencoba untuk memperbaiki bagian kecil dari sebuah system, diperlukan pemahaman yang cukup tentang bagaimana system itu bekerja secara keseluruhan<br>
   Mengetahui alur data dari satu bagian ke bagian lain, komponen yang berinteraksi, adalah hal yang perlu diperhatikan<br>
   **Contoh** : Mencoba memperbaiki masalah pada sistem pembayaran tanpa memahami bagaimana status pesanan, inventaris barang, dan notifikasi email saling terhubung adalah resep untuk bencana. Perbaikan mungkin akan tercapai namun, akan muncul permasalahan baru<br>
   **Intinya** Jangan menjadi "montir" yang hanya tahu cara mengganti satu baut tanpa tahu fungsi mesin secara keseluruhan.

2. **Make it Fail** <br>
   Merupakan langkah pertama yang krusial, jika terdapat sebuah bug maka harus bisa memproduksi bug tersebut sesuka hati. Jika bug hanya muncul _kadang-kadang_ perubahan tidak akan pernah diketahui apakah disebabkan oleh sebuah perubahan yang ditujukan untuk memperbaikinya atau hanya sedang _beruntung_ <br>
   **Contoh** : Jika ada laporan "aplikasi crash saat menyimpan data", cari tahu langkah-langkah pastinya. Apakah terjadi saat data kosong? Saat ada karakter aneh? Saat koneksi internet lambat? Penting untuk Menemukan skenario pasti yang 100% menyebabkan crash. <br>
   **Intinya** Bug tidak akan bisa diperbaiki jika tidak bisa dilihat

3. **Quit Thinking and Look** <br>
   Reminder untuk melawan godaan terbesar dari programmer, **Berasumsi** dalam menanggapi suaru permasalahan seperti "_mungkin variable X yang salah_"<br>
   Aturan ini mengatakan: **Hentikan asumsi itu**. Gunakan **debugger**, tambahkan print atau log, dan lihat nilai sebenarnya dari variabel X.
   **Contoh** : Berasumsi bahwa sebuah fungsi selalu menerima angka positif. Setelah berjam-jam bingung, akhirnya memeriksa log dan menemukan bahwa fungsi tersebut ternyata menerima nilai -1, yang menyebabkan perhitungan jadi kacau <br>
   **Intinya** Data dan fakta dari eksekusi program lebih bisa dipercaya daripada asumsi terpintar sekalipun.

4. **Divide and Conquer** <br>
   Jika permasalahan terjadi dalam proses yang besar dan kompleks, memecah proses menjadi bagian bagian kecil dan melakukan pengujian pada setiap setiap bagian secara terisolasi agar dapat diketahui pada bagian mana letak kesalahanya <br>
   **Contoh** : Sebuah proses terdiri dari 10 langkah: A -> B -> C ... -> J. Hasil di J salah. Periksa output di langkah E (tengah). Jika output E sudah salah, berarti masalahnya ada di antara A-E. Jika output E benar, masalahnya ada di antara F-J. Setenga bagian pencarian sudah terpangkas. <br>
   **Intinya** Ubah satu masalah besar yang menakutkan menjadi beberapa masalah kecil yang mudah dikelola.

5. **Change One Thing at a Time** <br>
   Saat mencoba berbagai hipotesis perbaikan, lakukan satu perubahan saja pada satu waktu. Jika mengubah 5 hal sekaligus dan bug-nya hilang, Pelaku tidak tahu perubahan mana yang sebenarnya menjadi solusi. Jika bug-nya tetap ada, pelaku tidak tahu apakah salah satu perubahan Anda justru memperburuknya.<br>
   **Contoh** : Anda curiga masalahnya ada pada query database dan cara Anda memformat tanggal. Coba perbaiki query-nya dulu, lalu tes. Jika tidak berhasil, kembalikan query ke semula, lalu coba perbaiki format tanggalnya.
   **Intinya** Ini adalah metode ilmiah dasar. Kontrol variabel Anda untuk memahami sebab-akibat.

6. **Keep on Audit Trail** <br>
   Catat semua upaya yang sudah dicoba, Perubahan apa yang dibuat, Bagaimana hasilnya, Hipotesis yang terbukti salah<br>
   Dapat dilakukan sesimpel menggunakan notepad ataupun menggunakan version control seperti Git <br>
   Aturan ini bertujuan agar tidak mengulahi hal yang sama dalam proses memperbaiki suatu permasalahan <br>
   **Contoh** : Setelah 2 jam, Anda mungkin lupa apa saja 3 solusi pertama yang Anda coba. Dengan catatan, Anda bisa melihat kembali dan tidak akan mengulangi usaha yang sia-sia. <br>
   **Intinya** Jangan sampai Anda berputar-putar di lingkaran yang sama.

7. **Check the Plug** <br>
   Sebelum menyelam ke dalam kode yang rumit, periksa hal-hal yang paling dasar dan nyata. Apakah service-nya berjalan? Apakah koneksi database sudah benar? Apakah file konfigurasi sudah dimuat? Apakah Anda mengedit file yang benar? <br>
   **Contoh** : Anda menghabiskan satu jam mencari bug di algoritma kompleks, lalu sadar bahwa Anda lupa menjalankan server database lokal Anda.
   **Intinya** Seringkali masalahnya adalah kesalahan sepele yang kita abaikan karena kita terlalu fokus pada hal-hal rumit.

8. **Get a Fresh View** <br>
   Jika dirasa pikiran sudah berat seperti menahan air kencing yang justru akan berbahaya bagi Programmer saat menatap kode berjam-jam. Jangan sampai mengalami _Tunnel Vision_. Simpan project dan catatan, tekan tombol sleep dan coba untuk order Upsize Americano Ice<br>
   Jelaskan permasalahan kepada orang lain yang mungkin dapat membantu<br>
   **Intinya** Otak butuh istirahat, dan menjelaskan masalah pada orang lain akan memaksa untuk menstrukturkan pikiran secara logis.

9. **If You didn't Fix it, it ain't Fixed**
   Kadang-kadang, sebuah bug secara misterius "hilang" begitu saja. Anda me-restart program, melakukan compile ulang, dan bug-nya tidak muncul lagi. Jangan pernah percaya ini. Jika Anda tidak tahu mengapa bug itu hilang, berarti akar masalahnya masih ada dan akan kembali menghantui Anda nanti.
   **Contoh** : Sebuah race condition (bug terkait timing di program multithreading) mungkin tidak terjadi pada percobaan kedua, tapi itu hanya kebetulan. Masalah dasarnya belum teratasi <br>
   **Intinya** Perbaikan sejati datang dari pemahaman akan akar masalah, bukan dari keberuntungan.
