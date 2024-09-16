namespace OdontoManage.Core.Models;

public class ClinicalTreatment : Base
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double DefaultValue { get; set; }
}