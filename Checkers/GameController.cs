namespace Checkers;

using Checkers.Interfaces;
using Checkers.Models;
using Checkers.Enums;

public class GameController {
    private readonly IBoard _board;
    private readonly List<IPlayer> _players;
    private int _currentPlayerIndex = 0;
    private readonly Dictionary<IPlayer, List<IPiece>> _playerPieces;

    public GameController(IBoard board, IPlayer player1, IPlayer player2) {

        _board = board;
        _players = [player1, player2];
        // _players.Add(player1);
        // _players = new List<IPlayer> { player1, player2 };

        _playerPieces = new Dictionary<IPlayer, List<IPiece>>();
        _playerPieces[player1] = new List<IPiece>();
        _playerPieces[player2] = new List<IPiece>();

        InitializeBoard(_board);

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
        IPiece pieceToMove = _board[from.X, from.Y];
        if (pieceToMove == null) {
            Console.WriteLine($"Error: Empty piece in ({from.X}, {from.Y}).");
            return false;
        } else {
            Console.WriteLine($"Moving {pieceToMove.Color} from ({from.X},{from.Y}) to ({to.X},{to.Y})");
            MovePiece(pieceToMove, to);
            return true;
            // Switch Turn Here
        }
    }

    public List<Position> GetPossibleMoves(IPiece piece) {
        var validMoves = new List<Position>();

        if (piece == null) return validMoves;

        int forwardDirection = (piece.Color == PieceColor.BLACK) ? 1 : -1;
        int currentX = piece.Position.X;
        int currentY = piece.Position.Y;

        Position leftMove = new Position(currentX - 1, currentY + forwardDirection); // Depan kiri

        // bool inBoardLeft = leftMove.X >= 0 && leftMove.X < _board.Size && leftMove.Y >= 0 && leftMove.Y < _board.Size;
        // bool emptyForwardLeft = _board[leftMove.X, leftMove.Y] == null;

        Position rightMove = new Position(currentX + 1, currentY + forwardDirection); // Depan kanan

        // bool inBoardRight = rightMove.X >= 0 && rightMove.X < _board.Size && rightMove.Y >= 0 && rightMove.Y < _board.Size;
        // bool emptyForwardRight = _board[rightMove.X, rightMove.Y] == null;

        // if (inBoardLeft && emptyForwardLeft) validMoves.Add(leftMove);
        // if (inBoardRight && emptyForwardRight) validMoves.Add(rightMove);

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

        return validMoves;
    }

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

}

/*
    _board : piece dalam coordinate board, apakah null, operasi read/move akan berinteraksi dengan board
    _players : list dari player (2 player), get informasi player, mengatur giliran
    _currentPlayerIndex : penanda giliran, untuk SwitchTurn()
    _playerPieces : daftar piece dalam board, daripada scanning seluruh board
*/

