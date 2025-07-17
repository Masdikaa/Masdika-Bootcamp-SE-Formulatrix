namespace InterfaceExample {
    // Class sebagai kontrak untuk menentukan member yang harus diimplementasikan oleh sebuah class
    // Abstraksi
    public class Program {
        public static void Main() {
            Circle circle = new Circle(15);
            double area = circle.GetArea();
            double perimeter = circle.GetPerimeter();
            Console.WriteLine($"Circle Radius\t\t: {circle.Radius}\nCircle Area\t\t: {area}\nCircle Perimeter\t: {perimeter}");

            // Explicit Interface Implementation
            Machine mc = new Machine(13);
            IPrinter printer = mc;
            IScanner scanner = mc;

            printer.Print();
            scanner.Print();

            Console.WriteLine($"Print Price : {printer.Price()}");
            Console.WriteLine($"Scan  Price : {scanner.Price()}");
            /*-------------------------------------------------------*/

            Machine m = new Machine(19);
            ((IPrinter)m).Print();
            ((IScanner)m).Print();

            Console.WriteLine($"Print Price :{((IPrinter)m).Price()}");
            Console.WriteLine($"Scan  Price :{((IScanner)m).Price()}");


        }
    }

    public interface IShape {
        double GetArea();
        double GetPerimeter();
    }

    public class Circle : IShape {
        public double Radius { get; set; }

        public Circle(double radius) {
            Radius = radius;
        }

        public double GetArea() {
            return Math.PI * Radius * Radius;
        }

        public double GetPerimeter() {
            return 2 * Math.PI * Radius;
        }

    }

    // Explicit Interface Implementation
    public interface IPrinter {
        public void Print();
        public int Price();
    }

    public interface IScanner {
        public void Print();
        public double Price();
    }
    // Different interface but same method identifier

    public class Machine : IPrinter, IScanner { // Inherit 2 interface classes 

        public int Total { set; get; }

        public Machine(int total) {
            Total = total;
        }

        void IPrinter.Print() {
            Console.WriteLine($"Print IPrinter {Total}");
        }

        void IScanner.Print() {
            Console.WriteLine($"Print IScanner {Total}");
        }

        int IPrinter.Price() {
            return Total * 1000;
        }

        double IScanner.Price() {
            double total = Total * 1500.0;
            return total;
        }

    }

}