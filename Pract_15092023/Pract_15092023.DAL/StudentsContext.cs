using Microsoft.EntityFrameworkCore;
using Pract_15092023.DAL.Entity;
using System.Configuration;

namespace Pract_15092023.DAL
{

    public class StudentsContext : DbContext
    {
        //static string connectionString => ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=Students; Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public DbSet<Student> Students { get; set; }

        public DbSet <StudentCard> StudentCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
            .HasOne(s => s.StudentCard) 
            .WithOne(s => s.Student) 
            .HasForeignKey<StudentCard>(sc => sc.Id);
            
            //var studCard1 = new StudentCard()
            //{
            //    Id = 1,
            //    CardNumber = "HG-145899",
            //    ExpireDate = DateTime.Now.AddMonths(15),
            //    IsActive = true

            //};
            //var studCard2 = new StudentCard()
            //{
            //    Id = 2,
            //    CardNumber = "AG-145111",
            //    ExpireDate = DateTime.Now.AddMonths(12),
            //    IsActive = true

            //};
            //var studCard3 = new StudentCard()
            //{
            //    Id = 3,
            //    CardNumber = "AG-145111",
            //    ExpireDate = DateTime.Now.AddMonths(-2),
            //    IsActive = false

            //};
            //modelBuilder.Entity<StudentCard>().HasData(studCard1, studCard2, studCard3);

            //modelBuilder.Entity<Student>().HasData(
            //new Student()
            //{
            //    Id = 1,
            //    Name = "Ivan",
            //    LastName = "Ivanov",
            //    BirthDate = DateTime.Now.AddYears(-18),
            //    MailAddress = "ii@gmail.com",
            //    PhoneNumber = "380665544488"
            //},
            //new Student()
            //{
            //    Id = 2,
            //    Name = "Ivan",
            //    LastName = "Ivanov",
            //    BirthDate = DateTime.Now.AddYears(-20),
            //    MailAddress = "ii@gmail.com",
            //    PhoneNumber = "380665544488"
            //},
            //new Student()
            //{
            //    Id = 3,
            //    Name = "Ivan",
            //    LastName = "Ivanov",
            //    BirthDate = DateTime.Now.AddYears(-24),
            //    MailAddress = "ii@gmail.com",
            //    PhoneNumber = "380665544488"
            //});

            base.OnModelCreating(modelBuilder);

        }
    }
}