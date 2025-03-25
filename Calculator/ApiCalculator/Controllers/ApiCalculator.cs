using ApiCalculator.infrastructure.Database;
using ApiCalculator.Models;
using Calculator.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCalculator.Controllers;

[Controller]
[Route("api/[controller]")]
public class CalculatorController : Controller
{
    private readonly IComputingEngine _calculatorEngine;
    private readonly Dictionary<string, string> _availableActions;
    private readonly OperationsDbContext _context;
    
    public  CalculatorController(IComputingEngine calculatorEngine, OperationsDbContext context)
    {
        _calculatorEngine = calculatorEngine;
        _context = context;
        _availableActions = _calculatorEngine.GetAvailableActions();
    }

    [HttpGet]
    [Route("/api/[controller]/getinfo")]
    public ActionResult<Dictionary<string, string>> Index()
    {
        return Ok(_availableActions);
    }
    
    [HttpGet]
    [Route("/api/[controller]/gethistory")]
    public async Task<ActionResult<List<Operation>>>GetHistory()
    {
        var operationList = await _context.Operations.ToListAsync();
        return Ok(operationList);
    }
    
    [HttpPost]
    [Route("/api/[controller]/compute")]
    public async Task<ActionResult<string>> Calculate([FromBody] ComputeRequest computeRequest)
    {
        if (_availableActions.ContainsKey(computeRequest.Action))
        {   
            float? result;
            var operation = new Operation
            {
                Action = computeRequest.Action,
                FirstOperand = computeRequest.FirstOperand,
                SecondOperand = computeRequest.SecondOperand
            };

            try
            {
                if (float.TryParse(computeRequest.FirstOperand, out float firstOperand) && float.TryParse(computeRequest.SecondOperand, out float secondOperand))
                {
                    _calculatorEngine.ValidateInput(computeRequest.Action, [firstOperand, secondOperand]);
                    result = _calculatorEngine.Compute(computeRequest.Action, [firstOperand, secondOperand]);
                    operation.Result = result.ToString();
                    _context.Add(operation);
                    await _context.SaveChangesAsync();
                    return Ok("Your result is: " + result);
                }
                operation.Result = "Provided operands are not valid numbers";
                _context.Add(operation);
                await _context.SaveChangesAsync();
                return BadRequest("Provided operands are not valid numbers.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
        return BadRequest("Invalid action.");
    }

}