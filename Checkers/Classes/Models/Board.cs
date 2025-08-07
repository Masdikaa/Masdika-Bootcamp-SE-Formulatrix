namespace Checkers.Classes.Models;

using Checkers.Interfaces;

public class Board : IBoard {

    private readonly IPiece?[,] _grid; // Di set sekali dalam constructor
    public int Size { get; }

    public Board(int size) {
        Size = size;
        _grid = new IPiece[Size, Size]; // Set size dari grid
    }

    public IPiece? this[int x, int y] {
        get => _grid[x, y];
        set => _grid[x, y] = value;
    }

}
