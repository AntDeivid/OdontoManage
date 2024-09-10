using OdontoManage.Core.Enums;

namespace OdontoManage.Application.Models.DTOs;

public class PatientUpdateDto
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string? Cpf { get; set; }
    public string? Rg { get; set; }
    public string Phone { get; set; }
    public Gender Gender { get; set; }
    public bool IsForeign { get; set; }
    public AddressUpdateDto Address { get; set; }
    public string? Document { get; set; }
    public DateDto Birthday { get; set; }
}