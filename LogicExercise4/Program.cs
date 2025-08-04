namespace LogicExercise;

public class Program() {
    public static void Main() {

        Console.Write("Input your number : ");
        int n = Convert.ToInt32(Console.ReadLine());

        RuleGenerator ruleGenerator = new RuleGenerator();
        ruleGenerator.AddRule(3, "Foo");
        ruleGenerator.AddRule(4, "Baz");
        ruleGenerator.AddRule(5, "Bar");
        ruleGenerator.AddRule(7, "Jazz");
        ruleGenerator.AddRule(9, "Huzz");
        ruleGenerator.AddRule(12, "Woo");

        ruleGenerator.Generate(n);

    }

}

public class RuleGenerator {
    private Dictionary<int, string> _rules = new();
    public void AddRule(int divisor, string output) {
        if (!_rules.ContainsKey(divisor))
            _rules.Add(divisor, output);
    }
    public void Generate(int max) {
        for (int i = 1; i <= max; i++) {
            string result = "";
            foreach (var rule in _rules) {
                if (i % rule.Key == 0)
                    result += rule.Value;
            }

            Console.WriteLine(string.IsNullOrEmpty(result) ? i.ToString() : result);
        }
    }
}