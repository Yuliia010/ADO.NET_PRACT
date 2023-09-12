using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace Pract_30082023_2
{
    internal class Program
    {
        private static string connectionString => ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();


        static async Task Main(string[] args)
        {
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", SqlClientFactory.Instance);

            DbProviderFactory factory = null;
            Console.WriteLine("Select Db:\n1 - ms sql\n2 - oracle");
            string selection = Console.ReadLine();
            if (selection == "1")
            {

                factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["DefaultConnection"].ProviderName);

                try
                {
                   await Menu(factory);

                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Connection error: {ex.Message}");
                }
            }
            else if (selection == "2")
            {
                factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["OracleDb"].ProviderName);

            }
            else
            {
                factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["DefaultConnection"].ProviderName);

            }
            Console.WriteLine("Disconnected from database.");

            
        }

        static async Task Menu(DbProviderFactory factory)
        {
            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                await connection.OpenAsync();
                do
                {
                    Console.Clear();
                    Console.WriteLine("Connected to database 'Students Marks'.");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("To display all information                -  enter 1");
                    Console.WriteLine("To display the full name of all students  -  enter 2");
                    Console.WriteLine("To display all average grades             -  enter 3");
                    Console.WriteLine("To display names of all students the minimum grade,\n" +
                                      "greater than specified                    -  enter 4");
                    Console.WriteLine("To display names of all subjects with\n" +
                                      "minimum averages assessments.\n" +
                                      "Item names must be unique                 -  enter 5");
                    Console.WriteLine("To update student data                    -  enter 6");
                    Console.WriteLine("To delete student data                    -  enter 7");
                    Console.WriteLine("-------------------------------------------------");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            await MeasureExecutionTime(() => AllInfo(connection, factory));
                            break;
                        case "2":
                            await MeasureExecutionTime(() => FullName(connection, factory));
                            break;
                        case "3":
                            await MeasureExecutionTime(() => AvMarks(connection, factory));
                            break;
                        case "4":
                            await MeasureExecutionTime(() => AvMarksValue(connection, factory));
                            break;
                        case "5":
                            await MeasureExecutionTime(() => uniqueMin(connection, factory));
                            break;
                        case "6":
                            await MeasureExecutionTime(() => UpdateStudentData(connection, factory));
                            break;
                        case "7":
                            await MeasureExecutionTime(() => DeleteStudentData(connection, factory));
                            break;
                        case "0":
                            return;
                    }
                    Console.ReadKey();
                } while (true);

            }
        }

        static void Show(DbDataReader reader, params string[] info)
        {
            while (reader.Read())
            {
                for (int i = 0; i < info.Length; i++)
                {
                    Console.WriteLine($"{reader[info[i]]}");
                }
                Console.WriteLine("-------------------------------------------------");
            }
        }

        static async Task MeasureExecutionTime(Func<Task> action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await action();
            stopwatch.Stop();
            Console.WriteLine($"Execution time: {stopwatch.Elapsed.TotalSeconds} seconds");
        }
        static async Task UpdateStudentData(DbConnection connection, DbProviderFactory factory)
        {
            Console.WriteLine("Enter student ID to update:");
            int studentId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter new data for the student:");
            Console.Write("Name: ");
            string name = Console.ReadLine();

            using (DbCommand command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = $"UPDATE Students SET Name = '{name}' WHERE Id = {studentId};";

                int rowsAffected = await command.ExecuteNonQueryAsync();
                Console.WriteLine($"{rowsAffected} rows updated.");
            }
        }

        static async Task DeleteStudentData(DbConnection connection, DbProviderFactory factory)
        {
            Console.WriteLine("Enter student ID to delete:");
            int studentId = int.Parse(Console.ReadLine());

            using (DbCommand command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = $"DELETE FROM Students WHERE Id = {studentId};";

                int rowsAffected = await command.ExecuteNonQueryAsync();
                Console.WriteLine($"{rowsAffected} rows deleted.");
            }
        }
        static async Task AllInfo(DbConnection connection, DbProviderFactory factory)
        {

            using (DbCommand command = factory.CreateCommand())
            {
                command.Connection = connection;

                command.CommandText = "select * from Students";

                using (DbDataReader reader = command.ExecuteReader())
                {
                    Show(reader, "Name", "Group", "AverageMark", "SubjectMinAvMark", "SubjectMaxAvMark");

                }
            }

        }

        static async Task FullName(DbConnection connection, DbProviderFactory factory)
        {
            using (DbCommand command = factory.CreateCommand())
            {
                command.Connection = connection;

                command.CommandText = "select * from Students";

                using (DbDataReader reader = command.ExecuteReader())
                {
                    Show(reader, "Name");
                }
            }


        }

        static async Task AvMarks(DbConnection connection, DbProviderFactory factory)
        {
            using (DbCommand command = factory.CreateCommand())
            {
                command.Connection = connection;

                command.CommandText = "select * from Students";

                using (DbDataReader reader = command.ExecuteReader())
                {
                    Show(reader, "AverageMark");
                }
            }
        }

        static async Task AvMarksValue(DbConnection connection, DbProviderFactory factory)
        {
            using (DbCommand command = factory.CreateCommand())
            {
                command.Connection = connection;

                command.CommandText = "select * from Students where AverageMark >= 4;";

                using (DbDataReader reader = command.ExecuteReader())
                {
                    Show(reader, "Name");
                }
            }

        }

        static async Task uniqueMin(DbConnection connection, DbProviderFactory factory)
        {
            using (DbCommand command = factory.CreateCommand())
            {
                command.Connection = connection;

                command.CommandText = "select SubjectMinAvMark from Students GROUP BY SubjectMinAvMark;";

                using (DbDataReader reader = command.ExecuteReader())
                {
                    Show(reader, "SubjectMinAvMark");
                }
            }

        }

    }
}