using System.ComponentModel;

namespace Calculator;

public class CalculatorEngine
{
    public float? Compute(AvailableActions operation, float[] numbers)
    {
        ValidateInput( operation, numbers);
        switch (operation)
        {
            case AvailableActions.Add:
                return Add(numbers);
            case AvailableActions.Subtract:
                return Subtract(numbers);
            case AvailableActions.Multiply:
                return Multiply(numbers);
            case AvailableActions.Divide:
                return Divide(numbers);
        }

        return null;
    }

    public bool ValidateInput(AvailableActions operation, float[] numbers)
    {
        if (operation == AvailableActions.Divide && numbers[1] == 0)
        {
            throw new Exception("Dividing by zero is not allowed");
        }

        return true;
    }
    private readonly Dictionary<AvailableActions, string> AvailableOptionDescriptions = new()
    {
        { AvailableActions.Add, "Adding numbers" },
        { AvailableActions.Subtract, "Subtracting numbers" },
        { AvailableActions.Multiply, "Multiplicating numbers" },
        { AvailableActions.Divide, "Dividing numbers" },
    };
    
    public enum AvailableActions
    {
        [Description("Adding numbers")]
        Add = 0,
        [Description("Subtracting numbers")]
        Subtract = 1,
        [Description("Multiplicating numbers")]
        Multiply = 2,
        [Description("Dividing numbers")]
        Divide = 3
    }

    public string GetActionDescritpion(AvailableActions operation)
    {
        return AvailableOptionDescriptions[operation];
    }
    
    private float Add(float [] numbers)
    {
        return numbers.Sum();
    }

    private float Subtract(float [] numbers)
    {
        float result = numbers[0];
        for (int i = 1; i < numbers.Length; i++)
        {
            result -= numbers[i];
        }
        
        return result;
    }

    private float Multiply(float [] numbers)
    {
        float result = 1;
        for (int i = 0; i < numbers.Length; i++)
        {
            result *= numbers[i];
        }

        return result;

    }

    private float Divide(float [] numbers)
    {
        return numbers[0] / numbers[1];
    }
}