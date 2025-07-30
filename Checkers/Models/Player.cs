namespace Checkers.Models;

using Checkers.Enums;
using Checkers.Interfaces;

public class Player : IPlayer {
    public PieceColor Color { set; get; }
    public string? Name { set; get; }
}