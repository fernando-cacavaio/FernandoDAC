using AutoMapper;
using FernandoDAC.Application.Core;
using FernandoDAC.Application.Patients;
using FernandoDAC.Application.Patients.Queries;
using FernandoDAC.Domain.Entities;
using FernandoDAC.Domain.Repositories;
using Moq;
using Shouldly;

namespace FernandoDAC.Application.UnitTests.Queries
{
    public class PatientsHandlerQueriesTest
    {
        private readonly IMapper _mapper;

        public PatientsHandlerQueriesTest()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfiles>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetPatientsList_ShouldReturnList()
        {
            var patients = new List<Patient>
            {
                new Patient
                {
                    Id = 1,
                    Name = "Fernando 1",
                    Address = "Address 1",
                    Phone = "12345678",
                    SIN = "123456789"
                },
                new Patient
                {
                    Id = 2,
                    Name = "Fernando 2",
                    Address = "Address 2",
                    Phone = "12345677",
                    SIN = "123456788"
                }
            };

            var mockRepo = new Mock<IPatientRepository>();

            mockRepo.Setup(r => r.GetAllPatients()).ReturnsAsync(patients);

            var handler = new List.Handler(mockRepo.Object, _mapper);

            var result = await handler.Handle(new List.Query(), CancellationToken.None);

            result.Value.ShouldBeOfType<List<PatientDto>>();

            result.Value.Count.ShouldBe(2);
        }

        [Fact]
        public async Task GetPatientById_ShouldReturnFirst()
        {
            var patient = new Patient

            {
                Id = 1,
                Name = "Fernando 1",
                Address = "Address 1",
                Phone = "12345678",
                SIN = "123456789"
            };

            var mockRepo = new Mock<IPatientRepository>();

            mockRepo.Setup(r => r.GetPatientById(1)).ReturnsAsync(patient);

            var handler = new Details.Handler(_mapper, mockRepo.Object);

            var result = await handler.Handle(new Details.Query { Id = 1 }, CancellationToken.None);

            result.Value.ShouldBeOfType<PatientDto>();

            Assert.Equal(result.Value.Id, 1);
        }

        [Fact]
        public async Task GetPatientById_ShouldReturnEmpty()
        {
            var patient = new Patient
            {
                Id = 1,
                Name = "Fernando 1",
                Address = "Address 1",
                Phone = "12345678",
                SIN = "123456789"
            };

            var mockRepo = new Mock<IPatientRepository>();

            mockRepo.Setup(r => r.GetPatientById(1)).ReturnsAsync(patient);

            var handler = new Details.Handler(_mapper, mockRepo.Object);

            var result = await handler.Handle(new Details.Query { Id = 22 }, CancellationToken.None);

            Assert.Null(result.Value);
        }
    }
}