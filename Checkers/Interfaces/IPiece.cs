namespace Checkers.Interfaces;

using Checkers.Classes;
using Checkers.Enums;

public interface IPiece {
    PieceColor Color { get; set; }
    Position Position { get; set; }
    PieceType PieceType { get; set; }
}