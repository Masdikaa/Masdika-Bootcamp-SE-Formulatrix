namespace Checkers;

using Checkers.Enums;
using Checkers.Interfaces;
using Checkers.Models;

public class Program {

    public static void Main() {

        IBoard board = new Board(8);
        IPlayer player1 = new Player { Color = PieceColor.BLACK, Name = "Player 1" };
        IPlayer player2 = new Player { Color = PieceColor.RED, Name = "Player 2" };

        GameController gc = new GameController(board, player1, player2);
        gc.Show();
        gc.ShowPlayerPieces();

        Console.WriteLine("Select piece to move");
        Console.Write("Select X : ");
        int x = Convert.ToInt32(Console.ReadLine());
        Console.Write("Select Y : ");
        int y = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine($"Move piece ({x},{y}) to ?");
        Console.Write("Select X : ");
        int xEnd = Convert.ToInt32(Console.ReadLine());
        Console.Write("Select Y : ");
        int yEnd = Convert.ToInt32(Console.ReadLine());

        Position startPosition = new Position(x, y);
        Position endPosition = new Position(xEnd, yEnd);

        bool moveSuccess = gc.HandleMove(startPosition, endPosition);

        // 3. Cek hasilnya
        if (moveSuccess) {
            Console.WriteLine($"\nMoved piece from ({x},{y}) to ({xEnd},{yEnd})!");
            gc.ShowPlayerPieces();
        } else {
            Console.WriteLine("\nFailed to move piece");
        }

    }

}