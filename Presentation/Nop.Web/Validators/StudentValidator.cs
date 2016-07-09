using FluentValidation;
using Nop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Validators
{
    public class StudentValidator : AbstractValidator<StudentModel>
    {
        public StudentValidator()
        {
            RuleFor(s => s.Name).NotEmpty().WithMessage("姓名不能为空！");
            RuleFor(s => s.Age).GreaterThan(0).LessThan(100).WithMessage("性别必须为1-100的数字！");
        }
    }
}