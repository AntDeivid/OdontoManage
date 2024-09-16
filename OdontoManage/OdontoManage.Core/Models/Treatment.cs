using System.ComponentModel.DataAnnotations.Schema;
using OdontoManage.Core.Enums;

namespace OdontoManage.Core.Models;

public class Treatment : Base
{
    public Plan Plan { get; set; }
    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }
    [ForeignKey("ClinicalTreatmentId")]
    public virtual ClinicalTreatment ClinicalTreatment { get; set; }
    public int Teeths { get; set; }
    public Region Region { get; set; }
    public double Value { get; set; }
    public bool Paid { get; set; }
    public DateOnly InstallmentDueDate { get; set; }
    [ForeignKey("DentistId")]
    public virtual Dentist Dentist { get; set; }
}