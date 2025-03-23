namespace Calculator;

internal class Program
{
    private static void Main(string[] args)
    {
        var consoleApp = new ConsoleApp(new CalculatorEngine(), new ConsoleOutput());
        consoleApp.RunApp();
    }
}