public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);

public class Stock {

    string symbol;
    decimal price;

    public Stock(string symbol) => this.symbol = symbol;

    public event PriceChangedHandler PriceChanged; // Declare event

    public decimal Price { // Class Property
        get => price; // Getter for price
        set {

            if (price == value) return; // Exit if price hasn't changed 

            decimal oldPrice = price; // price changed
            price = value;

            if (PriceChanged != null) { // Null jika tidak punya subscriber
                PriceChanged(oldPrice, price); // Trigger event jika harga berubah dan memiliki subscriber
            }
        }
    }
}