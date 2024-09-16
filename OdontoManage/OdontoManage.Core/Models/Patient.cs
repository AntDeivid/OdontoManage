using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using OdontoManage.Core.Enums;

namespace OdontoManage.Core.Models
{
    public class Patient : Base
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public int Age { get; set; }
        
        public string? Cpf { get; set; }
        
        public string? Rg { get; set; }
        
        public string Phone { get; set; }
        
        [Required]
        public Gender Gender { get; set; }
        
        [Required]
        public bool IsForeign { get; set; }
        
        [Required]
        public string? Document { get; set; }
        
        [Required]
        public DateOnly BirthDay { get; set; }
        
        [Required]
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }
    }
}
