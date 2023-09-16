using Newtonsoft.Json;
using Pract_15092023.DAL.Entity;
using Pract_15092023.DAL.Repositories;

namespace Pract_15092023
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            string jsonPath = "C:\\Users\\Yuliia\\Source\\Repos\\Yuliia010\\ADO.NET_PRACT\\Pract_15092023\\Pract_15092023\\Students.json";
            string contents = File.ReadAllText(jsonPath);
            TestObject testStudents = JsonConvert.DeserializeObject<TestObject>(contents);

            List<Student> students = testStudents.Students;

            //var repository = new Repository();
            //var students = repository.GetAllStudents();
            //var studentsCards = repository.GetAllStudentCards();



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

    public class TestObject
    {
        public List<Student> Students;
    }
}