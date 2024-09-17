using OdontoManage.Core.Enums;

namespace OdontoManage.Application.Models.DTOs;

public class SchedulingCreateDto
{
    public Guid PatientId { get; set; }
    
    public Guid DentistId { get; set; }
    
    public DateDto Date { get; set; }
    
    public DateTime Start { get; set; }
    
    public DateTime End { get; set; }
    
    public string Observation { get; set; }
    
    public SchedulingType Type { get; set; }
    
    public SchedulingReturn Return { get; set; }
}