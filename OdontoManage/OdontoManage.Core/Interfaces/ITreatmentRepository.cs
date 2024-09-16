using OdontoManage.Core.Models;

namespace OdontoManage.Core.Interfaces;

public interface ITreatmentRepository : IGenericRepository<Treatment>
{
    IQueryable<Treatment> GetTreatmentsByPatientId(Guid patientId, int page, int pageSize);
}