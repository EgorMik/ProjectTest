using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entities
{
    public class Company
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string NameCompany { get; set; }

        [Required]
        public int SizeCompany { get; set; }

        [Required]
        public string FormOfIncorporation { get; set; }

        public Employee Employees { get; set; }

    }
}
