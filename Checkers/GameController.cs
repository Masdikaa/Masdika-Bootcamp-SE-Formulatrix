namespace Checkers;

using Checkers.Interfaces;
using Checkers.Models;
using Checkers.Enums;

public class GameController {
    private readonly IBoard _board;
    private readonly List<IPlayer> _players;
    private int _currentPlayerIndex = 0;
    private readonly Dictionary<IPlayer, List<IPiece>> _playerPieces;
    public bool IsInCaptureChain { get; private set; }
    public IPiece ChainingPiece { get; private set; }

    public GameController(IBoard board, IPlayer player1, IPlayer player2) {

        _board = board;
        _players = [player1, player2];

        _playerPieces = new Dictionary<IPlayer, List<IPiece>>();
        _playerPieces[player1] = new List<IPiece>();
        _playerPieces[player2] = new List<IPiece>();

        IsInCaptureChain = false;
        ChainingPiece = null;

        InitializeBoard(_board); // USE THIS
        // InitializeForChainTest(_board);
        // InitializeKingTest(_board);

        for (int y = 0; y < _board.Size; y++) { // Scanning 8x8 board
            for (int x = 0; x < _board.Size; x++) {

                IPiece piece = _board[x, y];

                if (piece != null) {
                    // Finding piece owner by color
                    IPlayer owner = (piece.Color == player1.Color) ? player1 : player2;
                    _playerPieces[owner].Add(piece); // Adding piece to owner
                }
            }
        }

    }

    public void InitializeBoard(IBoard board) {
        // Clearing all piece
        for (int y = 0; y < board.Size; y++)
            for (int x = 0; x < board.Size; x++)
                board[x, y] = null;


        // Initialize Black Piece Position
        for (int y = 0; y < (board.Size - 5); y++) { // 3 rows
            for (int x = 0; x < board.Size; x++) {
                if ((x + y) % 2 != 0) { // Black Piece
                    board[x, y] = new Piece {
                        Color = PieceColor.BLACK,
                        PieceType = PieceType.NORMAL,
                        Position = new Position(x, y),
                    };
                }
            }
        }

        // Initialize Red Piece Position
        for (int y = 5; y < board.Size; y++) {
            for (int x = 0; x < board.Size; x++) {
                if ((x + y) % 2 != 0) {
                    board[x, y] = new Piece {
                        Color = PieceColor.RED,
                        PieceType = PieceType.NORMAL,
                        Position = new Position(x, y)
                    };
                }
            }
        }
    }

    public void MovePiece(IPiece piece, Position to) {
        Position from = piece.Position;
        _board[from.X, from.Y] = null;
        _board[to.X, to.Y] = piece;
        piece.Position = to;
    }

    public bool HandleMove(Position from, Position to) {
        IsInCaptureChain = false;
        ChainingPiece = null;

        IPiece pieceToMove = _board[from.X, from.Y];
        if (pieceToMove == null || !CanMoveTo(pieceToMove, to)) {
            Console.WriteLine($"Error: Invalid move ({from.X}, {from.Y}).");
            return false;
        } else {

            bool wasCapture = IsCapture(from, to);

            if (wasCapture) {
                CapturePiece(from, to);
            }

            Console.WriteLine($"Moving {pieceToMove.Color} from ({from.X},{from.Y}) to ({to.X},{to.Y})");
            MovePiece(pieceToMove, to);
            PromoteIfNeeded(pieceToMove);

            if (wasCapture) {
                List<Position> chainMoves = GetCaptureChain(pieceToMove);
                if (chainMoves.Count > 0) {
                    IsInCaptureChain = true;
                    ChainingPiece = pieceToMove;
                } else {
                    SwitchTurn();
                }
            } else {
                SwitchTurn();
            }

            return true;
        }
    }

    public bool CanMoveTo(IPiece piece, Position to) { // Validate, Hiding detail in Hanle Move
        if (piece == null) return false;
        List<Position> possibleMoves = GetPossibleMoves(piece);
        return possibleMoves.Contains(to);
    }

    public List<Position> GetPossibleMoves(IPiece piece) {
        List<Position> validMoves = new List<Position>();

        if (piece == null) return validMoves;

        // Piece Direction
        List<int> directionsToCheck = new List<int>();
        if (piece.Color == PieceColor.BLACK) {
            directionsToCheck.Add(1);
        } else {
            directionsToCheck.Add(-1);
        }

        if (piece.PieceType == PieceType.KING) {
            directionsToCheck.Add(directionsToCheck[0] * -1);
        }

        // int forwardDirection = (piece.Color == PieceColor.BLACK) ? 1 : -1;
        int currentX = piece.Position.X;
        int currentY = piece.Position.Y;

        foreach (int direction in directionsToCheck) {

            Position leftMove = new Position(currentX - 1, currentY + direction); // Depan kiri
            Position rightMove = new Position(currentX + 1, currentY + direction); // Depan kanan

            if (
                leftMove.X >= 0 && leftMove.X < _board.Size &&
                leftMove.Y >= 0 && leftMove.Y < _board.Size &&
                _board[leftMove.X, leftMove.Y] == null
            ) {
                validMoves.Add(leftMove);
            }

            if (rightMove.X >= 0 && rightMove.X < _board.Size &&
                rightMove.Y >= 0 && rightMove.Y < _board.Size &&
                _board[rightMove.X, rightMove.Y] == null
            ) {
                validMoves.Add(rightMove);
            }

            // Capture kiri depan 
            Position leftCapturePos = new Position(currentX - 2, currentY + (2 * direction)); // Posisi mendarat
            Position leftMiddlePos = new Position(currentX - 1, currentY + direction); // Posisi bidak yang dicapture
            if (
                leftCapturePos.X >= 0 && leftCapturePos.X < _board.Size && // Apakah posisi mendarat dalam board
                leftCapturePos.Y >= 0 && leftCapturePos.Y < _board.Size &&
                leftMiddlePos.X >= 0 && leftMiddlePos.X < _board.Size &&   // Apakah posisi makanan didalam board
                leftMiddlePos.Y >= 0 && leftMiddlePos.Y < _board.Size &&
                _board[leftCapturePos.X, leftCapturePos.Y] == null
            ) {
                IPiece middlePiece = _board[leftMiddlePos.X, leftMiddlePos.Y]; // Ambil posisi dari piece yang akan dimakan
                if (middlePiece != null && middlePiece.Color != piece.Color) {
                    validMoves.Add(leftCapturePos); // Jika posisi terdapat piece dan warnanya berbeda maka tambahkan dalam valid move
                }
            }

            Position rightCapturePos = new Position(currentX + 2, currentY + (2 * direction));
            Position rightMiddlePos = new Position(currentX + 1, currentY + direction);

            if (
                rightCapturePos.X >= 0 && rightCapturePos.X < _board.Size &&
                rightCapturePos.Y >= 0 && rightCapturePos.Y < _board.Size &&
                rightMiddlePos.X >= 0 && rightMiddlePos.X < _board.Size &&
                rightMiddlePos.Y >= 0 && rightMiddlePos.Y < _board.Size &&
                _board[rightCapturePos.X, rightCapturePos.Y] == null
            ) {
                IPiece middlePiece = _board[rightMiddlePos.X, rightMiddlePos.Y];
                if (middlePiece != null && middlePiece.Color != piece.Color) {
                    validMoves.Add(rightCapturePos);
                }
            }
        }

        return validMoves;
    }

    public Dictionary<IPiece, List<Position>> GetAllValidMovesForPlayer(IPlayer player) {
        Dictionary<IPiece, List<Position>> allValidMoves = new Dictionary<IPiece, List<Position>>();

        List<IPiece> playerPieces = _playerPieces[player]; // get all pieceis from _playerPieces

        foreach (IPiece piece in playerPieces) {
            // Possible move for each piece
            List<Position> moves = GetPossibleMoves(piece);
            if (moves.Count > 0) {
                allValidMoves[piece] = moves;
            }
        }

        return allValidMoves;
    }

    public bool IsCapture(Position from, Position to) {
        int deltaX = Math.Abs(to.X - from.X); // Hitung selisih dari posisi bidak ke 
        int deltaY = Math.Abs(to.Y - from.Y);

        return deltaX == 2 && deltaY == 2;
    }

    public List<Position> GetCapturedPositions(Position from, Position to) {
        List<Position> capturedPositions = new List<Position>();
        if (IsCapture(from, to)) {
            int capturedX = (from.X + to.X) / 2;
            int capturedY = (from.Y + to.Y) / 2;
            capturedPositions.Add(new Position(capturedX, capturedY));
        }
        return capturedPositions;
    }

    public bool HasForcedCaptures(IPlayer player) {
        Dictionary<IPiece, List<Position>> allPlayerMoves = GetAllValidMovesForPlayer(player);
        foreach (var moveEntry in allPlayerMoves) {
            IPiece piece = moveEntry.Key;
            List<Position> destinations = moveEntry.Value;
            foreach (Position to in destinations) {
                if (IsCapture(piece.Position, to)) {
                    return true;
                }
            }
        }
        return false;
    }

    public List<Position> GetCaptureChain(IPiece piece) {
        List<Position> possibleMoves = GetPossibleMoves(piece);
        List<Position> captureChainMoves = new List<Position>();
        foreach (var move in possibleMoves) {
            if (IsCapture(piece.Position, move)) {
                captureChainMoves.Add(move);
            }
        }
        return captureChainMoves;
    }

    public void CapturePiece(Position from, Position to) {
        List<Position> capturedPosition = GetCapturedPositions(from, to);
        foreach (Position pos in capturedPosition) {
            IPiece capturedPiece = _board[pos.X, pos.Y];
            if (capturedPiece != null) {
                _board[pos.X, pos.Y] = null; // Remove piece from board
                IPlayer owner = _players.First(p => p.Color == capturedPiece.Color);
                _playerPieces[owner].Remove(capturedPiece);
                Console.WriteLine($"{capturedPiece.Color} Piece in ({pos.X},{pos.Y}) has been captured!");
            }
        }
    }

    public void PromoteIfNeeded(IPiece piece) {
        if (piece.PieceType == PieceType.KING) return;
        if (piece.Color == PieceColor.BLACK && piece.Position.Y == _board.Size - 1) {
            piece.PieceType = PieceType.KING;
            Console.WriteLine($"{piece.Color} piece in ({piece.Position.X},{piece.Position.Y}) has promoted to KING!");
        } else if (piece.Color == PieceColor.RED && piece.Position.Y == 0) {
            piece.PieceType = PieceType.KING;
            Console.WriteLine($"{piece.Color} piece in ({piece.Position.X},{piece.Position.Y}) has promoted to KING!");
        }
    }

    public IPlayer GetCurrentPlayer() { // current player
        return _players[_currentPlayerIndex];
    }

    public void SwitchTurn() {
        _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
    }

    public bool IsGameOver() {
        IPlayer currentPlayer = GetCurrentPlayer();
        var moves = GetAllValidMovesForPlayer(currentPlayer);
        return moves.Count == 0;
    }

    public void EndGame() {
        IPlayer loser = GetCurrentPlayer();
        IPlayer winner = _players.First(p => p != loser);

        Console.WriteLine("\n===========================================");
        Console.WriteLine("               GAME OVER!                  ");
        Console.WriteLine($"       {winner.Name} is the Winner        ");
        Console.WriteLine("===========================================");
    }

    //======================================================================================================================//

    public void Show() {
        Console.WriteLine("--- List Player ---");
        Console.WriteLine($"{_players[0].Name} is {_players[0].Color}");
        Console.WriteLine($"{_players[1].Name} is {_players[1].Color}");
        Console.WriteLine();
    }

    public void ShowPlayerPieces() {
        Console.WriteLine("--- List Piece per Player ---");

        foreach (var playerEntry in _playerPieces) {

            IPlayer player = playerEntry.Key;
            List<IPiece> pieces = playerEntry.Value;

            Console.WriteLine($"Player: {player.Name} ({player.Color}) - Pieces: {pieces.Count}");

            int i = 0;
            foreach (IPiece piece in pieces) {
                Console.WriteLine($"{++i}\tType: {piece.PieceType}, Position: ({piece.Position.X},{piece.Position.Y})");
            }

            Console.WriteLine();
        }
    }

    public void ShowBoardPosition() {
        for (int y = 0; y < _board.Size; y++) {
            for (int x = 0; x < _board.Size; x++) {
                Console.Write($" [{x},{y}] ");
                if (x < _board.Size - 1) {
                    Console.Write("+");
                }
            }
            Console.WriteLine();
        }
    }

    // Chain Capture Testing 
    public void InitializeForChainTest(IBoard board) {
        for (int y = 0; y < board.Size; y++)
            for (int x = 0; x < board.Size; x++)
                board[x, y] = null;

        board[2, 1] = new Piece { Color = PieceColor.RED, PieceType = PieceType.NORMAL, Position = new Position(2, 1) };
        board[4, 3] = new Piece { Color = PieceColor.RED, PieceType = PieceType.NORMAL, Position = new Position(4, 3) };
        board[6, 5] = new Piece { Color = PieceColor.RED, PieceType = PieceType.NORMAL, Position = new Position(6, 5) };
        board[1, 0] = new Piece { Color = PieceColor.BLACK, PieceType = PieceType.NORMAL, Position = new Position(1, 0) };
    }

    public void InitializeKingTest(IBoard board) {
        for (int y = 0; y < board.Size; y++)
            for (int x = 0; x < board.Size; x++)
                board[x, y] = null;

        board[0, 3] = new Piece { Color = PieceColor.RED, PieceType = PieceType.NORMAL, Position = new Position(0, 3) };
        board[7, 4] = new Piece { Color = PieceColor.BLACK, PieceType = PieceType.NORMAL, Position = new Position(7, 4) };
    }

}

/*
    _board : piece dalam coordinate board, apakah null, operasi read/move akan berinteraksi dengan board
    _players : list dari player (2 player), get informasi player, mengatur giliran
    _currentPlayerIndex : penanda giliran, untuk SwitchTurn()
    _playerPieces : daftar piece dalam board, daripada scanning seluruh board
*/

/*
    Capture brief
    Method :
    + IsCapture(Position from, Position to) bool -> Apakah gerakan dari posisi from ke to merupakan sebuah lompatan (capture)? boolean return
    + GetCapturedPositions(Position from, Position to) List<Position> -> Jika sebuah gerakan adalah capture, method ini bertugas menemukan posisi bidak lawan yang dilompati. Mengembalikan sebuah List<Position> yang berisi posisi dari semua bidak yang di-capture dalam satu lompatan itu
    + CapturePiece(Position from, Position to) -> Eksekusi capture dan menghapus bidak yang dicapture dari _board dan _playerPieces
    + HasForcedCaptures(IPlayer) bool -> Menerapkan aturan "wajib makan". Method ini memeriksa seluruh papan untuk menjawab pertanyaan: "Apakah pemain ini memiliki setidaknya satu gerakan capture yang bisa dilakukan?"
    + GetCaptureChain(Position from, Position to) List<Position> -> Menangani capture berantai (multi-jump). Jika setelah melakukan capture pertama, bidak tersebut mendarat di posisi di mana ia bisa melakukan capture lagi, method ini akan menemukan seluruh rangkaian lompatannya.

    Alur Implementasi :
    IsCapture(Position from, Position to)
    Modifikasi GetPossibleMoves(IPiece piece) untuk mendeteksi gerakan lompatan
    GetCapturedPositions(Position from, Position to) untuk mencari korban yang bisa dimakan 
    CapturePiece(...) dan Modifikasi HandleMove(...) HandleMove untuk memanggil CapturePiece dan menghapus bidak yang di capture
    HasForcedCaptures(IPlayer player) Menggunakan GetPossibleMoves yang sudah dimodifikasi untuk memeriksa semua bidak pemain. Dipanggil di setiap giliran jika hasilinya true
    GetCaptureChain(...) (Opsional/Tingkat Lanjut)
*/

/*
    Promote to King Brief
    + PromoteIfNeeded(IPiece) - untuk mengevaluasi piece yang akan menjadi king 
    update rules GetPossibleMoves untuk piece king
    update simbol untuk king
*/