using System;
using System.Collections.Generic;
using Calculator.Interfaces;
using Moq;
using Xunit;

namespace Calculator.Tests
{
    public class ConsoleAppTests
    {
        [Fact]
        public void RunApp_ShouldPrintWelcomeMessage()
        {
            // Arrange
            var mockConsole = new Mock<IConsole>();
            var mockComputingEngine = new Mock<IComputingEngine>();
            var app = new ConsoleApp(mockComputingEngine.Object, mockConsole.Object);

            // Act
            app.RunApp();

            // Assert
            mockConsole.Verify(c => c.WriteLine(It.Is<string>(s => s.Contains("Welcome in my simple calculator"))), Times.Once);
        }

        [Fact]
        public void RunApp_ShouldHandleValidActionAndComputeResult()
        {
            // Arrange
            var mockConsole = new Mock<IConsole>();
            var mockCalculatorEngine = new Mock<IComputingEngine>();
            var app = new ConsoleApp(mockCalculatorEngine.Object, mockConsole.Object);

            // Mock user input
            mockConsole.SetupSequence(c => c.ReadLine())
                .Returns("1")  // Action 1 (Add)
                .Returns("5")  // First number
                .Returns("3"); // Second number

            // Mock available actions
            mockCalculatorEngine.Setup(c => c.GetAvailableActions())
                .Returns(new Dictionary<string, string> { { "1", "Add" }, { "q", "Exit" } });

            mockCalculatorEngine.Setup(c => c.Compute("1", It.IsAny<float[]>()))
                .Returns(8f);

            // Act
            app.RunApp();

            // Assert
            mockConsole.Verify(c => c.WriteLine(It.Is<string>(s => s.Contains("Calculation result: 8"))), Times.Once);
        }

        [Fact]
        public void RunApp_ShouldHandleInvalidActionAndShowMessage()
        {
            // Arrange
            var mockConsole = new Mock<IConsole>();
            var mockCalculatorEngine = new Mock<IComputingEngine>();
            var app = new ConsoleApp(mockCalculatorEngine.Object, mockConsole.Object);

            // Mock user input
            mockConsole.SetupSequence(c => c.ReadLine())
                .Returns("InvalidAction")
                .Returns("q"); // Exit

            // Mock available actions
            var availableActions = new Dictionary<string, string> { { "1", "Add" }, { "q", "Exit" } };

            // Act
            app.RunApp();

            // Assert
            mockConsole.Verify(c => c.WriteLine(It.Is<string>(s => s.Contains("Unknown action, please try again"))), Times.Once);
        }
    }
}
