using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace WorkmanCiera_RaevensWritingDesk
{
    class Program
    {
        private MySqlConnection connection = null;
        static void Main(string[] args)
        {
            /* Ciera Workman
             * Project & Portfolio 2
             * Raeven's Writing Desk Code Files
             */
            Program instance = new Program();
            instance.connection = new MySqlConnection();
            instance.Connect();

            Console.WriteLine("Welcome to Raeven's Writing Desk!");
            Console.WriteLine("Login or Create New Account?");
            string input = Console.ReadLine().ToLower();
            switch(input)
            {
                case "login":
                    {
                        instance.LoginUser();
                        break;
                    }
                case "create new account":
                case "create account":
                case "create":
                    {
                        instance.CreateUser();
                        break;
                    }
                default:
                    Console.WriteLine($"Your entry of {input} was invalid. Please try again.");
                    break;
            }

        }
        DataTable QueryToDB(string query)
        {

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataTable data = new DataTable();

            adapter.SelectCommand.CommandType = CommandType.Text;
            adapter.Fill(data);

            return data;
        }
        void BuildConnection()
        {

            string ip = "192.168.179.1";


            string conString = $"Server={ip};";
            conString += "uid=dbremoteuser;";
            conString += "password=password;";
            conString += "database=CieraWorkman_RaevensWritingDesk_MDV229_Database_201902;";
            conString += "port=8889;";

            connection.ConnectionString = conString;
        }
        void Connect()
        {
            BuildConnection();
            try
            {
                connection.Open();
                Console.WriteLine("Connection Successful.");
            }
            catch (MySqlException ex)
            {

                string msg = "";
                switch (ex.Number)
                {
                    case 0:
                        {
                            msg = ex.ToString();
                            break;
                        }
                    case 1042:
                        {
                            msg = "Cannot resolve host address." + connection.ConnectionString;
                            break;
                        }
                    case 1045:
                        {
                            msg = "Invalid username or password.";
                            break;
                        }
                    default:
                        {
                            msg = ex.ToString();
                            break;
                        }

                }
                Console.WriteLine(msg);

            }
        }
        void CreateUser()
        {
            string query = "INSERT INTO users(username, users_password, user_email) VALUES (@username, @password, @email)";
            Console.Write("What username would you like to use? ");
            string username = Console.ReadLine();

            Console.Write("\r\nWhat is your email? ");
            string email = Console.ReadLine();

            Console.Write("\r\nEnter your desired password: ");
            string password = Console.ReadLine();

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);

            MySqlDataReader rdr;

            rdr = cmd.ExecuteReader();
            Console.WriteLine("New user added successfully!");
        }
        void LoginUser()
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();

            Console.Write("\r\nEnter your password: ");
            string password = Console.ReadLine();

            string query = "SELECT username, users_password FROM users WHERE username = @username AND users_password = @password";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            MySqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                Console.WriteLine($"Welcome {username}!");
            }
            else
            {
                Console.WriteLine("User not found. Please try again.");
            }

        }
    }
}
