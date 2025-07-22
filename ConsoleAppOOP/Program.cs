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
using System.Numerics;
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

            var rect = new Rectangle(3, 4);
            (float width, float height) = rect; // Deconstruction call 
            Console.WriteLine(width + " " + height);

            // Date Time
            DateTimeOffset dateTime = DateTime.Now;
            Console.WriteLine(dateTime);

            TimeSpan timeSpan1 = new TimeSpan(2, 30, 59);
            TimeSpan timeSpan2 = new TimeSpan(1, 0, 0);
            Console.WriteLine(timeSpan1 += timeSpan2);

            TimeSpan ts = new TimeSpan(days: 10, hours: 12, minutes: 0, seconds: 0);
            Console.WriteLine(ts);

            // Parsing and TryParse
            FormattingAndParsing();

            // Random 
            RandomExample();

            // Enum 
            EnumExample(Size.Large);

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

        public static void FormattingAndParsing() {

            // Parsing 
            string booleanA = true.ToString();
            bool booleanB = bool.Parse(booleanA);

            Console.WriteLine(booleanA);
            Console.WriteLine(booleanB);

            // TryParse 
            bool success = int.TryParse("100", out int result); // True
            Console.WriteLine(success);

            // Formatting Hex
            string hex = 1982912.ToString("X"); // X besar untuk hexadecimal berformt besar(1E41C0) dan sebaliknya
            Console.WriteLine(hex);

            // Parsing Hex to Int
            int thirty = Convert.ToInt32("FF5733", 16);
            Console.WriteLine(thirty);

            // Big Integer
            BigInteger googol = BigInteger.Pow(10, 100); // 10^100
            Console.WriteLine(googol);

            // 16 Bit Float Type
            Half half = (Half)12.345678f;
            Console.WriteLine(half); // Output 12,34;

            var random = new Random();
            int a = (int)random.Next(20);
            Console.WriteLine(a);

        }

        public static void RandomExample() {

            // Bad
            for (int i = 0; i < 5; i++) {
                Random tempRandom = new Random(1);
                Console.WriteLine("- " + tempRandom.Next(1, 100));
            }

            // Good
            Random random = new Random(1);
            for (int i = 0; i < 5; i++) {
                Console.WriteLine("~ " + random.Next(1, 100));
            }
        }

        public enum Size {
            Small = 1,
            Medium = 2,
            Large = 3,
        }

        public static void EnumExample(Size size) {
            switch (size) {
                case Size.Small:
                    Console.WriteLine("Small Size");
                    break;
                case Size.Medium:
                    Console.WriteLine("Medium Size");
                    break;
                case Size.Large:
                    Console.WriteLine("Large Size");
                    break;
                default:
                    Console.WriteLine("Unknown Size");
                    break;
            }

            // Enum to Integral
            int index = (int)Size.Small;
            Console.WriteLine($"{Size.Small} : {index}");

            // Using Convert 
            decimal en = Convert.ToDecimal(Size.Medium);

            // Using ToString("D")
            Console.WriteLine(Size.Large.ToString("D"));
        }


    }
}