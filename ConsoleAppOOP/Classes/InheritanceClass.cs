// Inheritance basic
namespace Inheritance {

    public class Room { // Base Class
        public double Area;
    }

    public class Decoration : Room { // Derived Class/Subclass
        public string[]? DecorationList;
    }

    public class Furniture : Room {
        public string[]? FurnitureList;
    }


    public class Animal {

        public Animal(string species = "Nothing") {
            Console.WriteLine(species);
        }

        public void Breathe() {
            Console.WriteLine("Breathe...");
        }

        // Virtual member
        public virtual int pace => 0; // 

    }

    public class Dog : Animal {
        public Dog(string name) : base("Dog Class") { // Inherit Constructor
            Console.WriteLine("Dog name : " + name);
        }

        public void Bark() {
            Console.WriteLine("Bark...");
        }

        int speed = 45;
        public override int pace => speed;
    }

}