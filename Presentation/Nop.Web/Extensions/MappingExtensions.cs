using AutoMapper;
using Nop.Core.Domain;
using Nop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Extensions
{
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public static Student ToEntity(this StudentModel model)
        {
            return model.MapTo<StudentModel, Student>();
        }
    }
}