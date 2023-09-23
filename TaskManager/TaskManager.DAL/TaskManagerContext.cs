using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DAL.Entity;

namespace TaskManager.DAL
{
    public class TaskManagerContext :DbContext
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=TaskManager; Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TicketArchive> TicketArchives { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
            .HasMany(u => u.Ticket) 
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);


            var user = new User()
            {
                Id = 1,
                login = "yuliia",
                Name = "Yuliia Shlgnv",
                password = "password",
                IsAdmin = true
            };

            modelBuilder.Entity<User>().HasData(user);

            var ticket1 = new Ticket()
            {
                Id = 1,
                Summary = "Add first test ticket",
                Priority = 1,
                Description = "To do test Ticket",
                DueDateTime = DateTime.Now.AddDays(12),
                UserId = user.Id
            };

            var ticket2 = new Ticket()
            {
                Id = 2,
                Summary = "Add second test ticket",
                Priority = 1,
                Description = "To do test Ticket num2",
                DueDateTime = DateTime.Now.AddDays(7),
                UserId = user.Id
            };

            modelBuilder.Entity<Ticket>().HasData(ticket1, ticket2);

            base.OnModelCreating(modelBuilder);

        }

    }
}
