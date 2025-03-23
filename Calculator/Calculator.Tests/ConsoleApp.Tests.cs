namespace Calculator.Tests;

using Calculator;
using Calculator.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

public class ConsoleAppTests
{
    private readonly Mock<IConsole> _mockConsole;
    private readonly Mock<IComputingEngine> _mockCalculatorEngine;
    private readonly ConsoleApp _consoleApp;

    public ConsoleAppTests()
    {
        // Arrange: Set up the mocks
        _mockConsole = new Mock<IConsole>();
        _mockCalculatorEngine = new Mock<IComputingEngine>();
        _consoleApp = new ConsoleApp(_mockCalculatorEngine.Object, _mockConsole.Object);
    }

    [Fact]
    public void RunApp_When_Started_Then_DisplayWelcomeMessage()
    {
        // Arrange
        var availableActions = new Dictionary<string, string>
        {
            { "1", "Adding numbers" },
            { "2", "Subtracting numbers" }
        };
        _mockCalculatorEngine.Setup(c => c.GetAvailableActions()).Returns(availableActions);
        
        // Act
        _consoleApp.RunApp();

        // Assert
        _mockConsole.Verify(c => c.WriteLine("Welcome in my simple calculator\n"), Times.Once);
    }

    [Fact]
    public void RunApp_When_Started_Then_ShouldDisplayAvailableActions()
    {
        // Arrange
        var availableActions = new Dictionary<string, string>
        {
            { "1", "Adding numbers" },
            { "2", "Subtracting numbers" }
        };

        _mockCalculatorEngine.Setup(c => c.GetAvailableActions()).Returns(availableActions);

        // Act
        _mockConsole.SetupSequence(c => c.ReadLine()).Returns("1").Returns("q");
        _consoleApp.RunApp();

        // Assert
        _mockConsole.Verify(c => c.WriteLine(It.IsAny<string>()), Times.AtLeastOnce);
    }

    [Fact]
    public void RunApp_When_ValidActionIsSelected_Then_ComputeIsCalled()
    {
        // Arrange
        var availableActions = new Dictionary<string, string>
        {
            { "1", "Adding numbers" },
            { "2", "Subtracting numbers" }
        };

        _mockCalculatorEngine.Setup(c => c.GetAvailableActions()).Returns(availableActions);
        _mockCalculatorEngine.Setup(c => c.Compute("1", It.IsAny<float[]>())).Returns(5);
        _mockConsole.SetupSequence(c => c.ReadLine()).Returns("1").Returns("q");

        // Act
        _consoleApp.RunApp();

        // Assert
        _mockCalculatorEngine.Verify(c => c.Compute("1", It.IsAny<float[]>()), Times.Once);
    }

    [Fact]
    public void RunApp_When_VlidationFailed_Then_ExceptionIsThrown()
    {
        // Arrange
        var availableActions = new Dictionary<string, string>
        {
            { "4", "Dividing numbers" }
        };

        _mockCalculatorEngine.Setup(c => c.GetAvailableActions()).Returns(availableActions);
        _mockCalculatorEngine.Setup(c => c.Compute("4", It.IsAny<float[]>()))
            .Throws(new Exception("Dividing by zero is not allowed"));
        _mockConsole.SetupSequence(c => c.ReadLine()).Returns("4").Returns("q");

        // Act
        _consoleApp.RunApp();

        // Assert
        _mockConsole.Verify(c => c.WriteLine("Dividing by zero is not allowed"), Times.Once);
    }

    [Fact]
    public void RunApp_When_QuitActionIsChosen_Then_ShouldExit()
    {
        // Arrange
        var availableActions = new Dictionary<string, string>
        {
            { "1", "Adding numbers" },
            { "2", "Subtracting numbers" }
        };

        _mockCalculatorEngine.Setup(c => c.GetAvailableActions()).Returns(availableActions);
        _mockConsole.SetupSequence(c => c.ReadLine()).Returns("q");

        // Act
        _consoleApp.RunApp();

        // Assert
        _mockConsole.Verify(c => c.WriteLine("Bye"), Times.Once);
    }

    [Fact]
    public void RunApp_When_UnknownActionChosen_Then_MessageIsDisplayed()
    {
        // Arrange
        var availableActions = new Dictionary<string, string>
        {
            { "1", "Adding numbers" },
            { "2", "Subtracting numbers" }
        };

        _mockCalculatorEngine.Setup(c => c.GetAvailableActions()).Returns(availableActions);
        _mockConsole.SetupSequence(c => c.ReadLine()).Returns("999").Returns("q");

        // Act
        _consoleApp.RunApp();

        // Assert
        _mockConsole.Verify(c => c.WriteLine("Unknown action, please try again"), Times.Once);
    }
}