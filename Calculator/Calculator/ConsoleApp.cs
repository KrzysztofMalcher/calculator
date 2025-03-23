using System;
using System.ComponentModel;
using System.Reflection;
namespace Calculator;

public class ConsoleApp
{
    
    public void RunApp()
    {
        Console.WriteLine("Welcome in my simple calculator\n");
        var action = "";
        var stopApp = false;
        var calculatorEngine = new CalculatorEngine();
        while (!stopApp)
        {
            var availableActions = calculatorEngine.GetAvailableActions();
            ShowAvailableAction(availableActions);
            action = Console.ReadLine();
            if (availableActions.ContainsKey(action))
            {
                Console.WriteLine(availableActions[action]);
                float[] arguments = GetArguments();
                try
                {
                    calculatorEngine.ValidateInput(action, arguments);
                    Console.WriteLine("Calculation result: " + calculatorEngine.Compute(action, arguments));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } else if (action == "q")
            {
                Console.WriteLine("Bye");
                stopApp = true;
            }
            else
            {
                Console.WriteLine("Unknown action, please try again");
            }
        }
    }

    private static void ShowAvailableAction(Dictionary<string, string> availableActions)
    {
        Console.WriteLine("Choose action:");
        Console.WriteLine("q. Exit");
        int index = 1;
        foreach (string value in availableActions.Values)
        {
            Console.WriteLine($"{index}: {value}");
            index++;
        }
        Console.Write("Enter action number: ");
    }
    
    private static float[] GetArguments()
    {
        float firstParsedArgument = GetConsoleInput("Enter first number: ");
        float secondParsedArgument = GetConsoleInput("Enter second number: : ");

        return [firstParsedArgument, secondParsedArgument];
    }

    private static float GetConsoleInput(string consoleMessage)
    {
        var loopStop = false;
        float parsedArgument = 0;
        do
        {
            Console.Write(consoleMessage);
            var argument = Console.ReadLine();
            if (float.TryParse(argument, out var number))
            {
                parsedArgument = number;
                loopStop = true;
            }
            else
            {
                Console.WriteLine("Provided input doesn't appear to be a number");
            }
        } while (!loopStop);

        return parsedArgument;
    }
}