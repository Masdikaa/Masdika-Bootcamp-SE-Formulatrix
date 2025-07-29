namespace Checkers;

public interface IPiece {
    PieceColor Color { get; set; }
    Position Position { get; set; }
    PieceType PieceType { get; set; }
}