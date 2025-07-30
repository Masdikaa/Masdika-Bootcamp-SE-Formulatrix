namespace Checkers.Models;

using Checkers.Enums;
using Checkers.Interfaces;

public class Piece : IPiece {
    public PieceColor Color { get; set; }
    public Position Position { get; set; }
    public PieceType PieceType { get; set; }
}