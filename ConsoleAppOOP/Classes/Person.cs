// Simple class declaration


class Person { // Complex class will include 

    /*
     - Attribute and Modifier (public, internal, abstract, sealed, static, unsafe, partial)
     - Generic Type Declaration
     - Class Member
    */

    // Field -> Class Member 
    string fullName; // Default modifier of field is Private that means can be only accessed inside the Person class 

    // Field initializer
    public int Age = 10; // Public modifier enable access from anywhere outside Person class
                         // More access modifier -> public, private, internal, protected

    // Readonly Modifier
    /*
        Readonly modifier means that the field can only be assigned once when it declared or in the constructor
    */
    public readonly string personId; // The value assigned in Constructor below from Program.cs
    public readonly string personCountry = "Indonesia"; // value assigned when field being declared  

    // Class Constructor
    public Person(string id) {
        personId = id;
    }

}