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
            Person person1 = new Person(fullName: "Alex", personId: "JagoanMama123"); // Assigning readonly personId | the readonly value cant be changed
            // person1.personId = "JagoanPapah"; // the readonly value cant be changed | Compile Error
            Console.WriteLine(person1.personId);

            Person person2 = new Person(
                fullName: "David",
                personId: "David321"
            );

            person1.PersonInfo();
            Console.WriteLine();

            Person person3 = new Person("Masdika Ilhan Mansiz", age: 23, "Basah^Kuyup#69");
            person3.PersonInfo();

            // Sum of int Array 
            int[] numbers = new int[5] { 2, 7, 4, 5, 9 };

            Program SumNum = new Program();
            int totalNumbers = SumNum.SumOfNumbers(numbers); // Need to create an object bcs SumOfNumbers is not static
            Console.WriteLine(totalNumbers);


            Fruit apple = new Fruit(color: "Red", size: "Small");
            apple.ShowColor();
            apple.ShowSize();


            // Set and Get
            Fruit banana = new Fruit(color: "Yellow", size: "Large");
            banana.SetSeed("BananaSeed"); // Set seed
            banana.Price = 102811282311; // Set price
            Console.WriteLine(banana.Seed);
            Console.WriteLine(banana.Price);

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
    public string Seed { get; private set; }
    public void SetSeed(string newSeed) {
        Seed = newSeed;
    }

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