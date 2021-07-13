﻿using EmployeeMGNT.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMGNT.ViewModels
{
    public class EmployeeCreateViewModel
    {

        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 charactors")]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+[.])+[a-z]{2,5}$", ErrorMessage = "Enter a valid Email")]
        [Display(Name = "Email ID")]
        public string Email { get; set; }
        [Required]
        public Dept? Department { get; set; }
        public IFormFile Photo { get; set; }
    }
}
