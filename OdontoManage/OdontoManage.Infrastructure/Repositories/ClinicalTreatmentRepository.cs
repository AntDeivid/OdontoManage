using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;
using OdontoManage.Infrastructure.Data;

namespace OdontoManage.Infrastructure.Repositories;

public class ClinicalTreatmentRepository : GenericRepository<ClinicalTreatment>, IClinicalTreatmentRepository
{
    public ClinicalTreatmentRepository(OdontoManageDbContext dbContext) : base(dbContext)
    {
    }
}