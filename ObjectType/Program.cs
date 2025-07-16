namespace ObjectExample {
    // Object type is base class of for all other types
    /* 
        Flexibility
        Reference Type
        Uses a lot of Casting
    */
    public class Program {
        static void Main() {
            Stack stack = new Stack();
            stack.Push("Message"); // Index 0
            stack.Push("Masdika"); // Index 1

            Console.WriteLine(stack.Pop()); // Show index 1
            Console.WriteLine(stack.Pop()); // Show index 0

            // Boxing : Value Type -> Ref Type
            int x = 10;
            object objX = x;

            // Unboxing : Reference Type -> Value Type
            object objY = "Object";
            string y = (string)objY;

            // ToString Method 
            int year = 2025;
            string sYear = year.ToString();
            Console.WriteLine($"year  : {year.GetType()}"); // System.Int32
            Console.WriteLine($"sYear : {sYear.GetType()}"); // System.String

            Panda panda = new Panda { Name = "Poo" };
            Console.WriteLine(panda.ToString());

        }
    }


    public class Stack {
        int position;
        object[] data = new object[10]; // data can be Array or List

        public void Push(object obj) {
            data[position++] = obj;
        }

        public object Pop() {
            return data[--position];
        }
    }

    public class Panda {
        public string Name;
        public override string ToString() => Name; // by default ToString method is returning Namespace.ClassName
    }

}