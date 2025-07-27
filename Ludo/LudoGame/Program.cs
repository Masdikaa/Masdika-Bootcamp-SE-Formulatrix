public class Program {
    public static void Main() {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Defines 4 player
        Player player1 = new Player("Satu", Color.Blue);
        Player player2 = new Player("Dua", Color.Red);
        Player player3 = new Player("Tiga", Color.Green);
        Player player4 = new Player("Empat", Color.Yellow);

        // Method call generate player
        GameController game = new GameController(player1, player2, player3, player4);

        Console.WriteLine($"Name : {player1.Name} - {player1.Color}");
        Console.WriteLine($"Name : {player2.Name} - {player2.Color}");
        Console.WriteLine($"Name : {player3.Name} - {player3.Color}");
        Console.WriteLine($"Name : {player4.Name} - {player4.Color}");

        Piece pieceP1 = player1.Pieces.FirstOrDefault(p => p.Id == 1);
        Console.WriteLine($"Piece ID: {pieceP1.Id}");
        Console.WriteLine($"Piece Color: {pieceP1.PieceColor}");
        Console.WriteLine($"Owned by: {pieceP1.PieceOwner.Name}");

    }
}

public class GameController {

    private List<IPlayer> _players = new List<IPlayer>();

    // Generating Player
    public GameController(IPlayer p1, IPlayer p2, IPlayer p3, IPlayer p4) {
        _players.Add(p1);
        _players.Add(p2);
        _players.Add(p3);
        _players.Add(p4);
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

    public Piece(int id, Color pieceColor, Player pieceOwner) {
        Id = id;
        PieceColor = pieceColor;
        PieceOwner = pieceOwner;
    }
}

public enum Color {
    Blue,
    Red,
    Green,
    Yellow
}