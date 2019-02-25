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
            int currentUserID = 0;
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
                        string currentUser = instance.LoginUser();
                        if (currentUser != null)
                        {
                            currentUserID = instance.GetUserId(currentUser, currentUserID);
                            
                            instance.CreateNoteBook(currentUser);
                        }
                        else
                        {
                            instance.LoginUser();
                        }
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
            
            string username = Validation.StringValidation("What username would you like to use? ");

            string email = Validation.StringValidation("What is your email? ");

            string password = Validation.StringValidation("Enter your desired password: ");

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);

            MySqlDataReader rdr;

            rdr = cmd.ExecuteReader();
            Console.WriteLine("New user added successfully!");

            rdr.Close();
        }
        string LoginUser()
        {

            string _currentUser = null;

            string username = Validation.StringValidation("Enter your username: ");

            
            string password = Validation.StringValidation("\r\nEnter your password: ");

            string query = "SELECT username, users_password FROM users WHERE username = @username AND users_password = @password";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            MySqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                Console.WriteLine($"Welcome {username}!");
                _currentUser = username;
                rdr.Close();

                return _currentUser;
            }
            else
            {
                Console.WriteLine("User not found. Please try again.");
                rdr.Close();
                
                return _currentUser;
            }

            

        }
        void CreateNoteBook(string _currentUser)
        {
            string notebookName = Validation.StringValidation("What is the title of your notebook?");
            string query = "INSERT INTO notebooks(notebook_user, notebook_name) VALUES @_currentUser, @notebookName";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@_currentUser", _currentUser);
            cmd.Parameters.AddWithValue("@notebookName", notebookName);

            MySqlDataReader rdr;

            rdr = cmd.ExecuteReader();

            Console.WriteLine($"The new notebook {notebookName} has been created!");
        }
        void ViewNB()
        {
            string query = "SELECT notebook_user, notebook_name FROM notebooks JOIN users ON users.user_id = notebook_user.notebooks";

            Console.WriteLine("Create New Notebook or View Page? Enter No to do nothing.");
            string input = Console.ReadLine().ToLower();
            switch (input)
            {
                case "create":
                    {
                        break;
                    }
                case "page":
                    {
                        break;
                    }
                case "no":
                    {
                        break;
                    }
            }
        }

        void ViewPage()
        {

        }

        int GetUserId(string _currentUser, int _currentUserID)
        {
            int userID = 0;
            string query = ( "SELECT user_id FROM users WHERE username = @_currentUser");
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@_currentUser", _currentUser);

            MySqlDataReader rdr;

            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string userIDString = rdr["user_id"].ToString();
                int.TryParse(userIDString, out userID);

                

                _currentUserID = userID;

              
            }
           
            rdr.Close();
            return _currentUserID;
        }
    }
}
