namespace Checkers.Interfaces;

using Checkers.Enums;

public interface IPlayer {
    PieceColor Color { set; get; }
    string? Name { set; get; }
}