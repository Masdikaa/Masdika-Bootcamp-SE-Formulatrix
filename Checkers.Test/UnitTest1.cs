using System.Reflection.Metadata;
using Checkers.Classes;
using Checkers.Classes.Models;
using Checkers.Enums;
using Checkers.Interfaces;

namespace Checkers.Test;

public class Tests {

    private IBoard _board;
    private List<IPlayer> _players;
    private List<IPiece> _blackPieces;
    private List<IPiece> _redPieces;
    private GameController? _gameController;

    [SetUp]
    public void Setup() {
        _board = new Board(8);
        _players = new List<IPlayer> {
            new Player { Color = PieceColor.BLACK, Name = "Player 1 Test" },
            new Player { Color = PieceColor.RED, Name = "Player 2 Test" }
        };
        _blackPieces = new List<IPiece> { new Piece { Color = PieceColor.BLACK } };
        _redPieces = new List<IPiece> { new Piece { Color = PieceColor.RED } };
    }

    [Test]
    public void CanMoveTo_ValidForwardMoveToEmptySquare_ReturnsTrue() {
        for (int y = 0; y < _board.Size; y++) {
            for (int x = 0; x < _board.Size; x++) {
                _board[x, y] = null;
            }
        }

        IPiece blackPiece = _blackPieces[0];
        blackPiece.Position = new Position(1, 2);
        _board[1, 2] = blackPiece;

        Position to = new Position(0, 3);

        _gameController = new GameController(_board, _players, _blackPieces, _redPieces);

        bool result = _gameController.CanMoveTo(blackPiece, to);
        Assert.That(result, Is.True, "Ok");
        // Assert.IsTrue(result, "Piece can move forward to empty square");
        // ?Comment InitializeBoard in GameController Constructor
    }

    [Test]
    public void MovePiece_MoveToFront_ReturnTrue() {
        Position fromPosition = new Position(1, 2);
        Position toPosition = new Position(0, 3);

        IPiece blackPiece = new Piece { Color = PieceColor.BLACK, Position = fromPosition };
        _board[fromPosition.X, fromPosition.Y] = blackPiece;

        List<IPiece> blackPiecesForTest = new List<IPiece> { blackPiece };

        _gameController = new GameController(_board, _players, blackPiecesForTest, new List<IPiece>());
        _gameController.MovePiece(blackPiece, toPosition);

        var emptyBoard = _board[fromPosition.X, fromPosition.Y];

        Assert.IsNull(_board[fromPosition.X, fromPosition.Y], "Petak asal seharusnya kosong setelah pion bergerak.");
        // Assert.AreEqual(blackPiece, _board[toPosition.X, toPosition.Y], "Petak tujuan seharusnya berisi pion yang dipindahkan.");
        // Assert.AreEqual(toPosition, blackPiece.Position, "Properti posisi pada pion seharusnya diperbarui.");
    }


}