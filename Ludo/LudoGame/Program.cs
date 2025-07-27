public class Program {
    public static void Main() {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Defines 4 player
        Player player1 = new Player("Satu", Color.Blue);
        Player player2 = new Player("Dua", Color.Red);
        Player player3 = new Player("Tiga", Color.Green);
        Player player4 = new Player("Empat", Color.Yellow);

        IDice dice = new Dice();
        IBoard board = new Board();

        // Method call generate player
        GameController game = new GameController(player1, player2, player3, player4, dice, board);

        Console.WriteLine($"Name : {player1.Name} - {player1.Color}");
        Console.WriteLine($"Name : {player2.Name} - {player2.Color}");
        Console.WriteLine($"Name : {player3.Name} - {player3.Color}");
        Console.WriteLine($"Name : {player4.Name} - {player4.Color}");

        // Get Piece
        Piece pieceP1 = player1.Pieces.FirstOrDefault(p => p.Id == 1);
        Console.WriteLine($"Piece ID: {pieceP1.Id}");
        Console.WriteLine($"Piece Color: {pieceP1.PieceColor}");
        Console.WriteLine($"Owned by: {pieceP1.PieceOwner.Name}");

        // Printpiece
        if (pieceP1.Position.HasValue) {
            Console.WriteLine($"Piece Position {pieceP1.Id}: ({pieceP1.Position.Value.X}, {pieceP1.Position.Value.Y})");
        } else {
            Console.WriteLine($"Piece {pieceP1.Id} At Base");
        }

        // Move piece
        game.MovePiece(pieceP1, 13, 6);
        Console.WriteLine($" Piece Move {pieceP1.Id}: ({pieceP1.Position.Value.X}, {pieceP1.Position.Value.Y})");
        Console.WriteLine(pieceP1.Position); // Possible because override ToString

        // Get Dice / Player Turn
        Console.WriteLine("Giliran: " + game.GetCurrentPlayer().Name);
        int diceValue = game.RollDice();
        Console.WriteLine($"Dadu keluar: {diceValue}");

        SimulationBoard.DisplayBoard();

    }
}

public class GameController {

    private List<IPlayer> _players = new List<IPlayer>();
    private IDice _dice;
    private int _currentTurnIndex = 0;
    private IBoard _board;

    // Generating Player
    public GameController(IPlayer p1, IPlayer p2, IPlayer p3, IPlayer p4, IDice dice, IBoard board) {
        _players.Add(p1);
        _players.Add(p2);
        _players.Add(p3);
        _players.Add(p4);
        _dice = dice;
        _board = board;
    }

    public int RollDice() {
        return _dice.Roll();
    }

    public Player GetCurrentPlayer() {
        return _players[_currentTurnIndex] as Player;
    }

    public void NextTurn() {
        _currentTurnIndex = (_currentTurnIndex + 1) % _players.Count;
    }

    public bool MovePiece(Piece piece, int targetX, int targetY) {
        if (_board.IsBlocked(targetX, targetY))
            return false;

        // Tambahkan aturan lain: safezone, commonpath, dll.
        piece.Position = new Position(targetX, targetY);
        return true;
    }

}

public interface IPlayer {
    string Name { set; get; }
    Color Color { set; get; }
}

public class Player : IPlayer {
    public string Name { get; set; }
    public Color Color { get; set; }
    public List<Piece> Pieces { get; set; }

    public Player(string name, Color color) {
        Name = name;
        Color = color;
        Pieces = new List<Piece>();
        for (int i = 0; i < 4; i++) {
            Pieces.Add(new Piece(i, color, this));
        }
    }
}

public class Piece {
    public int Id { set; get; }
    public Color PieceColor { set; get; }
    public Player PieceOwner { set; get; }

    // public (int x, int y)? Position { get; set; }
    public Position? Position { get; set; }

    public Piece(int id, Color pieceColor, Player pieceOwner) {
        Id = id;
        PieceColor = pieceColor;
        PieceOwner = pieceOwner;
        Position = null; // AtBase
    }

    public bool IsAtBase => Position == null;

    public void SetPosition(int x, int y) {
        Position = new Position(x, y);
    }
}

public interface IDice {
    int Roll();
}

public class Dice : IDice {
    private Random _random;

    public Dice() {
        _random = new Random();
    }

    public int Roll() {
        return _random.Next(1, 7);
    }
}

public interface IBoard {
    int[,] Grid { get; }
    ZoneType GetZoneType(int x, int y);
    bool IsSafeZone(int x, int y);
    bool IsBlocked(int x, int y);
}

public class Board : IBoard {
    public int[,] Grid { get; } = new int[15, 15];
    // private Dictionary<(int x, int y), ZoneType> zoneMap;
    private Dictionary<Position, ZoneType> zoneMap;

    public Board() {
        zoneMap = new Dictionary<Position, ZoneType>();
        InitializeZones();
    }

    private void InitializeZones() {
        // === BASE ZONES ===
        for (int i = 0; i <= 5; i++) {
            for (int j = 0; j <= 5; j++)
                zoneMap[new Position(i, j)] = ZoneType.Base; // Red Base

            for (int j = 9; j <= 14; j++)
                zoneMap[new Position(i, j)] = ZoneType.Base; // Green Base
        }

        for (int i = 9; i <= 14; i++) {
            for (int j = 0; j <= 5; j++)
                zoneMap[new Position(i, j)] = ZoneType.Base;    // Blue Base

            for (int j = 9; j <= 14; j++)
                zoneMap[new Position(i, j)] = ZoneType.Base; // Yellow Base
        }

        zoneMap[new Position(6, 1)] = ZoneType.StartPoint;
        zoneMap[new Position(1, 8)] = ZoneType.StartPoint;
        zoneMap[new Position(8, 13)] = ZoneType.StartPoint;
        zoneMap[new Position(13, 6)] = ZoneType.StartPoint;

        // === HOME POINTS ===
        zoneMap[new Position(7, 6)] = ZoneType.HomePoint;
        zoneMap[new Position(6, 7)] = ZoneType.HomePoint;
        zoneMap[new Position(7, 8)] = ZoneType.HomePoint;
        zoneMap[new Position(8, 7)] = ZoneType.HomePoint;

        // === BLOCKED PATH ===
        zoneMap[new Position(7, 7)] = ZoneType.BlockedPath;
        zoneMap[new Position(6, 6)] = ZoneType.BlockedPath;
        zoneMap[new Position(6, 8)] = ZoneType.BlockedPath;
        zoneMap[new Position(8, 6)] = ZoneType.BlockedPath;
        zoneMap[new Position(8, 8)] = ZoneType.BlockedPath;

        // === SAFE ZONE (ekspisit) ===
        zoneMap[new Position(8, 2)] = ZoneType.SafeZone;
        zoneMap[new Position(2, 6)] = ZoneType.SafeZone;
        zoneMap[new Position(6, 12)] = ZoneType.SafeZone;
        zoneMap[new Position(12, 8)] = ZoneType.SafeZone;

        // === HOME PATH ===
        for (int j = 1; j <= 5; j++) zoneMap[new Position(7, j)] = ZoneType.HomePath;
        for (int i = 1; i <= 5; i++) zoneMap[new Position(i, 7)] = ZoneType.HomePath;
        for (int j = 9; j <= 13; j++) zoneMap[new Position(7, j)] = ZoneType.HomePath;
        for (int i = 9; i <= 13; i++) zoneMap[new Position(i, 7)] = ZoneType.HomePath;
    }

    public ZoneType GetZoneType(int x, int y) {
        if (zoneMap.TryGetValue(new Position(x, y), out var zone))
            return zone;
        return ZoneType.Empty;
    }

    public bool IsSafeZone(int x, int y) {
        var zone = GetZoneType(x, y);
        return zone == ZoneType.SafeZone || zone == ZoneType.StartPoint || zone == ZoneType.HomePath;
    }

    public bool IsBlocked(int x, int y) {
        return GetZoneType(x, y) == ZoneType.BlockedPath;
    }

}

public struct Position {
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y) {
        X = x;
        Y = y;
    }

    public override string ToString() {
        return $"({X}, {Y})";
    }

    public override bool Equals(object? obj) {
        if (obj is Position other)
            return X == other.X && Y == other.Y;
        return false;
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public enum ZoneType {
    Base,
    StartPoint,
    CommonPath,
    HomePath,
    HomePoint,
    SafeZone,
    BlockedPath,
    Empty
}

public enum Color {
    Blue,
    Red,
    Green,
    Yellow
}

// 1. Membuat 4 Player
// Setiap player memiliki nama dan warna (Color)
// Setiap player otomatis memiliki 4 buah Piece

// 2. Mengakses Informasi Player dan Pieces
// Mendapatkan nama player dan warnanya
// Mendapatkan ID dan posisi piece
// Mengecek apakah sebuah piece masih di base (IsAtBase)
// Mendapatkan siapa pemilik suatu piece

// 3. Memindahkan Piece
// Menggunakan GameController.MovePiece(piece, x, y)
// Validasi BlockedPath sebelum pindah
// Setelah berpindah, posisi Piece akan berubah

// 4. Simulasi Roll Dice
// Dadu menghasilkan angka dari 1 sampai 6
// Dice dapat digunakan melalui GameController.RollDice()

// 5. Menampilkan Giliran Pemain
// Fungsi GetCurrentPlayer() menampilkan pemain yang sedang jalan
// Fungsi NextTurn() mengganti giliran

// 6. Board dengan Zona-Zona
// Zona board dibagi berdasarkan ZoneType: Base, StartPoint, SafeZone, dll
// Bisa mengecek apakah sebuah koordinat adalah SafeZone atau BlockedPath

// 7. Menggunakan Struktur Data Efisien
// Mapping posisi ke zona dengan Dictionary<Position, ZoneType>
// Position menggunakan struct, efisien dan override Equals/GetHashCode dengan baik

