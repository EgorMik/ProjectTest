using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Presentation_Layer.Model
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

      

    }
}
