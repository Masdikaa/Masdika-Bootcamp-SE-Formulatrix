class Parameter {
    /* Parameter Modifier 
        - None
        - ref
        - in
        - out 
    */
    //Test

    public void Foo(int p) {
        p = p + 1;
        Console.WriteLine($"Inside Method : {p}");
        // p refer to value passed in parameter
    }

    public static void RefModifier(ref int p) {
        p = p + 10; // operation will change reference value
        Console.WriteLine($"Inside Ref Method : {p}");
    }

    public static void Swap(ref string a, ref string b) {
        string c = a;
        a = b;
        b = c;
    }

    public static void Split(string fullName, out string firstName, out string lastName) {
        int i = fullName.LastIndexOf(' ');
        Console.WriteLine("i = " + i);
        firstName = fullName.Substring(0, i);
        lastName = fullName.Substring(i + 1);
    }
}