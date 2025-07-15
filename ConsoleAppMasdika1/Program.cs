using System.Xml;

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

        }
    }
}
