using Calculator.Interfaces;

namespace Calculator;

public class ConsoleOutput: IConsole
{
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    public void Write(string message)
    {
        Console.Write(message);
    }

    public string ReadLine()
    {
        return Console.ReadLine();
    }
}