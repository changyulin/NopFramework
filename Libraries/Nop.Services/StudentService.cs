using Nop.Core.Domain;
using Nop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> studentRepository;
        public StudentService(IRepository<Student> studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public void InsertStudent(Student student)
        {
            this.studentRepository.Insert(student);
        }
    }
}
