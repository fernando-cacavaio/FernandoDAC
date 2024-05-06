using FernandoDAC.Domain.Entities;

namespace FernandoDAC.Domain.Repositories
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllPatients();

        Task CreatePatient(Patient patient);

        Task DeletePatient(int id);

        Task<Patient> GetPatientById(int id);

        Task UpdatePatient(Patient patient, int id);
    }
}