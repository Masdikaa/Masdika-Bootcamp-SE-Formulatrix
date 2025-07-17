using System.Dynamic;

namespace AccessModifier {
    /* 
    Public              : Fully Accessible
    Internal            : Only Assembly
    Private             : Only Containing Type
    Protected           : Containing Type and Subclass(Derived Type)
    Protected Internal
    Private Protected
    File                : Only Same File
    */

    class Program { //Default is Internal Access
        public static void Main() {
            DisplayGoodNight showGn = new DisplayGoodNight("Masdika");
            showGn.Say();
        }
    }

    public class GoodNight {

        private string _name;

        public GoodNight(string name) {
            _name = name;
        }

        protected void SayGoodNight() {
            Console.WriteLine($"Goodnight {_name}");
        }
    }

    public class DisplayGoodNight : GoodNight {
        public DisplayGoodNight(string name) : base(name) {

        }

        public void Say() {
            SayGoodNight(); // Accessing protected method from base
        }
    }


}