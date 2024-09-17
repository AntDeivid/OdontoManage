using AutoMapper;
using Moq;
using OdontoManage.Application.Models.DTOs;
using OdontoManage.Application.Services;
using OdontoManage.Core.Enums;
using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;

namespace OdontoManage.UnitTests;

[TestFixture]
public class TreatmentServiceTests
{
    private Mock<ITreatmentRepository> _treatmentRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private Mock<IPatientRepository> _patientRepositoryMock;
    private Mock<IDentistRepository> _dentistRepositoryMock;
    private Mock<IClinicalTreatmentRepository> _clinicalTreatmentRepositoryMock;
    private TreatmentService _treatmentService;

    [SetUp]
    public void SetUp()
    {
        _treatmentRepositoryMock = new Mock<ITreatmentRepository>();
        _mapperMock = new Mock<IMapper>();
        _patientRepositoryMock = new Mock<IPatientRepository>();
        _dentistRepositoryMock = new Mock<IDentistRepository>();
        _clinicalTreatmentRepositoryMock = new Mock<IClinicalTreatmentRepository>();

        _treatmentService = new TreatmentService(
            _treatmentRepositoryMock.Object,
            _mapperMock.Object,
            _patientRepositoryMock.Object,
            _dentistRepositoryMock.Object,
            _clinicalTreatmentRepositoryMock.Object
        );
    }

    [Test]
    public void Create_ShouldReturnCreatedTreatmentDto()
    {
        // Arrange
        var treatmentCreateDto = new TreatmentCreateDto
        {
            Plan = Plan.Basic,
            PatientId = Guid.NewGuid(),
            ClinicalTreatmentId = Guid.NewGuid(),
            Teeths = 4,
            Region = Region.UpperLeftCanine,
            Value = 200.0,
            Paid = false,
            InstallmentDueDate = new DateDto { Year = 2024, Month = 9, Day = 15 },
            DentistId = Guid.NewGuid()
        };

        var patient = new Patient { Id = treatmentCreateDto.PatientId };
        var dentist = new Dentist { Id = treatmentCreateDto.DentistId };
        var clinicalTreatment = new ClinicalTreatment { Id = treatmentCreateDto.ClinicalTreatmentId };

        var treatment = new Treatment
        {
            Plan = treatmentCreateDto.Plan,
            Patient = patient,
            Dentist = dentist,
            ClinicalTreatment = clinicalTreatment,
            Teeths = treatmentCreateDto.Teeths,
            Region = treatmentCreateDto.Region,
            Value = treatmentCreateDto.Value,
            Paid = treatmentCreateDto.Paid,
            InstallmentDueDate = new DateOnly(treatmentCreateDto.InstallmentDueDate.Year,
                treatmentCreateDto.InstallmentDueDate.Month, treatmentCreateDto.InstallmentDueDate.Day)
        };

        var createdTreatment = new Treatment
        {
            Id = Guid.NewGuid(),
            Plan = treatment.Plan,
            Patient = patient,
            Dentist = dentist,
            ClinicalTreatment = clinicalTreatment,
            Teeths = treatment.Teeths,
            Region = treatment.Region,
            Value = treatment.Value,
            Paid = treatment.Paid,
            InstallmentDueDate = treatment.InstallmentDueDate
        };

        var treatmentDto = new TreatmentDto
        {
            Id = createdTreatment.Id,
            Plan = createdTreatment.Plan,
            PatientId = createdTreatment.Patient.Id,
            ClinicalTreatmentId = createdTreatment.ClinicalTreatment.Id,
            Teeths = createdTreatment.Teeths,
            Region = createdTreatment.Region,
            Value = createdTreatment.Value,
            Paid = createdTreatment.Paid,
            InstallmentDueDate = new DateDto
            {
                Year = createdTreatment.InstallmentDueDate.Year, Month = createdTreatment.InstallmentDueDate.Month,
                Day = createdTreatment.InstallmentDueDate.Day
            },
            DentistId = createdTreatment.Dentist.Id
        };

        // Configurar os mocks
        _mapperMock.Setup(mapper => mapper.Map<Treatment>(treatmentCreateDto)).Returns(treatment);
        _mapperMock.Setup(mapper => mapper.Map<TreatmentDto>(createdTreatment)).Returns(treatmentDto);

        _patientRepositoryMock.Setup(repo => repo.GetById(treatmentCreateDto.PatientId)).Returns(patient);
        _dentistRepositoryMock.Setup(repo => repo.GetById(treatmentCreateDto.DentistId)).Returns(dentist);
        _clinicalTreatmentRepositoryMock.Setup(repo => repo.GetById(treatmentCreateDto.ClinicalTreatmentId))
            .Returns(clinicalTreatment);
        _treatmentRepositoryMock.Setup(repo => repo.Save(It.IsAny<Treatment>())).Returns(createdTreatment);

        // Act
        var result = _treatmentService.Create(treatmentCreateDto);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(treatmentDto.Id));
            Assert.That(result.Plan, Is.EqualTo(treatmentDto.Plan));
            Assert.That(result.PatientId, Is.EqualTo(treatmentDto.PatientId));
            Assert.That(result.ClinicalTreatmentId, Is.EqualTo(treatmentDto.ClinicalTreatmentId));
            Assert.That(result.Teeths, Is.EqualTo(treatmentDto.Teeths));
            Assert.That(result.Region, Is.EqualTo(treatmentDto.Region));
            Assert.That(result.Value, Is.EqualTo(treatmentDto.Value));
            Assert.That(result.Paid, Is.EqualTo(treatmentDto.Paid));
            Assert.That(result.InstallmentDueDate.Year, Is.EqualTo(treatmentDto.InstallmentDueDate.Year));
            Assert.That(result.InstallmentDueDate.Month, Is.EqualTo(treatmentDto.InstallmentDueDate.Month));
            Assert.That(result.InstallmentDueDate.Day, Is.EqualTo(treatmentDto.InstallmentDueDate.Day));
            Assert.That(result.DentistId, Is.EqualTo(treatmentDto.DentistId));
        });
    }
}