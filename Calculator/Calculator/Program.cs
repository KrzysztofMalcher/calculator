namespace Calculator;

internal class Program
{
    private static void Main(string[] args)
    {
        var consoleApp = new ConsoleApp(new ConsoleOutput());
        consoleApp.RunApp();
    }
}