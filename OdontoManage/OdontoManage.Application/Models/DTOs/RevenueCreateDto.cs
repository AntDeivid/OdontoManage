namespace OdontoManage.Application.Models.DTOs;

public class RevenueCreateDto
{
    public Guid PatientID { get; set; }
    public DateDto InstallmentDueDate { get; set; }
    public ICollection<TreatmentCreateDto> Treatment { get; set; }
    public string? Observation { get; set; }
}