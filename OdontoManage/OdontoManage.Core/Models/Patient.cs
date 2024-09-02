using System.ComponentModel.DataAnnotations;
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
        
        [Required]
        public string Cpf { get; set; }
        
        [Required]
        public string Rg { get; set; }
        
        [Required]
        public string Phone { get; set; }
        
        [Required]
        public Gender Gender { get; set; }
        
        [Required]
        public bool IsForeign { get; set; }
        
        [Required]
        public string Address { get; set; }
        
        [Required]
        public string Document { get; set; }
        
        [Required]
        public DateOnly BirthDay { get; set; }
    }
}
