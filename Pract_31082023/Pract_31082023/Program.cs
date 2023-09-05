using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace Pract_30082023
{
    internal class Program
    {
        private static string connectionString => ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        static void Main(string[] args)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    Menu(connection);

                    connection.Close();
                    Console.WriteLine("Disconnected from database.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection error: {ex.Message}");
            }
        }

        static void Menu(SqlConnection connection)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Connected to database 'Students Marks'.");

                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("To display all information   -  enter 1");
                Console.WriteLine("To display types             -  enter 2");
                Console.WriteLine("To display providers         -  enter 3");
                Console.WriteLine("To display MaxQuan           -  enter 4");
                Console.WriteLine("To display MinQuant          -  enter 5");
                Console.WriteLine("To display MinCost           -  enter 6");
                Console.WriteLine("To display MaxCost           -  enter 7");
                Console.WriteLine("-------------------------------------------------");

                string choice = Console.ReadLine();

                var products = new Dictionary<string, List<string>>();

                switch (choice)
                {
                    case "1":
                        AllInfo(connection);
                        break;
                    case "2":
                        AllTypes(connection);
                        break;
                    case "3":
                        AllProvider(connection);
                        break;
                    case "4":
                        MaxQuant(connection);
                        break;
                    case "5":
                        MinQuant(connection);
                        break;
                    case "6":
                        MinCost(connection);
                        break;
                    case "7":
                        MaxCost(connection);
                        break;
                    case "0":
                        return;
                }
                Console.ReadKey();
            } while (true);


        }

        static void Show(SqlConnection connection, string query, params string[] info)
        {
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("-----------------------------");
                        for (int i = 0; i < info.Length; i++)
                        {
                            Console.WriteLine($"{reader[info[i]]}");
                        }
                        Console.WriteLine("-----------------------------");
                    }
                }

            }
        }


        static void AllInfo(SqlConnection connection)
        {
            string query =   @"SELECT P.Id,
                              P.Name AS ProductName,
                              T.Name AS TypeName,
                              PR.Name AS ProviderName,
                              P.Quantity,
                              P.Cost,
                              P.DeliveryDate
                              FROM Product AS P
                              JOIN Type AS T ON P.TypeId = T.Id
                              JOIN Provider AS PR ON P.ProviderId = PR.Id";
           

            Show(connection, query, "ProductName", "TypeName", "ProviderName", "Quantity", "Cost", "DeliveryDate");
        }


        static void AllTypes(SqlConnection connection)
        {
            string query = @" SELECT Name FROM Type ";

            Show(connection, query, "Name");
            
        }


        static void AllProvider(SqlConnection connection)
        {
            string query = @" SELECT Name FROM Provider ";

            Show(connection, query, "Name");
        }

        static void MaxQuant(SqlConnection connection)
        {
            string query = @"
                            SELECT TOP 1
                                P.Id,
                                P.Name AS ProductName,
                                T.Name AS TypeName,
                                PR.Name AS ProviderName,
                                P.Quantity,
                                P.Cost,
                                P.DeliveryDate
                            FROM Product AS P
                            JOIN Type AS T ON P.TypeId = T.Id
                            JOIN Provider AS PR ON P.ProviderId = PR.Id
                            ORDER BY P.Quantity DESC";

            Show(connection, query, "ProductName", "TypeName", "ProviderName", "Quantity", "Cost", "DeliveryDate");
        }

        static void MinQuant(SqlConnection connection)
        {
            string query = @"
                            SELECT TOP 1
                                P.Id,
                                P.Name AS ProductName,
                                T.Name AS TypeName,
                                PR.Name AS ProviderName,
                                P.Quantity,
                                P.Cost,
                                P.DeliveryDate
                            FROM Product AS P
                            JOIN Type AS T ON P.TypeId = T.Id
                            JOIN Provider AS PR ON P.ProviderId = PR.Id
                            ORDER BY P.Quantity ASC";

            Show(connection, query, "ProductName", "TypeName", "ProviderName", "Quantity", "Cost", "DeliveryDate");
        }

        static void MaxCost(SqlConnection connection)
        {
            string query = @"
                            SELECT TOP 1
                                P.Id,
                                P.Name AS ProductName,
                                T.Name AS TypeName,
                                PR.Name AS ProviderName,
                                P.Quantity,
                                P.Cost,
                                P.DeliveryDate
                            FROM Product AS P
                            JOIN Type AS T ON P.TypeId = T.Id
                            JOIN Provider AS PR ON P.ProviderId = PR.Id
                            ORDER BY P.Cost DESC";

            Show(connection, query, "ProductName", "TypeName", "ProviderName", "Quantity", "Cost", "DeliveryDate");
        }

        static void MinCost(SqlConnection connection)
        {
            string query = @"
                            SELECT TOP 1
                                P.Id,
                                P.Name AS ProductName,
                                T.Name AS TypeName,
                                PR.Name AS ProviderName,
                                P.Quantity,
                                P.Cost,
                                P.DeliveryDate
                            FROM Product AS P
                            JOIN Type AS T ON P.TypeId = T.Id
                            JOIN Provider AS PR ON P.ProviderId = PR.Id
                            ORDER BY P.Cost ASC";

            Show(connection, query, "ProductName", "TypeName", "ProviderName", "Quantity", "Cost", "DeliveryDate");
        }

    }

}