using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using OdontoManage.Application.Models.DTOs;
using OdontoManage.Application.Services;
using OdontoManage.Core.Enums;
using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;

namespace OdontoManage.UnitTests;

[TestFixture]
public class PatientServiceTests
{

    private Mock<IPatientRepository> _patientRepositoryMock;
    private Mock<IAddressRespository> _addressRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private Mock<ILogger<PatientService>> _loggerMock;
    private PatientService _patientService;

    [SetUp]
    public void SetUp()
    {
        _patientRepositoryMock = new Mock<IPatientRepository>();
        _addressRepositoryMock = new Mock<IAddressRespository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<PatientService>>();

        _patientService = new PatientService(
            _patientRepositoryMock.Object,
            _mapperMock.Object,
            _loggerMock.Object,
            _addressRepositoryMock.Object
        );
    }

    [Test]
    public void CreatePatient_WhenValidPatient_ShouldReturnPatientDto()
    {
        // Arrange
        var patientCreateDto = new PatientCreateDto
        {
            Name = "John Doe",
            Age = 30,
            Cpf = "12345678900",
            Phone = "123456789",
            Gender = Gender.Male,
            IsForeign = false,
            Address = new AddressCreateDto
            {
                Street = "123 Main St",
                City = "City",
                State = "State",
                ZipCode = "12345",
                Neighborhood = "Neighborhood"
            },
            Document = "Document",
            Birthday = new DateDto { Year = 1994, Month = 5, Day = 15 }
        };

        var patient = new Patient
        {
            Id = Guid.NewGuid(),
            Name = patientCreateDto.Name,
            Age = patientCreateDto.Age,
            Cpf = patientCreateDto.Cpf,
            Phone = patientCreateDto.Phone,
            Gender = patientCreateDto.Gender,
            IsForeign = patientCreateDto.IsForeign,
            Document = patientCreateDto.Document,
            BirthDay = new DateOnly(patientCreateDto.Birthday.Year, patientCreateDto.Birthday.Month, patientCreateDto.Birthday.Day),
            Address = new Address
            {
                Id = Guid.NewGuid(),
                Street = patientCreateDto.Address.Street,
                City = patientCreateDto.Address.City,
                State = patientCreateDto.Address.State,
                ZipCode = patientCreateDto.Address.ZipCode,
                Neighborhood = patientCreateDto.Address.Neighborhood
            }
        };

        var patientDto = new PatientDto
        {
            Id = patient.Id,
            Name = patient.Name,
            Age = patient.Age,
            Cpf = patient.Cpf,
            Phone = patient.Phone,
            Gender = patient.Gender,
            IsForeign = patient.IsForeign,
            Document = patient.Document,
            Birthday = patient.BirthDay,
            Address = new AddressDto
            {
                Street = patient.Address.Street,
                City = patient.Address.City,
                State = patient.Address.State,
                zipCode = patient.Address.ZipCode,
                Neighborhood = patient.Address.Neighborhood
            }
        };

        _patientRepositoryMock.Setup(repo => repo.GetPatientByCpfWithAddress(It.IsAny<string>())).Returns((Patient)null);
        _mapperMock.Setup(mapper => mapper.Map<Patient>(It.IsAny<PatientCreateDto>())).Returns(patient);
        _mapperMock.Setup(mapper => mapper.Map<Address>(It.IsAny<AddressCreateDto>())).Returns(patient.Address);
        _mapperMock.Setup(mapper => mapper.Map<PatientDto>(It.IsAny<Patient>())).Returns(patientDto);

        _patientRepositoryMock.Setup(repo => repo.Save(It.IsAny<Patient>())).Returns(patient);
        _addressRepositoryMock.Setup(repo => repo.Save(It.IsAny<Address>())).Returns(patient.Address);

        // Act
        var result = _patientService.Create(patientCreateDto);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(patientDto));
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(patientDto.Id));
            Assert.That(result.Name, Is.EqualTo(patientDto.Name));
            Assert.That(result.Age, Is.EqualTo(patientDto.Age));
            Assert.That(result.Cpf, Is.EqualTo(patientDto.Cpf));
            Assert.That(result.Phone, Is.EqualTo(patientDto.Phone));
            Assert.That(result.Gender, Is.EqualTo(patientDto.Gender));
            Assert.That(result.IsForeign, Is.EqualTo(patientDto.IsForeign));
            Assert.That(result.Document, Is.EqualTo(patientDto.Document));
            Assert.That(result.Birthday, Is.EqualTo(patientDto.Birthday));
            Assert.That(result.Address, Is.Not.Null);
        });
        Assert.That(result.Address, Is.EqualTo(patientDto.Address));
        Assert.Multiple(() =>
        {
            Assert.That(result.Address.Street, Is.EqualTo(patientDto.Address.Street));
            Assert.That(result.Address.City, Is.EqualTo(patientDto.Address.City));
            Assert.That(result.Address.State, Is.EqualTo(patientDto.Address.State));
            Assert.That(result.Address.zipCode, Is.EqualTo(patientDto.Address.zipCode));
            Assert.That(result.Address.Neighborhood, Is.EqualTo(patientDto.Address.Neighborhood));
        });
    }
}