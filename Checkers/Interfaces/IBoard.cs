namespace Checkers.Interfaces;

public interface IBoard {
    IPiece? this[int x, int y] { get; set; }
    int Size { get; }
}