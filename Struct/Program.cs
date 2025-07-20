namespace StructExample {
    /*
        Simple Data
        No Inheritance
        Support interface
        Value Type
        Cant have Destructor
    */
    public class Program {
        public static void Main() {
            Point point = new Point(5, 2);
            point.Display();

            Point point1 = new Point();
            point1.Display();
        }
    }

    struct Point {
        public int X = 1;
        public int Y;

        public Point(int x, int y) { // Constructor
            X = x;
            Y = y;
        }

        public Point() => Y = 1;

        public void Display() {
            Console.WriteLine($"X : {X}");
            Console.WriteLine($"Y : {Y}");
        }
    }

    struct ReadOnlyFuncP {
        public int X, Y;
        // public readonly void ResetX() => 0; // Cant modify from readonly function
    }

    // Ref Struct
    // stack only
    ref struct RefStruct {
        public int X;
        public void DoSomething() {
            Console.WriteLine(X);
        }
    }

}