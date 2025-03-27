using Calculator.Interfaces;

namespace ApiCalculator.Models;

public class ActionList
{
    private List<(string Name, string Action)> _mappedOperations;
    private Dictionary<string, string> _availableOperations;
    
    public ActionList(IComputingEngine computingEngine)
    {
        _availableOperations = computingEngine.GetAvailableActions();
        _mappedOperations = GetMappedOperations(_availableOperations);
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
    
    public string? GetActionKey(string action)
    {
        return _mappedOperations.FirstOrDefault(x => x.Name == action).Action;
    }
    
    public string? GetActionValue(string actionKey)
    {
        return _availableOperations[actionKey];
    }
    
  public List<(string Name, string Action)> GetMappedActions()
    {
        return _mappedOperations;
    }
    
    private List<(string Name, string Action)> GetMappedOperations(Dictionary<string, string> availableOperations)
    {
        var mappedOperations = new List<(string Name, string Action)>();
        foreach (var operation in availableOperations)
        {
            mappedOperations.Add((operation.Key, operation.Key));
        }
        return mappedOperations;
    }
}