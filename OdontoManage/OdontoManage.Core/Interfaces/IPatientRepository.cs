using OdontoManage.Core.Models;
namespace OdontoManage.Core.Interfaces;

public interface IPatientRepository : IGenericRepository<Patient>
{
    Patient? GetPatientByCpfWithAddress(string cpf);
    Patient? GetPatientByIdWithAddress(Guid id);
}