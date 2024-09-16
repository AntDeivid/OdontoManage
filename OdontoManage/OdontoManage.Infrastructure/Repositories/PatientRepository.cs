using Microsoft.EntityFrameworkCore;
using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;
using OdontoManage.Infrastructure.Data;

namespace OdontoManage.Infrastructure.Repositories;

public class PatientRepository : GenericRepository<Patient>, IPatientRepository
{
    public PatientRepository(OdontoManageDbContext dbContext) : base(dbContext){ }

    public Patient? GetPatientByCpfWithAddress(string cpf)
    {
        return (_dbContext.Patients ?? throw new InvalidOperationException()).
            FirstOrDefault(x => x.Cpf == cpf);
    }

    public Patient? GetPatientByIdWithAddress(Guid id)
    {
        return (_dbContext.Patients ?? throw new InvalidOperationException())
            .Include(p => p.Address)
            .FirstOrDefault(x => x.Id == id);
    }
}