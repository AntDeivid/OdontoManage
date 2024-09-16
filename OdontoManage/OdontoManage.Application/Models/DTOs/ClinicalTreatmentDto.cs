namespace OdontoManage.Application.Models.DTOs;

public class ClinicalTreatmentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double DefaultValue { get; set; }
}