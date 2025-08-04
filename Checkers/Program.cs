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

        // --- Game Loop Utama ---
        while (true) {
            Console.WriteLine("\n=============================================");

            IPlayer currentPlayer = gc.GetCurrentPlayer();
            Console.WriteLine($"Turn: {currentPlayer.Name}");

            var availableMoves = gc.GetAllValidMovesForPlayer(currentPlayer);

            // Jika tidak ada gerakan yang tersedia, permainan berakhir
            if (availableMoves.Count == 0) {
                Console.WriteLine("No valid moves");
                break;
            }

            // 2. Tampilkan daftar bidak yang bisa digerakkan
            Console.WriteLine("Piece to move:");
            int index = 1;
            // Membuat list sementara untuk memudahkan pemilihan berdasarkan nomor
            var movablePieces = availableMoves.Keys.ToList();
            foreach (var piece in movablePieces) {
                Console.WriteLine($"{index++}. Piece in ({piece.Position.X}, {piece.Position.Y})");
            }

            // 3. Minta pemain memilih bidak
            Console.Write("Choose the number of piece to move: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            IPiece selectedPiece = movablePieces[choice - 1];

            // 4. Tampilkan daftar tujuan yang valid untuk bidak yang dipilih
            List<Position> destinations = availableMoves[selectedPiece];
            Console.WriteLine($"\nPiece ({selectedPiece.Position.X}, {selectedPiece.Position.Y}) possible move:");
            index = 1;
            foreach (var pos in destinations) {
                Console.WriteLine($"{index++}. ({pos.X}, {pos.Y})");
            }

            // 5. Minta pemain memilih tujuan
            Console.Write("Select valid move: ");
            choice = Convert.ToInt32(Console.ReadLine());
            Position selectedDestination = destinations[choice - 1];

            // 6. Eksekusi gerakan
            gc.HandleMove(selectedPiece.Position, selectedDestination);

            // Tampilkan keadaan papan setelah bergerak (opsional)
            gc.ShowPlayerPieces();
        }

        // gc.Show();
        // gc.ShowPlayerPieces();

        // // Simulating move piece
        // Console.WriteLine("Select piece to move");
        // Console.Write("Select X : ");
        // int x = Convert.ToInt32(Console.ReadLine());
        // Console.Write("Select Y : ");
        // int y = Convert.ToInt32(Console.ReadLine());

        // Console.WriteLine($"Move piece ({x},{y}) to ?");
        // Console.Write("Select X : ");
        // int xEnd = Convert.ToInt32(Console.ReadLine());
        // Console.Write("Select Y : ");
        // int yEnd = Convert.ToInt32(Console.ReadLine());

        // Position startPosition = new Position(x, y);
        // Position endPosition = new Position(xEnd, yEnd);

        // bool moveSuccess = gc.HandleMove(startPosition, endPosition);

        // if (moveSuccess) {
        //     Console.WriteLine($"\nMoved piece from ({x},{y}) to ({xEnd},{yEnd})!");
        //     gc.ShowPlayerPieces();
        // } else {
        //     Console.WriteLine("\nFailed to move piece");
        // }

        // Board position
        // gc.ShowBoardPosition();
    }

}

/*
    1,2 -> 2,3 Black
    2,5 -> 1,4 Red
    3,2 -> 4,3 Black
    1,4 -> 3,2 Red Capture (2,3)
*/