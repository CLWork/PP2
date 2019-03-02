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

            bool programIsRunning = true;
            string currentUser = null;

            //Prompt user to choose to login or create account.

            Console.WriteLine("Login or Create New Account?");
            string input = Console.ReadLine().ToLower();
            switch (input)
            {
                case "login":
                    {
                        currentUser = instance.LoginUser();
                        if (currentUser != null)
                        {
                            currentUserID = instance.GetUserId(currentUser, currentUserID);

                            
                                //Allows user to view their associate notebooks or create one if one does not exist.
                                instance.ViewNB(currentUserID, currentUser, programIsRunning);
                            
                        }
                        else
                        {
                            while (currentUser == null)
                            {
                                currentUser = instance.LoginUser();
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
                        currentUser = instance.LoginUser();
                        if (currentUser != null)
                        {
                            currentUserID = instance.GetUserId(currentUser, currentUserID);

                            while (programIsRunning)
                            {

                                //Allows user to view their associate notebooks or create one if one does not exist.
                                instance.ViewNB(currentUserID, currentUser, programIsRunning);
                            }



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
                default:
                    Console.WriteLine($"Your entry of {input} was invalid. Please try again.");
                    break;
            }
            Utility.PauseBeforeContinuing();
            
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
        void ViewNB(int _currentUserID, string _currentUser, bool _programIsRunning)
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
                Console.WriteLine("Create a Notebook? Yes/No");
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
                        ViewAllPages(_currentUserID, _currentUser, notebookList);
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
                        
                        CreatePage(notebookList, _currentUserID);
                        break;
                    }
                case "exit":
                case "4":
                    {
                        _programIsRunning = false;
                        break;
                    }
                default:
                    Console.WriteLine($"Your entry of {input} is invalid. Please try again.");
                    break;
            }
            rdr2.Close();
        }

        //Gets the Notebook ID
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
        void ViewAllPages(int _currentUserID, string _currentUser, List<string> _notebookList)
        {
            List<string> pageList = new List<string>();
            List<int> pageIDList = new List<int>();
            int pageID = 0;
            int notebookChoice = Validation.IntValidation("Which notebook would you like to view? Enter the Number to the Left: ");
            bool notebookExists = _notebookList.Contains(_notebookList[notebookChoice]);
            string chosenNotebook = _notebookList[notebookChoice].ToString();

            

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
                    pageList.Add(pages);
                   

                    string pageIDString = rdr3["page_id"].ToString();
                    if (!int.TryParse(pageIDString, out pageID))
                    {
                        Console.WriteLine("Cannot convert to number.");
                    }
                    pageIDList.Add(pageID);

                }
                rdr3.Close();
                Console.WriteLine($"Number of Pages in Notebook: {pageList.Count}");
                for (int i = 0; i < pageList.Count; i++)
                {
                    Console.WriteLine($"{i}. {pageList[i]}\r\n\r\n");
                }

                Console.WriteLine("Select An Action: \r\n" +
                    "1. View One Page\r\n" +
                    "2. Edit Page\r\n" +
                    "3. Back to Menu");
                string pageChoice = Console.ReadLine().ToLower();
                switch (pageChoice)
                {
                    case "1":
                    case "view page":
                    case "view one page":
                        {
                            for (int i = 0; i < pageIDList.Count; i++)
                            {
                                Console.WriteLine($"{1}. {pageIDList[i]}");
                                
                            }
                            int pageIDIndex = Validation.IntValidation("Enter the number to the left of the Page Number: ");
                            while (pageIDIndex > pageIDList.Count - 1)
                            {
                                Console.WriteLine("Index out of bounds. Please try again.\r\n");
                                pageIDIndex = Validation.IntValidation("Enter the number to the left of the Page Number: ");
                            }

                            int chosenPageID = pageIDList[pageIDIndex];

                            int _contentID = GetContentID(chosenPageID);
                            UpdatePage(_contentID, chosenPageID);
                            break;
                        }
                    case "2":
                    case "edit page":
                        {
                            for (int i = 0; i < pageIDList.Count; i++)
                            {
                                Console.WriteLine($"{1}. {pageIDList[i]}");

                            }
                            int pageIDIndex = Validation.IntValidation("Enter the number to the left of the Page Number: ");
                            while (pageIDIndex > pageIDList.Count - 1)
                            {
                                Console.WriteLine("Index out of bounds. Please try again.\r\n");
                                pageIDIndex = Validation.IntValidation("Enter the number to the left of the Page Number: ");
                            }

                            int chosenPageID = pageIDList[pageIDIndex];

                            int _contentID = GetContentID(chosenPageID);
                            UpdatePage(_contentID, chosenPageID);
                            break;
                        }
                    case "back":
                    case "back to menu":
                    case "menu":
                        {
                            break;
                        }
                    default:
                        Console.WriteLine($"Your entry of {pageChoice} was invalid. Please try again.");
                        break;

                }
            }

            
        }

        //Retrieve the ID of the content.
        int GetContentID(int _chosenPageID)
        {
            int contentID = 0;
            string getIDQuery = "SELECT page_type, freeform_page.ff_id FROM pages JOIN freeform_page ON freeform_page.ff_id = pages.page_type WHERE page_type = @_chosenPageID";
            MySqlCommand getIDCmd = new MySqlCommand(getIDQuery, connection);
            getIDCmd.Parameters.AddWithValue("@_chosenPageID", _chosenPageID);

            MySqlDataReader getIDRdr;

            getIDRdr = getIDCmd.ExecuteReader();

            while (getIDRdr.Read())
            {
                string contentIDString = getIDRdr["ff_id"].ToString();
                if (!int.TryParse(contentIDString, out contentID))
                {
                    Console.WriteLine("Cannot convert to number");
                }
            }

            getIDRdr.Close();
            return contentID;
        }

        //View a single page
        void ViewOnePage(int _chosenPageID)
        {
            string viewOneQuery = "SELECT page_type, freeform_page.ff_content FROM pages JOIN freeform_page ON freeform_page.ff_id = pages.page_type WHERE page_id = @_chosenPageID";
            MySqlCommand viewOneCmd = new MySqlCommand(viewOneQuery, connection);
            viewOneCmd.Parameters.AddWithValue("@_chosenPageID", _chosenPageID);

            MySqlDataReader viewOneRdr;
            viewOneRdr = viewOneCmd.ExecuteReader();
            
            while (viewOneRdr.Read())
            {
             
                string _pageContent = viewOneRdr["ff_content"].ToString();
                Console.WriteLine($"{_pageContent}");
            }

            viewOneRdr.Close();
        }

        //Edit a chosen page
        void UpdatePage(int _contentID, int _chosenPageID)
        {
            string pageContent = null;
            string selectContent = "SELECT page_type, freeform_page.ff_content FROM pages JOIN freeform_page ON freeform_page.ff_id = pages.page_type WHERE page_id = @_chosenPage";
            MySqlCommand selectContentCmd = new MySqlCommand(selectContent, connection);
            selectContentCmd.Parameters.AddWithValue("@_chosenPage", _chosenPageID);

            MySqlDataReader contentRdr;
            contentRdr = selectContentCmd.ExecuteReader();
            while (contentRdr.Read())
            {
                pageContent = contentRdr["ff_content"].ToString();
            }

            contentRdr.Close();

            Console.WriteLine($"Current Content:\r\n {pageContent}");

            Console.WriteLine("Select Action:\r\n1. Add Content\r\n2. Delete Content\r\n3. Input New Content\r\n4.Back\r\n");
            string contentAction = Console.ReadLine().ToLower();
            switch (contentAction)
            {
                case "1":
                case "Add content":
                    {
                        string updateAddQuery = "UPDATE freeform_page SET ff_content = @_userContent WHERE ff_id = @_contentID";

                        Console.WriteLine("Enter your Content: \r\n\r\n");
                        string newContent = Console.ReadLine();

                        pageContent = pageContent + " " + newContent;

                        MySqlCommand updateAddCmd = new MySqlCommand(updateAddQuery, connection);
                        updateAddCmd.Parameters.AddWithValue("@_userContent", pageContent);
                        updateAddCmd.Parameters.AddWithValue("@_contentID",_contentID);

                        MySqlDataReader addRdr;

                        addRdr = updateAddCmd.ExecuteReader();

                        Console.WriteLine("Page updated!");

                        addRdr.Close();

                        Console.WriteLine("New Content: \r\n");
                        ViewOnePage(_chosenPageID);
                        

                        break;
                    }
                case "2":
                case "delete content":
                    {
                        pageContent = null;
                        string updateDeleteQuery = "UPDATE freeform_page SET ff_content = @_userContent WHERE ff_id = @_contentID";
                        MySqlCommand updateDeleteCmd = new MySqlCommand(updateDeleteQuery, connection);
                        updateDeleteCmd.Parameters.AddWithValue("@_userContent", pageContent);
                        updateDeleteCmd.Parameters.AddWithValue("@_contentID", _contentID);

                        MySqlDataReader deleteRdr;

                        deleteRdr = updateDeleteCmd.ExecuteReader();

                        Console.WriteLine("Page updated!");

                        deleteRdr.Close();

                        Console.WriteLine("New Content: \r\n");
                        ViewOnePage(_chosenPageID);

                        break;
                    }
                case "3":
                
                case "new content":
                case "input new content":
                    {
                        pageContent = null;
                        Console.WriteLine("Enter your Content: \r\n\r\n");
                        pageContent = Console.ReadLine();

                        string updateNewQuery = "UPDATE freeform_page SET ff_content = @_userContent WHERE ff_id = @_contentID";
                       
                        MySqlCommand updateNewCmd = new MySqlCommand(updateNewQuery, connection);
                        updateNewCmd.Parameters.AddWithValue("@_userContent", pageContent);
                        updateNewCmd.Parameters.AddWithValue("@_contentID", _contentID);

                        MySqlDataReader newRdr;

                        newRdr = updateNewCmd.ExecuteReader();

                        Console.WriteLine("Page updated!");

                        newRdr.Close();

                        Console.WriteLine("New Content: \r\n");
                        ViewOnePage(_chosenPageID);
                        
                        break;
                    }
                default:
                    Console.WriteLine($"Your entry of {contentAction} was invalid,");
                    break;
            }
            
            
        }

        void CreatePage(List<string> _notebookList, int _currentUserID)
        {
            int notebookChoice = Validation.IntValidation("Which notebook would you like to add to? Enter the Number to the Left: ");
            bool notebookExists = _notebookList.Contains(_notebookList[notebookChoice]);
            string chosenNotebook = _notebookList[notebookChoice].ToString();
            
            int _notebookID = GetNoteBookID(_currentUserID, chosenNotebook);

            Console.WriteLine("Enter Content for new Page: ");
            string _newUserContent = Console.ReadLine();

            string insertContentQuery = "INSERT INTO freeform_page(ff_content) VALUES (@_newUserContent)";
            MySqlCommand insertContentCmd = new MySqlCommand(insertContentQuery, connection);
            insertContentCmd.Parameters.AddWithValue("@_newUserContent", _newUserContent);

            MySqlDataReader insertContentRdr;
            insertContentRdr = insertContentCmd.ExecuteReader();

            insertContentRdr.Close();

            int dBCount = 0;

            string countDB = "SELECT COUNT(*) FROM freeform_page;";
            MySqlCommand countDBCmd = new MySqlCommand(countDB, connection);


            object dBCountObject = countDBCmd.ExecuteScalar();
            string dBCountString = dBCountObject.ToString();
            if (!int.TryParse(dBCountString, out dBCount))
            {
                Console.WriteLine("Cannot convert to number");
            }
            countDBCmd.Dispose();
           
            int newPageID = dBCount;
            Console.WriteLine($"{newPageID.ToString()}");
            
            string insertToPages = "INSERT INTO pages(pages.notebook_id, pages.page_type) VALUES (@_notebookID, @newPageID)";
            MySqlCommand insertToPagesCmd = new MySqlCommand(insertToPages, connection);
            insertToPagesCmd.Parameters.AddWithValue("@_notebookID", _notebookID);
            insertToPagesCmd.Parameters.AddWithValue("@newPageID", newPageID);

            MySqlDataReader intoPagesRdr;

            intoPagesRdr = insertToPagesCmd.ExecuteReader();

            Console.WriteLine("Page added!");

            intoPagesRdr.Close();

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
