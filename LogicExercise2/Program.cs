namespace LogicExercise2 {
    public class Program {
        public static void Main() {
            Console.Write("Enter your number : ");
            int n = Convert.ToInt32(Console.ReadLine());
            // int n = 110;

            for (int i = 1; i <= n; i++) {
                string x = "";

                if (i % 3 == 0) {
                    x += "Foo";
                    // Console.WriteLine($"x : {x} | i : {i}");
                }

                if (i % 5 == 0) {
                    x += "Bar";
                }

                if (i % 7 == 0) {
                    x += "Jazz";
                }

                if (x != "") {
                    Console.WriteLine(x);
                } else {
                    Console.WriteLine(i);
                }
            }

            // for (int i = 1; i <= n; i++) {
            //     if (i % 7 == 0 && i % 5 == 0 && i % 3 == 0) {
            //         Console.WriteLine(i + " FooBarJazz");
            //     } else if (i % 7 == 0 && i % 5 == 0) {
            //         Console.WriteLine(i + " BarJazz");
            //     } else if (i % 7 == 0 && i % 3 == 0) {
            //         Console.WriteLine(i + " FooJazz");
            //     } else if (i % 7 == 0) {
            //         Console.WriteLine(i + " Jazz");
            //     } else if (i % 5 == 0 && i % 3 == 0) {
            //         Console.WriteLine(i + " FooBar");
            //     } else if (i % 5 == 0) {
            //         Console.WriteLine(i + " Bar");
            //     } else if (i % 3 == 0) {
            //         Console.WriteLine(i + " Foo");
            //     } else {
            //         Console.WriteLine(i);
            //     }
            // }
        }
    }
}