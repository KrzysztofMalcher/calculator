namespace Calculator;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Welcome in my simple calculator\n");

        var stopApp = false;

        while (!stopApp)
        {
            var action = "";
            float[] arguments = [];
            Console.WriteLine("Choose action:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Add\n2. Subtract\n3. Multiply\n4. Divide");
            Console.Write("Enter action number: ");

            action = Console.ReadLine();
            switch (action)
            {
                case "0":
                    stopApp = true;
                    break;
                case "1":
                    Console.WriteLine("You are adding two numbers");
                    arguments = GetArguments();
                    Console.WriteLine("Sum of provided numbers is " + arguments.Sum());
                    break;
                case "2":
                    Console.WriteLine("You are subtracting two numbers");
                    arguments = GetArguments();
                    Console.WriteLine(string.Concat("Subtraction of provided numbers is ",
                        arguments[0] - arguments[1]));
                    break;
                case "3":
                    Console.WriteLine("You are multiplicating two numbers");
                    arguments = GetArguments();
                    Console.WriteLine(string.Concat("Multiplication of provided numbers is ",
                        arguments[0] * arguments[1]));
                    break;
                case "4":
                    Console.WriteLine("You are dividing two numbers");
                    arguments = GetArguments();
                    if (arguments[1] == 0)
                        Console.WriteLine("Dividing by zero is not allowed");
                    else
                        Console.WriteLine(string.Concat("Division of provided numbers is ",
                            arguments[0] / arguments[1]));
                    break;
                default:
                    Console.WriteLine("Unknown action, please try again");
                    break;
            }
        }
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