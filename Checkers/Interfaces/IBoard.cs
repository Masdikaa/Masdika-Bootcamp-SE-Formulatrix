namespace Checkers.Interfaces;

public interface IBoard {
    IPiece? this[int x, int y] { get; set; } // Indexer list piece
    int Size { get; }
}