using System.ComponentModel;

namespace Calculator;

public class CalculatorEngine
{
    public float? Compute(AvailableActions operation, float[] numbers)
    {   
        ValidateInput(operation, numbers);
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

    public enum AvailableActions
    {
        [Description("Adding numbers")]
        Add,
        [Description("Subtracting numbers")]
        Subtract,
        [Description("Multiplicating numbers")]
        Multiply,
        [Description("Dividing numbers")]
        Divide
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