using System;
using FluentValidation.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Nop.Web.Validators;

namespace Nop.Web.Models
{
    [Validator(typeof(StudentValidator))]
    public class StudentModel
    {
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "年龄")]
        public int? Age { get; set; }

        public string JxCode { get; set; }
    }
}