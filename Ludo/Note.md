## Class Diagram Relation

1. Inheritance (`<|--`) <br>
   Hubungan pewarisan is-a untuk mewarisi property dan method dari class lain <br>
   Contohnya Class Animal dan Clas Dog, Dog is an Animal, Dog akan mewarisi Animal

   ```
   Animal <|-- Dog
   ```

2. Composition (`*--`) <br>
   Hubungan memiliki owns-a yang kuat, bagian yang lemah tidak akan ada tanpa yang kuat <br>
   Contohnya Class House dan Room, Room tidak akan ada tanpa House, House owns a Room

   ```
   House *-- Room
   ```

3. Aggregation (`o--`) <br>
   Hubungan memiliki has-a yang lemah, class lemah yang bisa berdiri secara independent namun class yang kuat dapat memilki yang lemah <br>
   Contohnya Team and Player, Team memilki Player, namun jika Team hilang Player akan tetap ada

   ```
   Team o-- Player
   ```

4. Association (`-->`) <br>
   Hubungan related-with yang paling umum, yang menunjukan object dari setiap class dapat saling berinteraksi, arah panah menunjukan arah hubungan <br>
   Contohnya Teacher and Student, Teacher mengetahui Student tetapi Student tidak harus mengetahui Teacher

   ```
   Teacher --> Student
   ```

5. Dependency (`..>`) <br>
   Hubungan "menggunakan" uses-a yang lemah dan sementara. Satu kelas bergantung pada kelas lain (misalnya, sebagai parameter method), tetapi tidak menyimpannya sebagai properti

   ```
   Order ..> PaymentService
   ```

6. Realization (`<|..`) <br>
   Hubungan sebuah class mengimplementasikan sebuah interface. Garisnya putus-putus dengan panah pewarisan. Panah menunjuk ke interface

   ```
   class ILoggable {
    <<interface>>
   }

   ILoggable <|.. FileLogger
   ```

7. Link (`--` or `..`) <br>
   Solid Link : Menandakan hubungan yang lebih kuat dan permanen, seperti pada Asosiasi, Pewarisan, Komposisi, dan Agregasi <br>
   Dashed Link : Menandakan hubungan yang lebih lemah dan sementara, seperti pada Dependensi dan Realisasi

## Ludo Games Class Diagram

### Identifikasi Object

- Player : Minimal 2 Player dan Maksimal 4 Berbeda Warna
- Piece : Setiap Player memilki 4 Piece
- Board : Papan Permainan
- Tile : Lantai dalam papan untuk jalanya player
- Dice : Dadu untuk
- Color : Memilki 4 warna (Player, Piece, Tile)
- Turn : Giliran setiap player
- GameRules : Aturan
- GameState : Status pada game(Mulai, Berjalan, Berhenti)
- Base : Tile dalam board untuk wadah piece di awal game
- HomePath : Jalan dalam tile digunakan piece untuk sampai finish

### Class Candidate Identification

Yang akan menjadi class utama

- Player : Menyimpan nama, warna, dan daftar piece milik masing"
- Piece : Pemilik, lokasi pada tile, dan status (IsAtBase, IsAtHomePath, Finish). Pieces bisa bergerak atau kembali ke base
- Board : Menyimpan semua daftar tile. Memberikan StartTile, NextTile, HomePath
- Tile : Posisi safezone, piecesOnTile, colorPath
- Dice : Roll
- GameController : Menyimpan referensi dari, Player, Dice, Board, dan StateManagement

### Relasi Antar Kelas

- GameController memilki Board, Dice, dan StateManager, Player(lemah)
- Player memilki 4 Pieces
- Board terdiri dari banyak Tile (Komposisi)
- Piece berdiri di atas Tile (hubungan assosiasi)


Revisi Class Diagram 
- List on tiles
- Enum (Color)
- Event ( ex : OnGameStarted )
- Interfacing Board, Player, Dice
- Pieces and Color 
- Hapus GameStateManager
- NamingRule
- Method dibikin public
- Method game and rules dimasukin ke dalam GameController
- Tiles & Board --> List/Array to Coordinating Position