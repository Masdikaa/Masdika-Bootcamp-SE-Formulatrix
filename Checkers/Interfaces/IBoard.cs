namespace Checkers.Interfaces;

public interface IBoard {

    // public IPiece GetPiece(int x, int y);
    // public IPiece SetPiece(int x, int y, IPiece piece);
    IPiece this[int x, int y] { get; set; }
    public int Size { get; }

}