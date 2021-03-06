﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Presentation_Layer.Model
{
    public class Employee
    {
        public enum Job
        {
            [Display(Name = "Developer")]
            Developer,
            [Display(Name = "QA Engineer")]
            QA_Engineer,
            [Display(Name = "BA")]
            BA, 
            [Display(Name = "Manager")]
            Meneger
        }

        //Job prof = (Job)Enum.Parse(typeof(Job), "Developer, Manager");

        [Required]
        public int Id { get; set; }
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public Job Prof { get; set; }

    }
}

