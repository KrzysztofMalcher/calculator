using Calculator.Interfaces;

namespace ApiCalculator.Models;

public class ActionList
{
    private List<string> _operations;
    
    public ActionList(IComputingEngine computingEngine)
    {
        _operations = computingEngine.GetAvailableActions();
    }
    
    public List<string> GetAllActions<TList>(List<TList> operations)
    {
        List<string> actionList = new List<string>();
        int index = 1;
        foreach (var operation in operations)
        {
            actionList.Add($"{index}: {operation.ToString()}");
        }
        return actionList;
    }
    
    
}