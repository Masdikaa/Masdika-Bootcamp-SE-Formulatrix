namespace Checkers.Interfaces;

using Checkers.Models;
using Checkers.Enums;

public interface IPiece {
    PieceColor Color { get; set; }
    Position Position { get; set; }
    PieceType PieceType { get; set; }
}