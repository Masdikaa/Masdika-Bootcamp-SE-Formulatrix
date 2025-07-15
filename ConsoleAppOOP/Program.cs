/*

Class
Class adalah sebuah blueprint dari object yang di akan di instance

Class member
- Field
- Constant
- Method
- Instance Constructor
- Deconstructor
- 'this' keyword reference
- Properties
- Indexer
- Primary Constructor
- Static Constructor 
- Static Classer
- Finalizer
- Partial type and method
- The Named Operator

*/
namespace MainOOP {
    class Program() {
        public static void Main() {
            // Object from Person.cs
            Person person1 = new Person("JagoanMama123"); // Assigning readonly personId | the readonly value cant be changed
            // person1.personId = "JagoanPapah"; // the readonly value cant be changed | Compile Error
            Console.WriteLine(person1.personId);


            // Sum of int Array 
            int[] numbers = new int[5] { 2, 7, 4, 5, 9 };

            Program SumNum = new Program();
            int totalNumbers = SumNum.SumOfNumbers(numbers); // Need to create an object bcs SumOfNumbers is not static
            Console.WriteLine(totalNumbers);


            Fruit apple = new Fruit(color: "Red", size: "Small");
            apple.ShowColor();
            apple.ShowSize();

        }

        int SumOfNumbers(int[] numbers) {

            int total = 0;
            foreach (int item in numbers) {
                total += item;
            }

            return total;
        }
    }
}


class Fruit {
    public string color;
    public string size;

    public Fruit(string color, string size) {
        this.color = color;
        this.size = size;
    }

    public void ShowColor() {
        Console.WriteLine(this.color);
    }

    public void ShowSize() {
        Console.WriteLine(this.size);
    }
}