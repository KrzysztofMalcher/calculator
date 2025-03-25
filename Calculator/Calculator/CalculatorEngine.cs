using Calculator.Interfaces;

namespace Calculator;

public class CalculatorEngine : IComputingEngine
{
    public float? Compute(int operation, float[] numbers)
    {
        switch (operation)
        {
            case 0:
                return Add(numbers);
            case 1:
                return Subtract(numbers);
            case 2:
                return Multiply(numbers);
            case 3:
                return Divide(numbers);
        }

        return null;
    }

    public bool ValidateInput(int operation, float[] numbers)
    {
        if (operation == 3 && numbers[1] == 0)
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

    public List<string> GetAvailableActions()
    {
        return _availableActions;
    }

    private List<string> _availableActions = new List<string>
    {
        "Adding numbers",
        "Subtracting numbers",
        "Multiplication numbers",
        "Dividing numbers"
    };
}