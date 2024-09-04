using OdontoManage.Core.Models;

namespace OdontoManage.Core.Interfaces;

public interface IDentistRepository : IGenericRepository<Dentist>
{
    Dentist GetByCpf(string cpf);
}