using Microsoft.EntityFrameworkCore;
using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;
using OdontoManage.Infrastructure.Data;

namespace OdontoManage.Infrastructure.Repositories;

public class TreatmentRepository : GenericRepository<Treatment>, ITreatmentRepository
{
    public TreatmentRepository(OdontoManageDbContext dbContext) : base(dbContext) { }


    public IQueryable<Treatment> GetTreatmentsByPatientId(Guid patientId, int page, int pageSize)
    {
        return _dbContext.Treatments!
            .Where(t => t.Patient.Id == patientId)
            .Include(t => t.Patient)
            .Include(t => t.ClinicalTreatment)
            .Include(t => t.Dentist)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
    }
}