using OdontoManage.Core.Enums;

namespace OdontoManage.Application.Models.DTOs;

public class TreatmentCreateDto
{
    public Plan Plan { get; set; }
    public Guid PatientId { get; set; }
    public Guid ClinicalTreatmentId { get; set; }
    public int Teeths { get; set; }
    public Region Region { get; set; }
    public double Value { get; set; }
    public bool Paid { get; set; }
    public DateDto InstallmentDueDate { get; set; }
    public Guid DentistId { get; set; }
}