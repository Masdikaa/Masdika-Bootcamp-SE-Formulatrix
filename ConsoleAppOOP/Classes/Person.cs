// Simple class declaration


using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

class Person { // Complex class will include 

    /*
     - Attribute and Modifier (public, internal, abstract, sealed, static, unsafe, partial)
     - Generic Type Declaration
     - Class Member
    */

    // Field -> Class Member 
    string _fullName; // Default modifier of field is Private that means can be only accessed inside the Person class 

    // Field initializer
    public int age = 10; // Public modifier enable access from anywhere outside Person class
                         // More access modifier -> public, private, internal, protected

    // Readonly Modifier
    /*
        Readonly modifier means that the field can only be assigned once when it declared or in the constructor
    */
    public readonly string personId; // The value assigned in Constructor below from Program.cs
    public readonly string personCountry = "Indonesia"; // value assigned when field being declared  

    // Class Constructor
    public Person(string fullName, int age, string personId) {
        _fullName = fullName;
        this.age = age;
        this.personId = personId;
    }

    // Constructor for fullname and personId only
    public Person(string fullName, string personId) : this(fullName, 0, personId) { // this keyword calling the class iteself
        _fullName = fullName;
        this.personId = personId;
    } // When only knewing the fullname 

    public void PersonInfo() {
        string text = $"""
        Full Name   : {_fullName}
        Age         : {age}
        ID          : {personId}
        Country     : {personCountry}
        """;
        Console.WriteLine(text);
    }

}