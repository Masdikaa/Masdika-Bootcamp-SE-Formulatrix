namespace LogicExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input your number : ");
            int number = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= number; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    Console.Write("FOOBAR");
                }
                else if (i % 3 == 0)
                {
                    Console.Write("FOO");
                }
                else if (i % 5 == 0)
                {
                    Console.Write("BAR");
                }
                else
                {
                    Console.Write(i);
                }
                Console.Write(",");
            }

        }
    }
}
