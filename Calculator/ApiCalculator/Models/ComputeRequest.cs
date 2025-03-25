using System.ComponentModel.DataAnnotations;

namespace ApiCalculator.Models;

public class ComputeRequest
{
    [Required]
    public string Action { get; set; }
    [Required]
    public string FirstOperand { get; set; }
    [Required]
    public string SecondOperand { get; set; }

}