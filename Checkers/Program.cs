namespace Checkers;

using Checkers.Enums;
using Checkers.Interfaces;
using Checkers.Classes;
using Checkers.Classes.Models;

public class Program {

    public static void Main() {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        IBoard board = new Board(8);
        IPlayer player1 = new Player { Color = PieceColor.BLACK, Name = "Player 1" };
        IPlayer player2 = new Player { Color = PieceColor.RED, Name = "Player 2" };

        GameController game = new GameController(board, player1, player2);

        game.OnGameMessage += HandleGameMessage;
        game.StartGame(DisplayBoard, SelectPiece, SelectDestination);

    }

    static void DisplayBoard(IBoard board, List<Position>? highlightedSquares) {

        string darkSquareBg = "\x1b[48;2;191;146;100m";     // #BF9264 background
        string validSquareBg = "\x1b[48;2;149;76;46m";      // BG Possible Moves #954C2E
        string validSquareFg = "\x1b[48;2;59;6;10m";        // #3B060A 
        string lightSquareBg = "\x1b[48;2;248;244;225m";    // #F8F4E1 background
        string blackPieceFg = "\x1b[38;2;0;0;0m";           // Black Pieces foreground
        string redPieceFg = "\x1b[38;2;255;0;0m";           // Red Pieces foreground
        string resetColor = "\x1b[0m";                      // Default
        string normalPiece = " ● ";
        string kingPiece = " ✪ ";

        Console.Clear();

        Console.Write("   ");
        for (int col = 0; col < board.Size; col++) {
            Console.Write($" {col} ");
        }
        Console.WriteLine("\n");

        for (int y = 0; y < board.Size; y++) {
            Console.Write($" {y} ");
            for (int x = 0; x < board.Size; x++) {
                Position currentPos = new Position(x, y);
                string bgColor;
                bool isHighlighted = highlightedSquares != null && highlightedSquares.Contains(currentPos);

                if (isHighlighted) {
                    bgColor = validSquareBg;
                } else {
                    bgColor = ((x + y) % 2 != 0) ? darkSquareBg : lightSquareBg;
                }

                Console.Write(bgColor);

                if (isHighlighted) {
                    int moveIndex = highlightedSquares!.IndexOf(currentPos) + 1;
                    Console.Write(validSquareFg);
                    Console.Write($" {moveIndex} ");
                } else {
                    IPiece? selectedPiece = board[x, y];
                    if (selectedPiece == null) {
                        Console.Write("   ");
                    } else {
                        if (selectedPiece.Color == PieceColor.BLACK) {
                            Console.Write(blackPieceFg);
                            Console.Write(selectedPiece.PieceType == PieceType.KING ? kingPiece : normalPiece);
                        } else {
                            Console.Write(redPieceFg);
                            Console.Write(selectedPiece.PieceType == PieceType.KING ? kingPiece : normalPiece);
                        }
                    }
                }
                Console.Write(resetColor);
            }
            Console.WriteLine($" {y} ");
        }

        Console.Write("\n   ");
        for (int col = 0; col < board.Size; col++) {
            Console.Write($" {col} ");
        }
        Console.WriteLine("\n");
    }

    static IPiece SelectPiece(Dictionary<IPiece, List<Position>> movesToShow) {
        Console.WriteLine("Pieces to move:");
        int index = 1;
        var movablePieces = movesToShow.Keys.ToList();
        foreach (var piece in movablePieces) {
            Console.WriteLine($"{index++}. Piece in ({piece.Position.X}, {piece.Position.Y})");
        }

        while (true) {
            Console.Write("Select the number of piece: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= movablePieces.Count) {
                return movablePieces[choice - 1];
            }
            Console.WriteLine($"Invalid input. Please enter a number between 1 and {movablePieces.Count}.");
        }
    }

    static Position SelectDestination(List<Position> destinations) {
        while (true) {
            Console.WriteLine("Select a numbered destination on the board: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= destinations.Count) {
                return destinations[choice - 1];
            }
            Console.WriteLine($"Invalid input. Please enter a number between 1 and {destinations.Count}.");
        }
    }

    static void HandleGameMessage(string message) {
        Console.WriteLine(message);
        Thread.Sleep(200);
    }

}

/*
    1,2 -> 2,3 Black
    2,5 -> 1,4 Red
    3,2 -> 4,3 Black
    1,4 -> 3,2 Red Capture (2,3)
*/