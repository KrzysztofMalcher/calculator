using Calculator.Interfaces;

namespace Calculator;

public class CalculatorEngine : IComputingEngine
{
    public float? Compute(string operation, float[] numbers)
    {
        switch (operation)
        {
            case "Add":
                return Add(numbers);
            case "Subtract":
                return Subtract(numbers);
            case "Multiplicate":
                return Multiply(numbers);
            case "Divide":
                return Divide(numbers);
        }

        return null;
    }

    public bool ValidateInput(string operation, float[] numbers)
    {
        if (operation == "Divide" && numbers[1] == 0)
        {
            throw new Exception("Dividing by zero is not allowed");
        }

        return true;
    }

    private static float Add(float[] numbers)
    {
        return numbers.Sum();
    }

    private static float Subtract(float[] numbers)
    {
        float result = numbers[0];
        for (int i = 1; i < numbers.Length; i++)
        {
            result -= numbers[i];
        }

        return result;
    }

    private static float Multiply(float[] numbers)
    {
        float result = 1;
        for (int i = 0; i < numbers.Length; i++)
        {
            result *= numbers[i];
        }

        return result;
    }

    private static float Divide(float[] numbers)
    {
        return numbers[0] / numbers[1];
    }

    public Dictionary<string, string> GetAvailableActions()
    {
        return _availableActions;
    }

    private Dictionary<string, string> _availableActions = new Dictionary<string, string>
    {
        {"Add","Adding numbers"},
        {"Subtract", "Subtracting numbers"},
        {"Multiplicate", "Multiplicating numbers"},
        {"Divide", "Dividing numbers"}
    };
}