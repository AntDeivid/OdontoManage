namespace OdontoManage.Core.Models;

public class Revenue : Base
{
    public virtual Patient Patient { get; set; }
    public DateOnly InstallmentDueDate { get; set; }
    public virtual ICollection<Treatment> Treatments { get; set; }
    public string? Observation { get; set; }
}