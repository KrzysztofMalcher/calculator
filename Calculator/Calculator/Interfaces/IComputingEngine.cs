namespace Calculator.Interfaces;

public interface IComputingEngine
{
    float? Compute(string operation, float[] numbers);
    bool ValidateInput(string operation, float[] numbers);
    Dictionary<string, string> GetAvailableActions();
}
