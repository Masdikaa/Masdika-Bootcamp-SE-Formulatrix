using System.Runtime.InteropServices;

namespace Main {
    class Program {
        static void Main() {
            int x = 10;

            // Parameter
            Parameter a = new Parameter();
            a.Foo(x); // assigning parameter 
            Console.WriteLine($"Outside Method : {x}");

            // Reference modifier
            int xRef = 5;
            Console.WriteLine($"Initial Value : {xRef}");
            Parameter.RefModifier(ref xRef);
            Console.WriteLine($"Outside Ref Method : {xRef}");

            // Example swap value
            string value1 = "First";
            string value2 = "Second";
            Console.WriteLine($"Before swap\nvalue1 : {value1}\nvalue2 : {value2} ");

            Parameter.Swap(ref value1, ref value2);

            Console.WriteLine($"After  swap\nvalue1 : {value1}\nvalue2 : {value2} ");

            // Out modifier 
            string fullName = "Masdika Ilhan Mansiz";
            string firstName;
            string lastName;

            Parameter.Split(fullName, out firstName, out lastName); // Assing out value into variable
            Console.WriteLine(firstName + "-" + lastName);

            // Checked overflow
            // Check.NumericCheck();

            // Ternary Operation
            int maxNum = Max(23, 21);
            Console.WriteLine(maxNum);

            // Raw String Literal
            string raw = """This string contains "double quotes" without escaping.""";
            Console.WriteLine(raw);

            // Arrays 
            int[] arr = new int[10]; // Declare Array with length = 10
            Console.WriteLine(arr);
            foreach (int element in arr) {
                Console.Write(element + " "); // Default array values == 0
            }

            Console.WriteLine();

            string[] students = { "Bagas", "Juan", "Joko" }; // Array Initializatin 
            string[,] groupList = { { "Juan", "Joko" }, { "Juan", "Bagas" }, { "Bagas", "Joko" } }; // Multidimentional

            Console.WriteLine(groupList.GetLength(1));

            for (int i = 0; i < groupList.GetLength(0); i++) {
                for (int j = 0; j < groupList.GetLength(1); j++) {
                    Console.Write(groupList[i, j] + " ");
                }
                Console.WriteLine();
            }

            // Indices
            Console.WriteLine("Last Index: " + students[^1]); // Caret operator (^)
            string[] twoStudents = students[..2];
            foreach (string student in twoStudents) {
                Console.WriteLine("Student");
            }

            int[] testArr = DemoArray.TestArray(10);

            Console.WriteLine(testArr.Length);

            int trr = 45;
            double dtrr = (double)trr;

            // null coalescing operator
            string s1 = null;
            string s2 = s1 ??= "default";
            Console.WriteLine(s1);
            Console.WriteLine(s2);

            int f = 5;
            int g = 10;
            Console.WriteLine((f == g) ? "Equal" : "Not Equal");

        }

        static int Max(int a, int b) {
            return (a > b) ? a : b;  //Ternary 
        }

    }
}
