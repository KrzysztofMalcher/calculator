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
    private readonly OperationService _operationService;
    private readonly ActionList _actionList;
    
    public  CalculatorController(IComputingEngine calculatorEngine, OperationsDbContext context)
    {
        _calculatorEngine = calculatorEngine;
        _operationService = new OperationService(context);
        _actionList = new ActionList(calculatorEngine);
    }

    [HttpGet]
    [Route("/api/[controller]/info")]
    public ActionResult<Dictionary<string, string>> Index()
    {
        Dictionary<string, string> displayActions = new Dictionary<string, string>();
        foreach (var action in _actionList.GetMappedActions())
        {
            displayActions.Add(action.Name, _actionList.GetActionValue(action.Action));
        }
        return Ok(displayActions);
    }
    
    [HttpGet]
    [Route("/api/[controller]/history")]
    public async Task<ActionResult<List<Operation>>>GetAllOperations()
    {
        var operationList = await _operationService.GetAllItems();
        return Ok(operationList);
    }
    
    [HttpGet]
    [Route("/api/[controller]/history/{id}")]
    public async Task<ActionResult<List<Operation>>>GetOperation(int id)
    {
        var operation = await _operationService.GetItemById(id);
        
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
        var mappedAction = _actionList.GetActionKey(computeRequest.Action);
        if (mappedAction != null) 
        {   
            var operation = new Operation
            {
                Action = _actionList.GetActionValue(mappedAction),
                FirstOperand = computeRequest.FirstOperand,
                SecondOperand = computeRequest.SecondOperand
            };
            try
            {
                if (float.TryParse(computeRequest.FirstOperand, out float firstOperand) && float.TryParse(computeRequest.SecondOperand, out float secondOperand))
                {
                    _calculatorEngine.ValidateInput(mappedAction, [firstOperand, secondOperand]);
                    float? result = _calculatorEngine.Compute(mappedAction, [firstOperand, secondOperand]);
                    operation.Result = result.ToString() ?? "";
                    await _operationService.AddItem(operation);
                    return Ok("Your result is: " + result);
                }
                operation.Result = "Provided operands are not valid numbers";
                await _operationService.AddItem(operation);
                return BadRequest("Provided operands are not valid numbers.");
            }
            catch (Exception ex)
            {
                operation.Result = ex.Message;
                await _operationService.AddItem(operation);
                return BadRequest(ex.Message);
            }
        } 
        return BadRequest("Invalid action.");
    }
}