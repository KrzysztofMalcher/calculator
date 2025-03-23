using Calculator.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiCalculator.Controllers;

[Controller]
[Route("api/[controller]")]
public class CalculatorController : Controller
{
    private readonly IComputingEngine _calculatorEngine;
    private readonly Dictionary<string, string> _availableActions;
    
    public  CalculatorController(IComputingEngine calculatorEngine)
    {
        _calculatorEngine = calculatorEngine;
        _availableActions = _calculatorEngine.GetAvailableActions();
    }

    [HttpGet]
    public ActionResult<Dictionary<string, string>> Index()
    {
        return Ok(_availableActions);
    }

    [HttpGet]
    [Route("/api/[controller]/{operation}/{firstnumber}/{secondnumber}")]
    public ActionResult<string> Calculate(string operation, int firstnumber, int secondnumber)
    {
        float? result;
        if (_availableActions.ContainsKey(operation))
        {
            try
            {
                _calculatorEngine.ValidateInput(operation, [firstnumber, secondnumber]);
                result = _calculatorEngine.Compute(operation, [firstnumber, secondnumber]);
                return Ok("Your result is: " + result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
        return BadRequest("Invalid input data.");
    }
}