namespace Checkers.Classes;

using Checkers.Interfaces;
using Checkers.Enums;

public class GameController {
    private readonly IBoard _board;
    private readonly List<IPlayer> _players;
    private int _currentPlayerIndex = 0;
    private List<IPiece> _blackPiece;
    private List<IPiece> _redPiece;
    private readonly Dictionary<IPlayer, List<IPiece>> _playerPieces;
    public bool IsInCaptureChain = false;
    public IPiece? ChainingPiece = null;

    public event Action<IPiece>? OnPieceCaptured;
    public event Action<IPiece>? OnPiecePromoted;
    public event Action<IPlayer>? OnWinnerDeclared;
    public event Action<string>? OnUIMessage;

    public event Action<string>? OnGameMessage;

    public GameController(IBoard board, List<IPlayer> players, List<IPiece> blackPiece, List<IPiece> redPiece) {

        _board = board;
        _players = players;
        _blackPiece = blackPiece;
        _redPiece = redPiece;

        _playerPieces = new Dictionary<IPlayer, List<IPiece>>();
        _playerPieces[_players.First(p => p.Color == PieceColor.BLACK)] = _blackPiece;
        _playerPieces[_players.First(p => p.Color == PieceColor.RED)] = _redPiece;

        // InitializeBoard(_board); // USE THIS

    }

    public void InitializeBoard(IBoard board) {
        int blackPieceIndex = 0;
        int redPieceIndex = 0;

        for (int y = 0; y < (board.Size - 5); y++) {
            for (int x = 0; x < board.Size; x++) {
                if ((x + y) % 2 != 0) {
                    IPiece currentPiece = _blackPiece[blackPieceIndex];
                    board[x, y] = currentPiece;
                    currentPiece.Position = new Position(x, y);
                    blackPieceIndex++;
                }
            }
        }

        for (int y = 5; y < board.Size; y++) {
            for (int x = 0; x < board.Size; x++) {
                if ((x + y) % 2 != 0) {
                    IPiece currentPiece = _redPiece[redPieceIndex];
                    board[x, y] = currentPiece;
                    currentPiece.Position = new Position(x, y);
                    redPieceIndex++;
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

        IPiece? pieceToMove = _board[from.X, from.Y];
        if (pieceToMove == null || !CanMoveTo(pieceToMove, to)) {

            OnGameMessage?.Invoke($"Error: Invalid move ({from.X}, {from.Y}).");
            // OnUIMessage.Invoke();
            return false;

        } else {

            bool wasCapture = IsCapture(from, to);

            if (wasCapture) {
                CapturePiece(from, to);
            }

            OnGameMessage?.Invoke($"Moving {pieceToMove.Color} from ({from.X},{from.Y}) to ({to.X},{to.Y})");
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

    public bool CanMoveTo(IPiece piece, Position to) {
        if (piece == null) return false;
        List<Position> possibleMoves = GetPossibleMoves(piece);
        return possibleMoves.Contains(to);
    }

    public List<Position> GetPossibleMoves(IPiece piece) {
        List<Position> validMoves = new List<Position>();

        if (piece == null) return validMoves;

        List<int> directionsToCheck = new List<int>();
        if (piece.Color == PieceColor.BLACK) {
            directionsToCheck.Add(1);
        } else {
            directionsToCheck.Add(-1);
        }

        if (piece.PieceType == PieceType.KING) {
            directionsToCheck.Add(directionsToCheck[0] * -1);
        }

        int currentX = piece.Position.X;
        int currentY = piece.Position.Y;

        foreach (int direction in directionsToCheck) {

            Position leftMove = new Position(currentX - 1, currentY + direction);
            Position rightMove = new Position(currentX + 1, currentY + direction);

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

            Position leftCapturePos = new Position(currentX - 2, currentY + (2 * direction));
            Position leftMiddlePos = new Position(currentX - 1, currentY + direction);
            if (
                leftCapturePos.X >= 0 && leftCapturePos.X < _board.Size &&
                leftCapturePos.Y >= 0 && leftCapturePos.Y < _board.Size &&
                leftMiddlePos.X >= 0 && leftMiddlePos.X < _board.Size &&
                leftMiddlePos.Y >= 0 && leftMiddlePos.Y < _board.Size &&
                _board[leftCapturePos.X, leftCapturePos.Y] == null
            ) {
                IPiece? middlePiece = _board[leftMiddlePos.X, leftMiddlePos.Y];
                if (middlePiece != null && middlePiece.Color != piece.Color) {
                    validMoves.Add(leftCapturePos);
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
                IPiece? middlePiece = _board[rightMiddlePos.X, rightMiddlePos.Y];
                if (middlePiece != null && middlePiece.Color != piece.Color) {
                    validMoves.Add(rightCapturePos);
                }
            }
        }

        return validMoves;
    }

    public Dictionary<IPiece, List<Position>> GetAllValidMovesForPlayer(IPlayer player) {
        Dictionary<IPiece, List<Position>> allValidMoves = new Dictionary<IPiece, List<Position>>();

        List<IPiece> playerPieces = _playerPieces[player];

        foreach (IPiece piece in playerPieces) {
            List<Position> moves = GetPossibleMoves(piece);
            if (moves.Count > 0) {
                allValidMoves[piece] = moves;
            }
        }

        return allValidMoves;
    }

    public bool IsCapture(Position from, Position to) {
        int deltaX = Math.Abs(to.X - from.X);
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
            IPiece? capturedPiece = _board[pos.X, pos.Y];
            if (capturedPiece != null) {
                _board[pos.X, pos.Y] = null;
                IPlayer owner = _players.First(p => p.Color == capturedPiece.Color);
                _playerPieces[owner].Remove(capturedPiece);
                OnPieceCaptured?.Invoke(capturedPiece);
            }
        }
    }

    public void PromoteIfNeeded(IPiece piece) {
        if (piece.PieceType == PieceType.KING) return;
        if (piece.Color == PieceColor.BLACK && piece.Position.Y == _board.Size - 1) {
            piece.PieceType = PieceType.KING;
            OnPiecePromoted?.Invoke(piece);
        } else if (piece.Color == PieceColor.RED && piece.Position.Y == 0) {
            piece.PieceType = PieceType.KING;
            OnPiecePromoted?.Invoke(piece);
        }
    }

    public IPlayer GetCurrentPlayer() {
        return _players[_currentPlayerIndex];
    }

    public IBoard GetBoard() {
        return _board;
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

        OnWinnerDeclared?.Invoke(winner);
    }

}