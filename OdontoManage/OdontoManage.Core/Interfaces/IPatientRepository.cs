using OdontoManage.Core.Models;
namespace OdontoManage.Core.Interfaces;

public interface IPatientRepository : IGenericRepository<Patient>
{
    Patient? GetPatientByCpf(string cpf);
}