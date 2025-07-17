namespace GenericExample {
    public class Program {
        public static void Main() {

            var s = new Stack<int>();
            Console.WriteLine("Type" + s.GetType()); // TypeGenericExample.Stack`1[System.Int32]
            Console.WriteLine("Pos : " + s.position);

            s.Push(12);
            s.Push(8);
            // Console.WriteLine(s.Pop().GetType());

            Console.WriteLine(s.Pop()); // 8
            Console.WriteLine("Pos : " + s.position);
            Console.WriteLine(s.Pop()); // 12
            Console.WriteLine("Pos : " + s.position);

        }
    }

    public class Stack<T> {
        public int position;
        T[] data = new T[10];

        public void Push(T obj) => data[position++] = obj; // Accept T
        public T Pop() => data[--position]; // Return T
    }
}