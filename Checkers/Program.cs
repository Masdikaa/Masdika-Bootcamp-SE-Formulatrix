namespace Checkers;

using Checkers.Enums;
using Checkers.Interfaces;
using Checkers.Classes;
using Checkers.Classes.Models;
using Checkers.Views;

public class Program {

    public static void Main() {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        IBoard board = new Board(8);

        List<IPlayer> players = new List<IPlayer> {
            new Player { Color = PieceColor.BLACK, Name = "Player 1" },
            new Player { Color = PieceColor.RED, Name = "Player 2" }
        };

        List<IPiece> blackPiece = CreatePieceSet(PieceColor.BLACK, 12);
        List<IPiece> redPiece = CreatePieceSet(PieceColor.RED, 12);

        // IPiece blackPiece = new Piece();
        // IPiece redPiece = new Piece();

        GameController game = new GameController(board, players, blackPiece, redPiece);
        Display display = new Display(game);

        game.OnGameMessage += HandleGameMessage;
        display.StartGame();
    }

    static void HandleGameMessage(string message) {
        Console.WriteLine(message);
    }

    static List<IPiece> CreatePieceSet(PieceColor color, int count) {
        var pieces = new List<IPiece>();
        for (int i = 0; i < count; i++) {
            pieces.Add(new Piece { Color = color, PieceType = PieceType.NORMAL });
        }
        return pieces;
    }

}