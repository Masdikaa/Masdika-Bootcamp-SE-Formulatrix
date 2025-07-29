namespace Checkers;

public interface IPlayer {
    public PieceColor Color { set; get; }
    public string? Name { set; get; }
}