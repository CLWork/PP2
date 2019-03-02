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

            //Change greeting color message.
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Welcome to Raeven's Writing Desk!\r\n");
            Console.ResetColor();

            //Prompt user to choose to login or create account.
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
                            
                            //Allows user to view their associate notebooks or create one if one does not exist.
                            instance.ViewNB(currentUserID, currentUser);

                            
                        }
                        else
                        {
                            while (currentUser == null)
                            {
                                instance.LoginUser();
                            }
                        }
                        break;
                    }
                case "create new account":
                case "create account":
                case "create":
                    {
                        //Allows user to be created and then login.
                        instance.CreateUser();
                       string currentUser = instance.LoginUser();
                        currentUserID = instance.GetUserId(currentUser, currentUserID);
                        instance.ViewNB(currentUserID, currentUser);
                        break;
                    }
                default:
                    Console.WriteLine($"Your entry of {input} was invalid. Please try again.");
                    break;
            }
            Utility.PauseBeforeContinuing();
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

        //User creation
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

        //Login validation
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

        //Create a new notebook
        void CreateNoteBook(string _currentUser, int _currentUserID)
        {
            string notebookName = Validation.StringValidation("What is the title of your notebook?");
            string query = "INSERT INTO notebooks(notebook_user, notebook_name) VALUES (@_currentUserID, @notebookName)";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);
            cmd.Parameters.AddWithValue("@notebookName", notebookName);

            MySqlDataReader rdr;

            rdr = cmd.ExecuteReader();

            Console.WriteLine($"The new notebook {notebookName} has been created!");
        }

        //View all notebooks associated with the current user
        void ViewNB(int _currentUserID, string _currentUser)
        {
            List<string> notebookList = new List<string>();
            
            
            string query = "SELECT notebook_id, notebook_user, notebook_name FROM notebooks WHERE notebook_user = @_currentUserID";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);
            MySqlDataReader rdr2;

            rdr2 = cmd.ExecuteReader();
            if (rdr2.HasRows)
            {
                while (rdr2.Read())
                {
                   

                    string notebooks = rdr2["notebook_name"] as string;
                    notebookList.Add(notebooks);

                    
                }

                Console.WriteLine("Your Notebooks: ");
                for (int i = 0; i < notebookList.Count; i++)
                {
                    Console.WriteLine($"{i}. {notebookList[i]}");
                }
                rdr2.Close();
            }
            else
            {
                Console.WriteLine("No notebooks to display!");
                Console.WriteLine("Create a Notebook?");
                string userChoice = Console.ReadLine().ToLower();
                switch (userChoice)
                {
                    case "y":
                    case "yes":
                        {
                            CreateNoteBook(_currentUser, _currentUserID);
                            break;
                        }
                    case "n":
                    case "no":
                        {
                            break;
                        }
                    default:
                        Console.WriteLine($"Your entry of: {userChoice} was invalid. Please try again.");
                        break;
                }
                rdr2.Close();
            }

            Console.WriteLine("Select an Action: ");
            Console.WriteLine("\r\n1. View Pages\r\n2. Create New Notebook\r\n3. Create Page\r\n4. Exit");
            string input = Console.ReadLine().ToLower();
            switch (input)
            {
                case "1":
                case "view page":
                    {
                        ViewPage(_currentUserID, _currentUser, notebookList);
                        break;
                    }
                case "create new notebook":
                case "2":
                    {
                        CreateNoteBook(_currentUser, _currentUserID);
                        break;
                    }
                case "create page":
                case "3":
                    {
                        break;
                    }
                case "exit":
                case "4":
                    {
                        
                        break;
                    }
                default:
                    Console.WriteLine($"Your entry of {input} is invalid. Please try again.");
                    break;
            }
           
        }

        int GetNoteBookID(int _currentUserID, string _notebookName)
        {
            int _notebookID = 0;
            string query = ("SELECT notebook_id FROM notebooks WHERE notebook_user = @_currentUserID AND notebook_name = @_notebookName");
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);
            cmd.Parameters.AddWithValue("@_notebookName", _notebookName);

            MySqlDataReader rdr;

            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string notebookIDString = rdr["notebook_id"].ToString();
                
                if(!int.TryParse(notebookIDString, out _notebookID))
                {
                    Console.WriteLine("Cannot convert to number.");
                }

            }
            Console.WriteLine($"{_notebookID}");
            rdr.Close();
            return _notebookID;
        }

        //View pages for selected notebook for current user
        void ViewPage(int _currentUserID, string _currentUser, List<string> _notebookList)
        {
            List<string> pageList = new List<string>();
            int notebookChoice = Validation.IntValidation("Which notebook would you like to view? Enter the Number to the Left: ");
            bool notebookExists = _notebookList.Contains(_notebookList[notebookChoice]);
            string chosenNotebook = _notebookList[notebookChoice].ToString();

            Console.WriteLine($"{chosenNotebook}");

            int _notebookID = GetNoteBookID(_currentUserID, chosenNotebook);
            
            
            if (notebookExists == false)
            {
                Console.WriteLine("That notebook does not exist!\r\n Would you like to create one?(Y/N)");
                string userChoice = Console.ReadLine().ToLower();
                switch (userChoice)
                {
                    case "y":
                    case "yes":
                        {
                            CreateNoteBook(_currentUser, _currentUserID);
                            break;
                        }
                    case "n":
                    case "no":
                        {
                            break;
                        }
                    default:
                        Console.WriteLine($"Your entry of {userChoice} was invalid, please try again.");
                        break;
                }
            }
            else
            {
                string query = "SELECT page_id, notebook_id, freeform_page.ff_content FROM pages JOIN freeform_page ON freeform_page.ff_id = pages.page_type WHERE notebook_id = @notebookID";
                MySqlCommand cmd2 = new MySqlCommand(query, connection);
                cmd2.Parameters.AddWithValue("@notebookID", _notebookID);

                MySqlDataReader rdr3;
                rdr3 = cmd2.ExecuteReader();
                while (rdr3.Read())
                {
                    string pages = rdr3["ff_content"] as string;
                    Console.WriteLine($"\r\n{pages.ToString()}\r\n");
                }
                rdr3.Close();
            }

            
        }

        //Get user ID
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
