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
using Inheritance;

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

            Fruit orange = new Fruit("Orange", "Smalls");
            orange.Price = 50_000;
            decimal orangePrice = orange.Price;

            orange.Weight = 10.0;
            Console.WriteLine("Orange Weight : " + orange.Weight);

            // Inheritance
            Room roomA = new Room { Area = 10000 };
            Decoration decRoomA = new Decoration {
                Area = roomA.Area,
                DecorationList = new string[] { "Painting", "Porcelain" }
            };
            Furniture furRoomA = new Furniture {
                Area = roomA.Area,
                FurnitureList = new string[] { "Carpet", "Table" }
            };

            Console.WriteLine($"Area : {roomA.Area}");
            Console.WriteLine($"Room Decoration List : {string.Join(", ", decRoomA.DecorationList)}");
            Console.WriteLine($"Room Furniture  List : {string.Join(", ", furRoomA.FurnitureList)}");

            Animal dog = new Animal("Mammals");
            Dog husky = new Dog(name: "Scooby");

            dog.Breathe();
            husky.Bark();

            TestAnimal(dog);
            TestAnimal(husky);

            // Casting reference and Conversion
            // Upcasting -> Create a base class from subclass | implicit convertion
            Dog bagas = new Dog("Bagas");
            Animal animal1 = bagas; // Karena dog adalah bagian dari animal maka bagas bisa di upcase jadi animal

            // Downcasting -> Creating a subclass from base class reference | explicit
            Dog dhimas = new Dog("Dimas");
            Animal animal2 = dhimas; // Upcasting 
            Dog dhimas2 = (Dog)animal2; //
            Console.WriteLine(dhimas2 == animal2); // true
            Console.WriteLine(dhimas2 == dhimas); // true


            // The as Operator
            // Return null when downcasting fail
            Animal animal3 = new Animal("The as Operator");
            Dog arifin = animal3 as Dog; // Return null
            if (arifin != null) arifin.Bark();

            // The is Operator
            // Tests whether a variable matches a pattern
            Animal animal4 = new Dog("Juan");
            if (animal4 is Dog) {
                Console.WriteLine("Animal4 is Dog");
            }

        }

        // Polymorphism
        public static void TestAnimal(Animal animal) {
            animal.Breathe();
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