namespace OdontoManage.Core.Models;

public class Revenue : Base
{
    public Patient Patient { get; set; }
    public DateOnly InstallmentDueDate { get; set; }
    public ICollection<Treatment> Treatments { get; set; }
    public string? Observation { get; set; }
}