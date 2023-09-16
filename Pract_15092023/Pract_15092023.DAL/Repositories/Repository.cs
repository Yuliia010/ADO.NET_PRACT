using Pract_15092023.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract_15092023.DAL.Repositories
{
    public class Repository
    {
        public readonly StudentsContext context;

        public Repository()
        {
            context = new StudentsContext();
        }

        //public Student AddStudent(Student student)
        //{
        //    var result = context.Set<Student>().Add(student);
        //    context.SaveChanges();
        //    return result;
        //}
        public List<Student> GetAllStudents()
        {
            return context.Students.ToList();
        }
        public List<StudentCard> GetAllStudentCards()
        {
            return context.StudentCards.ToList();
        }
    }
}
