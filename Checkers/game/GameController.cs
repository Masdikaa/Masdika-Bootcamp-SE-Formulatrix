namespace Checkers;

public class GameController {
    private IBoard _board;
    private List<IPlayer> _players;
    private int _currentPlayerIndex = 0;
    private Dictionary<IPlayer, List<IPlayer>> _playersByColor;

    public GameController(IBoard board, IPlayer player1, IPlayer player2) {
        _board = board;
        _players = [player1, player2];
        _players.Add(player1);
    }

    public void ShowPlayer() {
        Console.WriteLine($"{_players[0].Name} is {_players[0].Color}");
        Console.WriteLine($"{_players[1].Name} is {_players[1].Color}");
    }
}