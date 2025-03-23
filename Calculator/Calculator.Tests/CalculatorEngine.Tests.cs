namespace Calculator.Tests;

public class CalculatorEngineTests
{
    private readonly CalculatorEngine _engine;

    public CalculatorEngineTests()
    {
        _engine = new CalculatorEngine();
    }

    [Fact]
    public void Compute_When_AddOperation_Then_Sum()
    {
        //Arrange
        var numbers = new float[] { 2, 3, 5 };

        //Act
        var result = _engine.Compute("1", numbers);

        //Assert
        Assert.Equal(10, result);
    }

    [Fact]
    public void Compute_When_SubtractOperation_Then_Difference()
    {
        //Arrange
        var numbers = new float[] { 10, 3, 2 };
        
        //Act
        var result = _engine.Compute("2", numbers);
        
        //Assert
        Assert.Equal(5, result);
    }

    [Fact]
    public void Compute_When_MultiplyOperation_Then_Multiplication()
    {
        //Arrange
        var numbers = new float[] { 2, 3, 4 };
        
        //Act
        var result = _engine.Compute("3", numbers);
        
        //Assert
        Assert.Equal(24, result);
    }

    [Fact]
    public void Compute_When_DivideOperation_Then_Division()
    {
        //Arrange
        var numbers = new float[] { 20, 4 };
        
        //Act
        var result = _engine.Compute("4", numbers);
        
        //Assert
        Assert.Equal(5, result);
    }

    [Fact]
    public void Compute_When_InvalidOperation_Then_Null()
    {
        //Arrange
        var numbers = new float[] { 1, 2 };
        
        //Act
        var result = _engine.Compute("invalid", numbers);
        
        //Assert
        Assert.Null(result);
    }

    [Fact]
    public void ValidateInput_When_DivideByZero_Then_ThrowsException()
    {
        //Arrange
        var numbers = new float[] { 10, 0 };

        //Act
        var exception = Assert.Throws<Exception>(() => _engine.ValidateInput("4", numbers));
        
        //Assert
        Assert.Equal("Dividing by zero is not allowed", exception.Message);
    }

    [Fact]
    public void ValidateInput_When_ValidInput_ThenTrue()
    {
        //Arrange
        var numbers = new float[] { 10, 2 };
        
        //Act
        var result = _engine.ValidateInput("4", numbers);
        
        //Assert
        Assert.True(result);
    }

    [Fact]
    public void GetAvailableActions_ReturnsCorrectDictionary()
    {
        //Act
        var actions = _engine.GetAvailableActions();

        //Assert
        Assert.IsType<Dictionary<string, string>>(actions);
        Assert.Equal(4, actions.Count);
        Assert.Contains("1", actions.Keys);
        Assert.Equal("Adding numbers", actions["1"]);
    }
}