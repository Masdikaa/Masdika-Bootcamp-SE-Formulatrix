namespace Checkers;

public class Board : IBoard {

    private IPiece[,] _grid { set; get; }

    public Board(int size) {
        _grid = new IPiece[size, size];
    }

    public IPiece this[int x, int y] {
        get => _grid[x, y];
        set => _grid[x, y] = value;
    }

}