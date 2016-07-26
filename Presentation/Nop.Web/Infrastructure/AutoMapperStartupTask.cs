using AutoMapper;
using Nop.Core.Domain;
using Nop.Core.Domain.Customers;
using Nop.Core.Infrastructure;
using Nop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Infrastructure
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<StudentModel, Student>().ForMember(d=>d.Id,op=>op.Ignore()));
            //Mapper.Initialize(cfg => cfg.CreateMap<RegisterViewModel, Customer>());
        }

        public int Order
        {
            get { return 0; }
        }
    }
}