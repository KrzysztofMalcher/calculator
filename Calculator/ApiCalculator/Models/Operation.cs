namespace ApiCalculator.Models;

public sealed class Operation
{
    public int Id { get; set; }
    public string Action { get; set; }
    public string FirstOperand { get; set; }
    public string SecondOperand { get; set; }
    public string Result { get; set; }
}