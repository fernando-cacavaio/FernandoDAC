using System.Data;
using Dapper;
using FernandoDAC.Domain.Entities;
using FernandoDAC.Domain.Repositories;

namespace FernandoDAC.Persistence.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly FernandoDACDapperDbContext _context;

        public PatientRepository(FernandoDACDapperDbContext context)
        {
            _context = context;
        }

        public async Task<List<Patient>> GetAllPatients()
        {
            var query = "SELECT * FROM Patients";
            using (var connection = _context.CreateConnection())
            {
                var patients = await connection.QueryAsync<Patient>(query);
                return patients.ToList();
            }
        }

        public async Task CreatePatient(Patient patient)
        {
            string query = "Insert into Patients(name,SIN,address,phone) values (@name,@sin,@address,@phone)";
            var parameters = new DynamicParameters();
            parameters.Add("name", patient.Name, DbType.String);
            parameters.Add("sin", patient.SIN, DbType.String);
            parameters.Add("address", patient.Address, DbType.String);
            parameters.Add("phone", patient.Phone, DbType.String);
            using (var connectin = _context.CreateConnection())
            {
                await connectin.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeletePatient(int id)
        {
            string query = "Delete From Patients where id=@id";
            using (var connectin = _context.CreateConnection())
            {
                await connectin.ExecuteAsync(query, new { id });
            }
        }

        public async Task<Patient> GetPatientById(int id)
        {
            string query = "Select * From Patients where id=@id";
            using (var connectin = _context.CreateConnection())
            {
                var patientsList = await connectin.QueryFirstOrDefaultAsync<Patient>(query, new { id });
                return patientsList!;
            }
        }

        public async Task UpdatePatient(Patient patient, int id)
        {
            string query = "update Patients set name=@name,SIN=@sin,address=@address,phone=@phone where id=@id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);
            parameters.Add("name", patient.Name, DbType.String);
            parameters.Add("sin", patient.SIN, DbType.String);
            parameters.Add("address", patient.Address, DbType.String);
            parameters.Add("phone", patient.Phone, DbType.String);
            using (var connectin = _context.CreateConnection())
            {
                await connectin.ExecuteAsync(query, parameters);
            }
        }
    }
}