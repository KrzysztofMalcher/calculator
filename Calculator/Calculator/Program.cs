using System;
using System.ComponentModel;
using System.Reflection;

namespace Calculator;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Welcome in my simple calculator\n");
        var action = "";
        var stopApp = false;
        var calculatorEngine = new CalculatorEngine();

        while (!stopApp)
        {
            float[] arguments = [];
            showAvailableAction();
            action = Console.ReadLine();
            //GetAction(action);
            switch (action)
            {
                case "q":
                    stopApp = true;
                    break;
                case "1":
                    Console.WriteLine("You are adding two numbers");
                    arguments = GetArguments();
                    Console.WriteLine("Sum of provided numbers is " +
                                      calculatorEngine.Compute(CalculatorEngine.AvailableActions.Add, arguments));
                    break;
                case "2":
                    Console.WriteLine("You are subtracting two numbers");
                    arguments = GetArguments();
                    Console.WriteLine(string.Concat("Subtraction of provided numbers is " +
                                                    calculatorEngine.Compute(CalculatorEngine.AvailableActions.Subtract,
                                                        arguments)));
                    break;
                case "3":
                    Console.WriteLine("You are multiplicating two numbers");
                    arguments = GetArguments();
                    Console.WriteLine(string.Concat("Multiplication of provided numbers is " +
                                                    calculatorEngine.Compute(CalculatorEngine.AvailableActions.Multiply,
                                                        arguments)));
                    break;
                case "4":
                    Console.WriteLine("You are dividing two numbers");
                    arguments = GetArguments();
                    if (arguments[1] == 0)
                        Console.WriteLine("Dividing by zero is not allowed");
                    else
                        Console.WriteLine(string.Concat("Division of provided numbers is " +
                                                        calculatorEngine.Compute(
                                                            CalculatorEngine.AvailableActions.Divide, arguments)));
                    break;
                default:
                    Console.WriteLine("Unknown action, please try again");
                    break;
            }
        }
    }

    private static void showAvailableAction()
    {
        Console.WriteLine("Choose action:");
        Console.WriteLine("q. Exit");
        Console.WriteLine("1. Add\n2. Subtract\n3. Multiply\n4. Divide");
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
            Console.Write("Enter first number: ");
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