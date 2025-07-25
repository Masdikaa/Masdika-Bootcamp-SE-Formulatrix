using System;
using System.Collections.Generic;
using System.Linq;

public class Program {
    public static void Main() {

        GameController game = new GameController();
        game.PlayerSetup(Color.Blue);

    }
}

public class GameController() {

    private List<Player> _players = new List<Player>();
    private static Random _random = new Random();

    public void PlayerSetup(Color userColor) {
        List<Color> availableColors = new List<Color>((Color[])Enum.GetValues(typeof(Color)));

        if (!availableColors.Contains(userColor)) {
            Console.WriteLine("Warna tidak valid.");
            return;
        }

        Player player1 = new Player("Player1", userColor);
        _players.Add(player1);

        availableColors.Remove(userColor);

        List<Color> shuffledColors = ShuffleColors(availableColors);

        for (int i = 0; i < 3; i++) {
            string playerName = "Player" + (i + 2).ToString();
            Player player = new Player(playerName, shuffledColors[i]);
            _players.Add(player);
        }

        for (int i = 0; i < _players.Count; i++) {
            Player p = _players[i];
            Console.WriteLine($"{p.Name}, Color: {p.Color}");
        }
    }

    private List<Color> ShuffleColors(List<Color> colors) {
        List<Color> shuffled = new List<Color>(colors);
        int n = shuffled.Count;
        while (n > 1) {
            n--;
            int k = _random.Next(n + 1);
            Color temp = shuffled[k];
            shuffled[k] = shuffled[n];
            shuffled[n] = temp;
        }
        return shuffled;
    }


}

public interface IPlayer {
    string Name { set; get; }
    Color Color { set; get; }
}

public class Player : IPlayer {
    public string Name { get; set; }
    public Color Color { get; set; }

    public Player(string name, Color color) {
        Name = name;
        Color = color;
    }
}

public enum Color {
    Blue,
    Red,
    Green,
    Yellow
}