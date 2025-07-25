public class Program {
    public static void Main() {

        // Defines 4 player
        Player player1 = new Player("Satu", Color.Blue);
        Player player2 = new Player("Satu", Color.Red);
        Player player3 = new Player("Satu", Color.Green);
        Player player4 = new Player("Satu", Color.Yellow);

        // Method call generate player
        GameController game = new GameController(player1, player2, player3, player4);
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        int[,] board = new int[15, 15];

        // for (int i = 0; i < board.GetLength(0); i++) {
        //     for (int j = 0; j < board.GetLength(1); j++) {
        //         Console.Write($" ({i},{j}) ");
        //     }
        //     Console.WriteLine();
        // }

        for (int i = 0; i < board.GetLength(0); i++) {
            for (int j = 0; j < board.GetLength(1); j++) {
                if (i >= 0 && i <= 5 && j >= 0 && j <= 5) {
                    // 🔴 Base Red
                    Console.ForegroundColor = ConsoleColor.Red;
                } else if (i >= 0 && i <= 5 && j >= 9 && j <= 14) {
                    // 🟢 Base Green
                    Console.ForegroundColor = ConsoleColor.Green;
                } else if (i >= 9 && i <= 14 && j >= 0 && j <= 5) {
                    // 🔵 Base Blue
                    Console.ForegroundColor = ConsoleColor.Blue;
                } else if (i >= 9 && i <= 14 && j >= 9 && j <= 14) {
                    // 🟡 base Yellow
                    Console.ForegroundColor = ConsoleColor.Yellow;
                } else {
                    // ⚪ 
                    Console.ResetColor();
                }
                Console.Write($" ({i},{j}) ");
                // Console.Write(" 0 ");

            }
            Console.WriteLine();
        }

        Console.ResetColor();

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

    public Player(string name, Color color) {
        Name = name;
        Color = color;
    }
}

public enum Color {
    Blue,
    Red,
    Green,
    Yellow
}

public class Board {

}

public class Tile {

}

struct Position {
    int X;
    int Y;
}

public class Piece {
    // Piece punya warna 
    // Menempati lokasi pada board
}