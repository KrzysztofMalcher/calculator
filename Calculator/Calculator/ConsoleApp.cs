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
            ShowAvailableAction<CalculatorEngine.AvailableActions>();
            action = Console.ReadLine();
            CalculatorEngine.AvailableActions operation;
            bool properEnumValue = Enum.TryParse(action, out operation);
            if (properEnumValue && Enum.IsDefined(typeof(CalculatorEngine.AvailableActions),operation))
            {
                Console.WriteLine(GetEnumDescription(operation));
                float[] arguments = GetArguments();
                Console.WriteLine("Calculation result: " + calculatorEngine.Compute(operation, arguments));
                
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

    private static void ShowAvailableAction<TEnum>() where TEnum : Enum
    {
        Console.WriteLine("Choose action:");
        Console.WriteLine("q. Exit");
        var values = Enum.GetValues(typeof(TEnum));
        int index = 0;
        foreach (TEnum enumValue in Enum.GetValues(typeof(TEnum)))
        {
            string description = GetEnumDescription(enumValue);
            Console.WriteLine($"{index}: {description}");
            index++;
        }
        Console.Write("Enter action number: ");
    }
    
    private static string GetEnumDescription(Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());
        DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attribute == null ? value.ToString() : attribute.Description;
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