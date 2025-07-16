public class Fruit {
    public string Color;
    public string Size;
    private decimal _price; // -> Private field 
    // Setter and Getter
    // Basic
    public decimal Price {
        get { return _price; }
        set { _price = value; }
    }

    // Auto Implemented Property > Set and Get
    public double Weight { get; set; }

    // Get / Set Only
    // ReadOnly (Get)
    public string? Seed { get; private set; }
    public void SetSeed(string newSeed) {
        Seed = newSeed;
    }

    public Fruit(string color, string size) {
        Color = color;
        Size = size;
    }

    public void ShowColor() {
        Console.WriteLine(Color);
    }

    public void ShowSize() {
        Console.WriteLine(Size);
    }
}
