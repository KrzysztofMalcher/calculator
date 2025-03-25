namespace Calculator.Interfaces;

public interface IComputingEngine
{
    float? Compute(int operation, float[] numbers);
    bool ValidateInput(int operation, float[] numbers);
    List<string> GetAvailableActions();
}
