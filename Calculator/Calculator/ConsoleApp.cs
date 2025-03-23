using Calculator.Interfaces;

namespace Calculator;

public class ConsoleApp
{
    private readonly IConsole _console;
    private readonly CalculatorEngine _calculatorEngine;
    
    public ConsoleApp(IConsole console)
    {
        _console = console;
        _calculatorEngine = new CalculatorEngine();
    }
    
    public void RunApp()
    {
        _console.WriteLine("Welcome in my simple calculator\n");
        var action = "";
        var stopApp = false;
        while (!stopApp)
        {
            var availableActions = _calculatorEngine.GetAvailableActions();
            ShowAvailableAction(availableActions);
            action = _console.ReadLine();
            if (availableActions.ContainsKey(action))
            {
                _console.WriteLine(availableActions[action]);
                float[] arguments = GetArguments();
                try
                {
                    _calculatorEngine.ValidateInput(action, arguments);
                    _console.WriteLine("Calculation result: " + _calculatorEngine.Compute(action, arguments));
                }
                catch (Exception ex)
                {
                    _console.WriteLine(ex.Message);
                }
            } else if (action == "q")
            {
                _console.WriteLine("Bye");
                stopApp = true;
            }
            else
            {
                _console.WriteLine("Unknown action, please try again");
            }
        }
    }

    private void ShowAvailableAction(Dictionary<string, string> availableActions)
    {
        _console.WriteLine("Choose action:");
        _console.WriteLine("q. Exit");
        int index = 1;
        foreach (string value in availableActions.Values)
        {
            _console.WriteLine($"{index}: {value}");
            index++;
        }
        _console.Write("Enter action number: ");
    }
    
    private float[] GetArguments()
    {
        float firstParsedArgument = GetConsoleInput("Enter first number: ");
        float secondParsedArgument = GetConsoleInput("Enter second number: : ");

        return [firstParsedArgument, secondParsedArgument];
    }

    private float GetConsoleInput(string consoleMessage)
    {
        var loopStop = false;
        float parsedArgument = 0;
        do
        {
            _console.Write(consoleMessage);
            var argument = Console.ReadLine();
            if (float.TryParse(argument, out var number))
            {
                parsedArgument = number;
                loopStop = true;
            }
            else
            {
                _console.WriteLine("Provided input doesn't appear to be a number");
            }
        } while (!loopStop);

        return parsedArgument;
    }
}