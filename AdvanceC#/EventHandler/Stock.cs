using System;
using System.Globalization;

namespace EventHandlerExample // <-- TAMBAHKAN INI
{

    // public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);

    // Class data event
    public class PriceChangedEventArgs : EventArgs { // Menggunakan EventArgs Subclass
        public decimal OldPrice { get; }
        public decimal NewPrice { get; }

        public PriceChangedEventArgs(decimal oldPrice, decimal newPrice) {
            OldPrice = oldPrice;
            NewPrice = newPrice;
        }
        // Membungkus semua yang dibutuhan subscriber, jika ada tambahan cukup masukan di EventArgs Subclass
    }

    public class Stock {

        string symbol;
        decimal price;

        public Stock(string symbol) => this.symbol = symbol;

        // public event PriceChangedHandler PriceChanged; // Declare event
        public event EventHandler<PriceChangedEventArgs>? PriceChanged; // Standart generic delegate delcaration

        // Event Trigger Method
        protected virtual void OnPriceChanged(PriceChangedEventArgs e) { // protected virtual "On" method
            PriceChanged?.Invoke(this, e); // Null conditional 
        }
        public decimal Price { // Class Property
            get => price; // Getter for price
            set {

                if (price == value) return; // Exit if price hasn't changed 

                decimal oldPrice = price; // price changed
                price = value;

                // if (PriceChanged != null) { // Null jika tidak punya subscriber
                //     PriceChanged(oldPrice, price); // Trigger event jika harga berubah dan memiliki subscriber
                // }

                OnPriceChanged(new PriceChangedEventArgs(oldPrice, price));
            }
        }
    }
}