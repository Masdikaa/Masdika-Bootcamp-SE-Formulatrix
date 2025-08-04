namespace Checkers;

using Checkers.Enums;
using Checkers.Interfaces;
using Checkers.Models;

public class Program {

    public static void Main() {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        IBoard board = new Board(8);
        IPlayer player1 = new Player { Color = PieceColor.BLACK, Name = "Player 1" };
        IPlayer player2 = new Player { Color = PieceColor.RED, Name = "Player 2" };

        GameController gc = new GameController(board, player1, player2);

        while (true) {
            // 1. TAMPILKAN PAPAN PERMAINAN
            ShowBoard(board);

            // 2. Tampilkan informasi giliran
            IPlayer currentPlayer = gc.GetCurrentPlayer();
            Console.WriteLine($"{currentPlayer.Name} Turn");

            var availableMoves = gc.GetAllValidMovesForPlayer(currentPlayer);
            if (availableMoves.Count == 0) {
                Console.WriteLine("No move avaible! You Lose 🫵🏻😹");
                break;
            }

            bool mustCapture = gc.HasForcedCaptures(currentPlayer);
            Dictionary<IPiece, List<Position>> movesToShow;

            if (mustCapture) {
                Console.WriteLine("ATTENTION: You must make a capture!");
                movesToShow = new Dictionary<IPiece, List<Position>>();

                // Langkah 2: Saring 'availableMoves' dan hanya ambil yang merupakan capture.
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
                // Langkah 3: Jika tidak ada capture, tampilkan semua gerakan seperti biasa.
                movesToShow = availableMoves;
            }

            Console.WriteLine("Pieces to move:");
            int index = 1;
            var movablePieces = movesToShow.Keys.ToList();
            foreach (var piece in movablePieces) {
                Console.WriteLine($"{index++}. Piece in ({piece.Position.X}, {piece.Position.Y})");
            }

            Console.Write("Select the number of piece: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            IPiece selectedPiece = movablePieces[choice - 1];

            List<Position> destinations = movesToShow[selectedPiece];
            Console.WriteLine($"\nPiece in ({selectedPiece.Position.X}, {selectedPiece.Position.Y}) valid move:");
            index = 1;
            foreach (var pos in destinations) {
                Console.WriteLine($"{index++}. ({pos.X}, {pos.Y})");
            }

            Console.Write("Select target position: ");
            choice = Convert.ToInt32(Console.ReadLine());
            Position selectedDestination = destinations[choice - 1];

            // 3. Eksekusi gerakan
            gc.HandleMove(selectedPiece.Position, selectedDestination);
        }

    }

    static void ShowBoard(IBoard board) {
        string darkSquareBg = "\x1b[48;2;191;146;100m";     // #BF9264 background
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
                string bgColor = ((x + y) % 2 != 0) ? darkSquareBg : lightSquareBg;
                Console.Write(bgColor);
                IPiece piece = board[x, y];
                if (piece == null) {
                    Console.Write("   "); // Empty Piece
                } else {
                    if (piece.Color == PieceColor.BLACK) {
                        Console.Write(blackPieceFg);
                        // Char for king ✪
                        Console.Write(" ● ");
                    } else {
                        Console.Write(redPieceFg);
                        Console.Write(" ● ");
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