namespace Calculator.Interfaces;

public interface IComputingEngine<TAction> where TAction : Enum
{
    float? Compute(TAction operation, float[] numbers);
    bool ValidateInput(TAction operation, float[] numbers);
}
