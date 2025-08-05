namespace Checkers;

using Checkers.Enums;
using Checkers.Interfaces;
using Checkers.Models;
using System.Threading;

public class Program {

    public static void Main() {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        IBoard board = new Board(8);
        IPlayer player1 = new Player { Color = PieceColor.BLACK, Name = "Player 1" };
        IPlayer player2 = new Player { Color = PieceColor.RED, Name = "Player 2" };

        GameController gc = new GameController(board, player1, player2);

        while (true) {
            DisplayBoard(board, null);

            IPlayer currentPlayer = gc.GetCurrentPlayer();
            Console.WriteLine($"{currentPlayer.Name} {currentPlayer.Color} Turn");

            if (gc.IsGameOver()) {
                break;
            }

            if (gc.IsInCaptureChain) {
                Console.WriteLine("Executing capture chain...");
                Thread.Sleep(700);

                IPiece pieceToContinue = gc.ChainingPiece;
                List<Position> chainMoves = gc.GetCaptureChain(pieceToContinue);

                Position startPos = pieceToContinue.Position;
                Position endPos = chainMoves[0];
                gc.HandleMove(startPos, endPos);

                continue;
            }

            var availableMoves = gc.GetAllValidMovesForPlayer(currentPlayer);
            if (availableMoves.Count == 0) {
                Console.WriteLine("No move avaible! You Lose 🫵🏻😹");
                break;
            }

            bool mustCapture = gc.HasForcedCaptures(currentPlayer);
            Dictionary<IPiece, List<Position>> movesToShow;

            if (mustCapture) {
                // Console.WriteLine("ATTENTION: You must make a capture!");
                movesToShow = new Dictionary<IPiece, List<Position>>();

                foreach (var moveEntry in availableMoves) {
                    IPiece piece = moveEntry.Key;
                    List<Position> captureDestinations = new List<Position>();

                    foreach (Position dest in moveEntry.Value) {
                        if (gc.IsCapture(piece.Position, dest)) {
                            captureDestinations.Add(dest);
                        }
                    }

                    if (captureDestinations.Count > 0) {
                        movesToShow[piece] = captureDestinations;
                    }
                }
            } else {
                movesToShow = availableMoves;
            }

            Console.WriteLine("Pieces to move:");
            int index = 1;
            var movablePieces = movesToShow.Keys.ToList();
            foreach (var piece in movablePieces) {
                Console.WriteLine($"{index++}. Piece in ({piece.Position.X}, {piece.Position.Y})");
            }

            IPiece selectedPiece;
            while (true) {
                Console.Write("Select the number of piece: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice)) {
                    if (choice >= 1 && choice <= movablePieces.Count) {
                        selectedPiece = movablePieces[choice - 1];
                        break;
                    }
                }
                Console.WriteLine($"Invalid input. Please enter a number between 1 and {movablePieces.Count}.");
            }
            List<Position> destinations = movesToShow[selectedPiece];

            // Drawboard
            DisplayBoard(board, destinations);
            Console.WriteLine($"{currentPlayer.Name} Turn");
            Console.WriteLine($"Selected piece at ({selectedPiece.Position.X}, {selectedPiece.Position.Y}).");

            Position selectedDestination;
            while (true) {
                Console.Write("Select the number of the target position: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int destChoice) && destChoice >= 1 && destChoice <= destinations.Count) {
                    selectedDestination = destinations[destChoice - 1];
                    break;
                } else {
                    Console.WriteLine("Invalid Input");
                }
            }

            gc.HandleMove(selectedPiece.Position, selectedDestination);
        }

        gc.EndGame();

    }

    static void DisplayBoard(IBoard board, List<Position> highlightedSquares) {

        string darkSquareBg = "\x1b[48;2;191;146;100m";     // #BF9264 background
        string validSquareBg = "\x1b[48;2;149;76;46m";      // BG Possible Moves #954C2E
        string validSquareFg = "\x1b[48;2;59;6;10m";        // #3B060A 
        string lightSquareBg = "\x1b[48;2;248;244;225m";    // #F8F4E1 background
        string blackPieceFg = "\x1b[38;2;0;0;0m";           // Black Pieces foreground
        string redPieceFg = "\x1b[38;2;255;0;0m";           // Red Pieces foreground
        string resetColor = "\x1b[0m";                      // Default

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
                    int moveIndex = highlightedSquares.IndexOf(currentPos) + 1;
                    Console.Write(validSquareFg);
                    Console.Write($" {moveIndex} ");
                } else {
                    IPiece selectedPiece = board[x, y];
                    if (selectedPiece == null) {
                        Console.Write("   ");
                    } else {
                        if (selectedPiece.Color == PieceColor.BLACK) {
                            Console.Write(blackPieceFg);
                            Console.Write(selectedPiece.PieceType == PieceType.KING ? " ✪ " : " ● ");
                        } else {
                            Console.Write(redPieceFg);
                            Console.Write(selectedPiece.PieceType == PieceType.KING ? " ✪ " : " ● ");
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

}

/*
    1,2 -> 2,3 Black
    2,5 -> 1,4 Red
    3,2 -> 4,3 Black
    1,4 -> 3,2 Red Capture (2,3)
*/