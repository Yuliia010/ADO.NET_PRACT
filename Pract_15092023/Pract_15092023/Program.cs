using Pract_15092023.DAL.Repositories;

namespace Pract_15092023
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            var repository = new Repository();
            var students = repository.GetAllStudents();
            var studentsCards = repository.GetAllStudentCards();

            

            foreach ( var student in students )
            {
                Console.WriteLine(
                    $@"Student Id: {student.Id}
            Name: {student.Name} {student.LastName},
            DateBirth: {student.BirthDate},
            Mail: {student.MailAddress},
            Phone: {student.PhoneNumber},
            StudentCard: {student.StudentCard.CardNumber}, exp.date: {student.StudentCard.ExpireDate}");

            }
        }
    }
}