using Pract_15092023.DAL.Entity;
using Pract_15092023.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract_15092023
{
    public class StudentsProvider
    {
        private readonly IRepository<Student> _studentRepository;


        public StudentsProvider(IRepository<Student> repository)
        {
            _studentRepository = repository;
        }

        public void AddStudents(List<Student> students)
        {
            foreach (var student in students)
            {
                AddStudent(student);
            }
        }

        public void AddStudent(Student student) 
        {
            _studentRepository.Add(student);
        }

        public Student GetStudent(int id)
        {
            return _studentRepository.Get(id);
        }

        public IEnumerable<Student> GetStudents() 
        { 
            return  _studentRepository.GetAll(); 
        }
    }
}
