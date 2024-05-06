using AutoMapper;
using FernandoDAC.Application.Core;
using FernandoDAC.Application.Patients;
using FernandoDAC.Application.Patients.Commands;
using FernandoDAC.Domain.Entities;
using FernandoDAC.Domain.Repositories;
using FluentValidation.TestHelper;
using Moq;

namespace FernandoDAC.Application.UnitTests.Commands
{
    public class PatientsHandlerCommandsTest
    {
        private readonly IMapper _mapper;
        private readonly Patient _patients;

        public PatientsHandlerCommandsTest()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfiles>();
            });

            _mapper = mapperConfig.CreateMapper();

            _patients = new Patient()
            {
                Name = "Fernando Teste 1",
                Address = "Fernando Address",
                Phone = "12345678",
                SIN = "123456788",
            };
        }

        [Fact]
        public async Task Invalid_Patient_SIN_Existed()
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

            mockRepo.Setup(r => r.CreatePatient(_patients));
            mockRepo.Setup(r => r.GetAllPatients()).ReturnsAsync(patients);

            var handler = new Create.Handler(mockRepo.Object);

            var result = await handler.Handle(new Create.Command() { Patient = _patients }, CancellationToken.None);

            Assert.Equal(result.Error, "Patient already exists with this SIN number");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Name_cannot_be_empty(string name)
        {
            // Arrange
            var request = new Patient
            {
                Name = name
            };
            var validator = new PatientValidator();

            // Act
            TestValidationResult<Patient> result =
                validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Theory]
        [InlineData(null)]
        public void SIN_cannot_be_empty(string sin)
        {
            // Arrange
            var request = new Patient
            {
                Name = "Teste",
                SIN = sin
            };
            var validator = new PatientValidator();

            // Act
            TestValidationResult<Patient> result =
                validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SIN);
        }

        [Theory]
        [InlineData("2222")]
        public void SIN_cannot_be_less_than_9(string sin)
        {
            // Arrange
            var request = new Patient
            {
                Name = "Teste",
                SIN = sin
            };
            var validator = new PatientValidator();

            // Act
            TestValidationResult<Patient> result =
                validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SIN);
        }
    }
}