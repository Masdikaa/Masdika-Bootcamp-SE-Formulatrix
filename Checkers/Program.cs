namespace Checkers;

public class Program {

    public static void Main() {
        IBoard board = new Board(8);
        IPlayer player1 = new Player { Color = PieceColor.BLACK, Name = "Player 1" };
        IPlayer player2 = new Player { Color = PieceColor.RED, Name = "Player 2" };

        GameController gc = new GameController(board, player1, player2);
        gc.ShowPlayer();

        IPlayer a = new Player { Name = "Bagas", Color = PieceColor.BLACK };
        Console.WriteLine(a.Name + a.Color);


    }

}