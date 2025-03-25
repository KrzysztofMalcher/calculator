using ApiCalculator.infrastructure.Database;
using ApiCalculator.Models;
using ApiCalculator.Service;
using Calculator.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCalculator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalculatorController : Controller
{
    private readonly IComputingEngine _calculatorEngine;
    private readonly List<string> _availableActions;
    private readonly OperationService _operationService;
    
    public  CalculatorController(IComputingEngine calculatorEngine, OperationsDbContext context)
    {
        _calculatorEngine = calculatorEngine;
        _operationService = new OperationService(context);
        _availableActions = _calculatorEngine.GetAvailableActions();
    }

    [HttpGet]
    [Route("/api/[controller]/info")]
    public ActionResult<Dictionary<string, string>> Index()
    {
        return Ok(_availableActions);
    }
    
    [HttpGet]
    [Route("/api/[controller]/history")]
    public async Task<ActionResult<List<Operation>>>GetAllOperations()
    {
        var operationList = await _operationService.GetAllOperations();
        return Ok(operationList);
    }
    
    [HttpGet]
    [Route("/api/[controller]/history/{id}")]
    public async Task<ActionResult<List<Operation>>>GetOperation(int id)
    {
        var operation = await _operationService.GetOperationById(id);
        
        if(operation != null)
        {
            return Ok(operation);
        }

        return NotFound();
    }

    
    [HttpPost]
    [Route("/api/[controller]/compute")]
    public async Task<ActionResult<string>> Calculate([FromBody] ComputeRequest computeRequest)
    {
        if(int.TryParse(computeRequest.Action, out int actionNumber) && actionNumber > 0 && actionNumber <= _availableActions.Count)
        {   
            actionNumber = actionNumber - 1;
            float? result;
            var operation = new Operation
            {
                Action = _availableActions[actionNumber],
                FirstOperand = computeRequest.FirstOperand,
                SecondOperand = computeRequest.SecondOperand
            };
            try
            {
                if (float.TryParse(computeRequest.FirstOperand, out float firstOperand) && float.TryParse(computeRequest.SecondOperand, out float secondOperand))
                {
                    _calculatorEngine.ValidateInput(actionNumber, [firstOperand, secondOperand]);
                    result = _calculatorEngine.Compute(actionNumber, [firstOperand, secondOperand]);
                    operation.Result = result.ToString() ?? "";
                    await _operationService.AddOperation(operation);
                    return Ok("Your result is: " + result);
                }
                operation.Result = "Provided operands are not valid numbers";
                await _operationService.AddOperation(operation);
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