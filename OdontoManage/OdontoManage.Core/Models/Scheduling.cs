using System.ComponentModel.DataAnnotations.Schema;
using OdontoManage.Core.Enums;

namespace OdontoManage.Core.Models;

public class Scheduling : Base
{
    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }
    
    [ForeignKey("DentistId")]
    public virtual Dentist Dentist { get; set; }
    
    public DateOnly Date { get; set; }
    
    public DateTime Start { get; set; }
    
    public DateTime End { get; set; }
    
    public string Observation { get; set; }
    
    public SchedulingType Type { get; set; }
    
    public SchedulingReturn Return { get; set; }
}