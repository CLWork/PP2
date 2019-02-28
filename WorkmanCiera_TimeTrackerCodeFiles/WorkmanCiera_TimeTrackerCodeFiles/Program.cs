using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace WorkmanCiera_TimeTrackerCodeFiles
{
    class Program
    {
        MySqlConnection connection = null;

        static void Main(string[] args)
        {
            /* Ciera Workman
             * Project & Portfolio 2 201902
             * Time Tracker Coding Files
             */

            Program instance = new Program();
            instance.connection = new MySqlConnection();
            instance.Connect();

            Console.WriteLine("\r\nWelcome to the Time Tracker Application!\r\n");
            bool programIsRunning = true;
            string currentUser = null;
            int currentUserID = 0;
           

            currentUser = instance.LoginMenu();
            while (currentUser == null)
            {
                Console.WriteLine("Login unsuccessful.\r\nPlease try again.");
                currentUser = instance.LoginMenu();
            }
            currentUserID = instance.GetUserId(currentUser, currentUserID);

            while (programIsRunning)
            {
                
               
                instance.DisplayMainMenu();
                Console.Write("Enter your Selection: ");
                string menuChoice = Console.ReadLine().ToLower();
                switch (menuChoice)
                {
                    case "1":
                    case "enter":
                    case "enter activity":
                    case "enter an activity":
                        {
                            break;
                        }
                    case "2":
                    case "view":
                    case "view data":
                    case "view tracked data":
                        {
                            instance.ViewData(currentUserID);
                            break;
                        }
                    case "3":
                    case "run":
                    case "calculations":
                    case "run calculations":
                        {
                            instance.RunCalc(currentUserID, currentUser);
                            break;
                        }
                    case "4":
                    case "quit":
                    case "exit":
                        {
                            programIsRunning = false;
                            break;
                        }
                    default:
                        Console.WriteLine($"Your entry of {menuChoice} was invalid.\r\nPlease try again.");
                        break;
                }


                Utility.PauseBeforeContinuing();
                Console.Clear();
            }

        }

        //connects to database
        void BuildConnection()
        {

            string ip = "192.168.179.1";


            string conString = $"Server={ip};";
            conString += "uid=dbremoteuser;";
            conString += "password=password;";
            conString += "database=CieraWorkman_MDV229_Database_201902;";
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

        //Logins the user in
        string LoginMenu()
        {
            string _currentUser = null;

            string loginQuery = "SELECT user_password, user_firstname, user_lastname FROM time_tracker_users WHERE user_password = @password AND user_firstname = @firstname AND user_lastname = @lastname";

            string firstName = Validation.StringValidation("Enter your First Name: ");
            string lastName = Validation.StringValidation("Enter your Last Name: ");
            string password = Validation.StringValidation("Enter your Password: ");

            MySqlCommand cmdLogin = new MySqlCommand(loginQuery, connection);
            cmdLogin.Parameters.AddWithValue("@firstname", firstName);
            cmdLogin.Parameters.AddWithValue("@lastname", lastName);
            cmdLogin.Parameters.AddWithValue("@password", password);

            MySqlDataReader loginRdr = cmdLogin.ExecuteReader();

            if (loginRdr.HasRows)
            {
                Console.WriteLine($"Hello {firstName}!");
                _currentUser = firstName;
                loginRdr.Close();
                return _currentUser;
            }
            else
            {
                Console.WriteLine("User not found.");
                loginRdr.Close();
                return _currentUser;
            }

        }

        //Obtains user ID
        int GetUserId(string _currentUser, int _currentUserID)
        {
            int _userID = 0;
            string query = ("SELECT user_id FROM time_tracker_users WHERE user_firstname = @_currentUser");
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@_currentUser", _currentUser);

            MySqlDataReader rdr;

            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string userIDString = rdr["user_id"].ToString();
                int.TryParse(userIDString, out _userID);



                _currentUserID = _userID;


            }

            rdr.Close();
            return _currentUserID;
        }

        //Displays main menu to user
        void DisplayMainMenu()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Enter an Activity\r\n" +
                "2. View Tracked Data\r\n" +
                "3. Run Calculations\r\n" +
                "4. Exit\r\n");

        }

        //Enter a new activity to the DB
        void EnterActivity()
        {

        }

        //View data
        void ViewData(int _currentUserID)
        {
            Console.WriteLine("1. Select By Date\r\n" +
                "2. Select By Category\r\n" +
                "3. Select By Description\r\n" +
                "4. Back\r\n");
            string viewChoice = Console.ReadLine().ToLower();
            switch (viewChoice)
            {
                case "1":
                case "date":
                case "select date":
                case "select by date":
                    {
                        break;
                    }
                case "2":
                case "category":
                case "select category":
                case "select by category":
                    {
                        SelectCategory(_currentUserID);
                        break;
                    }
                case "3":
                case "description":
                case "select description":
                case "select by description":
                    {
                        break;
                    }
                case "4":
                case "back":
                case "return":
                    {
                        break;
                    }
                default:
                    Console.WriteLine($"Your entry of {viewChoice} was invalid.\r\n Please try again.");
                    break;
            }
        }

        //Runs various calculations
        void RunCalc(int _currentUserID, string _currentUser)
        {
            string calcQuery = "SELECT user_id FROM activity_log WHERE user_id = @_currentUserID";
            MySqlCommand calcCmd = new MySqlCommand(calcQuery, connection);
            calcCmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);

            MySqlDataReader calcRdr;
            calcRdr = calcCmd.ExecuteReader();
             
            if (calcRdr.HasRows)
            {
                calcRdr.Close();

                Console.WriteLine($"~-~-~-~-~-~-~  \r\nCalculations for User {_currentUser} \r\n~-~-~-~-~-~-~\r\n\r\n");


                FindTotalLoggedHours(_currentUserID);
                Console.WriteLine($"Total Hours Across 26 Days: 624\r\n");

                RunSchoolCalc(_currentUserID);
                RunSleepingCalc(_currentUserID);
                RunWorkCalc(_currentUserID);
                RunFeedCatCalc(_currentUserID);
                RunTravelTime(_currentUserID);
                RunPersonalTime(_currentUserID);
                RunEatCalc(_currentUserID);
                RunGetReady(_currentUserID);
                NumberOfWorkEntries(_currentUserID);
            }
            else
            {
                Console.WriteLine("No entries to run for this user!");
                calcRdr.Close();
            }
            
        }

        //Allows user to select a category to View with ViewData
        void SelectCategory(int _currentUserID)
        {
            List<string> categoryList = new List<string>();
            string categoryQuery = "SELECT category_description FROM activity_categories";

            MySqlCommand catCmd = new MySqlCommand(categoryQuery, connection);

            MySqlDataReader categoryRdr = catCmd.ExecuteReader();

            while (categoryRdr.Read())
            {
                string category = categoryRdr["category_description"] as string;
                categoryList.Add(category);
            }

            for (int i = 0; i < categoryList.Count; i++)
            {
                Console.WriteLine($"{i}. {categoryList[i]}");
            }

            int categorySelection = Validation.IntValidation("Enter the number to the left of the category to select it: ");
            while (categorySelection > categoryList.Count - 1)
            {
                categorySelection = Validation.IntValidation("Your selection is out of bounds. Please try again.");

            }

            int categoryID = categorySelection + 1;
            categoryRdr.Close();

            Console.WriteLine($"You have chosen {categoryList[categorySelection].ToString()}.");
            ViewCategory(categoryID, _currentUserID);

        }

        //Shows the user the selected category
        void ViewCategory(int _categoryID, int _currentUserID)
        {
            List<string> dateList = new List<string>();
            List<string> activityDescList = new List<string>();
            List<string> timeSpentStringList = new List<string>();
            List<decimal> timeSpentList = new List<decimal>();
            decimal timeSpent = 0;
            decimal totalTime = 0m;

            string viewCatQuery = "SELECT activity_log.user_id, tracked_calendar_dates.calendar_date, activity_log.category_description, activity_descriptions.activity_description, activity_times.time_spent_on_activity FROM activity_log " +
                "JOIN tracked_calendar_dates ON tracked_calendar_dates.calendar_date_id = activity_log.calendar_date " +
                "JOIN activity_descriptions ON activity_descriptions.activity_description_id = activity_log.activity_description " +
                "JOIN activity_times ON activity_times.activity_time_id = activity_log.time_spent_on_activity " +
                "WHERE user_id = @_currentUserID AND category_description = @_categoryID";

            MySqlCommand viewCatCmd = new MySqlCommand(viewCatQuery, connection);
            viewCatCmd.Parameters.AddWithValue("@_categoryID", _categoryID);
            viewCatCmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);

            MySqlDataReader catRdr;
            catRdr = viewCatCmd.ExecuteReader();
            if (catRdr.HasRows)
            {
                while (catRdr.Read())
                {

                    string date = catRdr["calendar_date"] as string;

                    dateList.Add(date);
                    string activityDesc = catRdr["activity_description"] as string;
                    activityDescList.Add(activityDesc);
                    string timeSpentString = catRdr["time_spent_on_activity"] as string;
                    timeSpentStringList.Add(timeSpentString);

                }

                for (int i = 0; i < timeSpentStringList.Count; i++)
                {
                    decimal.TryParse(timeSpentStringList[i], out timeSpent);
                    timeSpentList.Add(timeSpent);
                }

                for (int i = 0; i < timeSpentList.Count; i++)
                {
                    totalTime = totalTime + timeSpentList[i];
                }

                for (int i = 0; i < activityDescList.Count; i++)
                {
                    Console.WriteLine($"{dateList[i].ToString()} - {activityDescList[i].ToString()} - {timeSpentStringList[i].ToString()}");
                }

                Console.WriteLine($"Total Time For This Category: {totalTime} hours.");
            }
            else
            {
                Console.WriteLine("No activites listed for that category!");
            }

            catRdr.Close();
        }
        
        //tells the user how many hours they spent sleeping.
        void RunSleepingCalc(int _currentUserID)
        {
            string sleepHourQuery = ("SELECT SUM(activity_times.time_spent_on_activity) FROM activity_log JOIN activity_times ON activity_times.activity_time_id = activity_log.time_spent_on_activity WHERE category_description = 7 AND user_id = @_currentUserID");

            MySqlCommand sleepHourCmd = new MySqlCommand(sleepHourQuery, connection);
            sleepHourCmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);

            object timeSleeping = sleepHourCmd.ExecuteScalar();
            Console.WriteLine($"Total Hours Spent Sleeping: {timeSleeping.ToString()}\r\n");
        }

        //tells the user how many hours they logged under the school category.
        void RunSchoolCalc(int _currentUserID)
        {
            string schoolHourQuery = ("SELECT SUM(activity_times.time_spent_on_activity) FROM activity_log JOIN activity_times ON activity_times.activity_time_id = activity_log.time_spent_on_activity WHERE category_description = 6 AND user_id = @_currentUserID");

            MySqlCommand schoolHourCmd = new MySqlCommand(schoolHourQuery, connection);
            schoolHourCmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);

            object timeOnSchool = schoolHourCmd.ExecuteScalar();
            Console.WriteLine($"Total Hours Spent Doing Assignments: {timeOnSchool.ToString()}\r\n");
        }

        //tells the user how many hours they logged.
        void FindTotalLoggedHours(int _currentUserID)
        {
            
            string loggedQuery = ("SELECT SUM(activity_times.time_spent_on_activity) FROM activity_log JOIN activity_times ON activity_times.activity_time_id = activity_log.time_spent_on_activity WHERE user_id = @_currentUserID");

            MySqlCommand loggedHourCmd = new MySqlCommand(loggedQuery, connection);
            loggedHourCmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);

            object totalHours = loggedHourCmd.ExecuteScalar();
            
            Console.WriteLine($"Total Hours Logged To App: {totalHours.ToString()}\r\n");
            
        }

        //tells the user how much time they logged at work.
        void RunWorkCalc(int _currentUserID)
        {
            string workHourQuery = ("SELECT SUM(activity_times.time_spent_on_activity) FROM activity_log JOIN activity_times ON activity_times.activity_time_id = activity_log.time_spent_on_activity WHERE category_description = 5 AND user_id = @_currentUserID");

            MySqlCommand workHourCmd = new MySqlCommand(workHourQuery, connection);
            workHourCmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);

            object timeSpentAtWork = workHourCmd.ExecuteScalar();
            Console.WriteLine($"Total Hours Working: {timeSpentAtWork.ToString()}\r\n");
        }

        //tells the user how many times they fed their cat
        void RunFeedCatCalc(int _currentUserID)
        {
            string feedCatQuery = ("SELECT COUNT(activity_times.time_spent_on_activity) FROM activity_log JOIN activity_times ON activity_times.activity_time_id = activity_log.time_spent_on_activity WHERE category_description = 2 AND activity_description = 15 AND user_id = @_currentUserID");

            MySqlCommand feedCatCmd = new MySqlCommand(feedCatQuery, connection);
            feedCatCmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);

            object timesCatFed = feedCatCmd.ExecuteScalar();
            Console.WriteLine($"Total Times Cats Were Fed: {timesCatFed.ToString()}\r\n");
        }

        //tells the user how much time they spent traveling
        void RunTravelTime(int _currentUserID)
        {
            string travelTimeQuery = ("SELECT SUM(activity_times.time_spent_on_activity) FROM activity_log JOIN activity_times ON activity_times.activity_time_id = activity_log.time_spent_on_activity WHERE category_description = 4 AND user_id = @_currentUserID");

            MySqlCommand timeTravelCmd = new MySqlCommand(travelTimeQuery, connection);
            timeTravelCmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);

            object timeTraveled = timeTravelCmd.ExecuteScalar();
            Console.WriteLine($"Total Time Spent Traveling: {timeTraveled.ToString()}\r\n");
        }

        //tells the user how much time they logged under personal category.
        void RunPersonalTime(int _currentUserID)
        {
            string personalTimeQuery = ("SELECT SUM(activity_times.time_spent_on_activity) FROM activity_log JOIN activity_times ON activity_times.activity_time_id = activity_log.time_spent_on_activity WHERE category_description = 3 AND user_id = @_currentUserID");

            MySqlCommand personalTimeCmd = new MySqlCommand(personalTimeQuery, connection);
            personalTimeCmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);

            object personalTime = personalTimeCmd.ExecuteScalar();
            Console.WriteLine($"Total Personal Time: {personalTime.ToString()}\r\n");
        }

        //tells the user how much time they spent eating.
        void RunEatCalc(int _currentUserID)
        {
            string eatQuery = ("SELECT SUM(activity_times.time_spent_on_activity) FROM activity_log JOIN activity_times ON activity_times.activity_time_id = activity_log.time_spent_on_activity WHERE category_description = 11 AND user_id = @_currentUserID");

            MySqlCommand eatCmd = new MySqlCommand(eatQuery, connection);
            eatCmd.Parameters.AddWithValue("@_currentUserID",_currentUserID);

            object eatTime = eatCmd.ExecuteScalar();
            Console.WriteLine($"Total Time Eating Meals: {eatTime.ToString()}\r\n");
        }

        //Tells the user how much time they spent getting ready for work
        void RunGetReady(int _currentUserID)
        {
            string getReadyQuery = ("SELECT SUM(activity_times.time_spent_on_activity) FROM activity_log JOIN activity_times ON activity_times.activity_time_id = activity_log.time_spent_on_activity WHERE activity_description = 19 AND user_id = @_currentUserID");

            MySqlCommand getReadyCmd = new MySqlCommand(getReadyQuery, connection);
            getReadyCmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);

            object getReady = getReadyCmd.ExecuteScalar();
            Console.WriteLine($"Total Time Spent Getting Ready For Work: {getReady.ToString()}\r\n");
        }

        //Tells the user how many entries they have for the Work category
        void NumberOfWorkEntries(int _currentUserID)
        {
            string workCountQuery = ("SELECT COUNT(activity_times.time_spent_on_activity) FROM activity_log JOIN activity_times ON activity_times.activity_time_id = activity_log.time_spent_on_activity WHERE category_description = 5 AND user_id = @_currentUserID");

            MySqlCommand workCountCmd = new MySqlCommand(workCountQuery, connection);
            workCountCmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);

            object workCount = workCountCmd.ExecuteScalar();
            Console.WriteLine($"Total Work Entries: {workCount.ToString()}\r\n");
        }

        //tells the user how many entries they contributed to app.
        void NumberOfEntries(int _currentUserID)
        {
            string numberOfEntriesQuery = ("SELECT COUNT(*) FROM activity_log WHERE user_id = @_currentUserID");

            MySqlCommand numEntriesCmd = new MySqlCommand(numberOfEntriesQuery, connection);
            numEntriesCmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);

            object numEntries = numEntriesCmd.ExecuteScalar();
            Console.WriteLine($"Total Entries to App: {numEntries.ToString()}\r\n");
        }
    }
}
