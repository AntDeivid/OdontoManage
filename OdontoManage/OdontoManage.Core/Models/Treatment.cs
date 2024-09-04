using OdontoManage.Core.Enums;

namespace OdontoManage.Core.Models;

public class Treatment : Base
{
    public Plan Plan { get; set; }
    public ClinicalTreatment ClinicalTreatment { get; set; }
    public int Teeths { get; set; }
    public Region Region { get; set; }
    public double Value { get; set; }
    // colcoar dentista
}