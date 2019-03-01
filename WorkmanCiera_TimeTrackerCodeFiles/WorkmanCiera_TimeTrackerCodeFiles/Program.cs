using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;

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
            List<string> _listOfDates = instance.PopulateDateList();
           

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
                            instance.EnterActivityMenu(currentUserID, _listOfDates);
                            break;
                        }
                    case "2":
                    case "view":
                    case "view data":
                    case "view tracked data":
                        {
                            instance.ViewData(currentUserID, _listOfDates);
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
                Console.WriteLine($"Hello {firstName}!\r\n");
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
        void EnterActivityMenu(int _currentUserID, List<string> _listOfDates)
        {
            Console.WriteLine("~-~-~-~-~-~-~-~-~-~-~-~\r\n Enter Activity Menu \r\n~-~-~-~-~-~-~-~-~-~-~-~\r\n");
            int categoryID = ChooseCategory();
            int descriptionID = ChooseDescription();
            int dateID = ChooseDate(_listOfDates);
            int monthDayID = MonthDay(dateID);
            int dayOfWeek = DayOfWeek(monthDayID);
            int timeSpentID = TimeSpent();
            while (timeSpentID == 0)
            {
                Console.WriteLine("Invalid time entry. Please try again.");
                timeSpentID = TimeSpent();
            }

            EnterActivityToDB(_currentUserID, monthDayID, dateID, categoryID, descriptionID, dayOfWeek, timeSpentID);

            Console.WriteLine("\r\n1.Enter Another Activity\r\n2.Return To Menu");
            string input = Console.ReadLine().ToLower();
            switch (input)
            {
                case "1":
                case "enter another activity":
                case "enter":
                    {
                        EnterActivityMenu(_currentUserID, _listOfDates);
                        break;
                    }
                case "2":
                case "return":
                case "menu":
                case "return to menu":
                    {
                        break;
                    }
                default:
                    Console.WriteLine($"Your entry of {input} was invalid. Please try again.");
                    break;
            }
            
        }

        //View data
        void ViewData(int _currentUserID, List<string> _listOfDates)
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
                        int chosenDateID = ChooseDate(_listOfDates);
                        ViewAllDate(chosenDateID, _currentUserID);
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
                        SelectDescription(_currentUserID);
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

                Console.WriteLine($"~-~-~-~-~-~-~~-~-~-~-~-~-~  \r\nCalculations for User {_currentUser} \r\n~-~-~-~-~-~-~~-~-~-~-~-~-~\r\n\r\n");


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
                NumberOfSchoolEntries(_currentUserID);
                NumberOfPersonal(_currentUserID);
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
            double _timeSpent = 0;
            double _totalTime = 0;
            string viewCatQuery = "SELECT tracked_calendar_dates.calendar_date, activity_categories.category_description, activity_descriptions.activity_description, activity_times.time_spent_on_activity FROM activity_log JOIN activity_categories ON activity_categories.activity_category_id = activity_log.category_description " +
                "JOIN activity_descriptions ON activity_descriptions.activity_description_id = activity_log.activity_description " +
                "JOIN activity_times ON activity_times.activity_time_id = activity_log.time_spent_on_activity " +
                "JOIN tracked_calendar_dates ON tracked_calendar_dates.calendar_date_id = activity_log.calendar_date WHERE activity_log.category_description = @_categoryID AND user_id = @_currentUserID";

            // Prepare SQL Statement
            MySqlCommand viewCatCmd = new MySqlCommand(viewCatQuery, connection);
            viewCatCmd.Parameters.AddWithValue("@_categoryID", _categoryID);
            viewCatCmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);

            MySqlDataReader viewCatRdr;

            viewCatRdr = viewCatCmd.ExecuteReader();
            if (viewCatRdr.HasRows)
            {
                Console.WriteLine("Entries for Selected Category: \r\n");
                while (viewCatRdr.Read())
                {
                    string date = viewCatRdr["calendar_date"].ToString();
                    
                    string description = viewCatRdr["activity_description"].ToString();

                    string _timeSpentString = viewCatRdr["time_spent_on_activity"].ToString();
                    if (!double.TryParse(_timeSpentString, out _timeSpent))
                    {
                        Console.WriteLine("Cannot convert to number");
                    }


                    Console.WriteLine($"{date} - {description} - {_timeSpent} hour(s)\r\n");
                    _totalTime = _totalTime + _timeSpent;


                }
                Console.WriteLine($"Total Time Logged: {_totalTime}");

            }
            else
            {
                Console.WriteLine("No data to display!");
            }
            viewCatRdr.Close();

        }

        //User selects description to view by
        void SelectDescription(int _currentUserID)
        {
            List<string> descriptionList = new List<string>();
            string descQuery = "SELECT activity_description FROM activity_descriptions";

            MySqlCommand descCmd = new MySqlCommand(descQuery, connection);

            MySqlDataReader descRdr = descCmd.ExecuteReader();

            while (descRdr.Read())
            {
                string description = descRdr["activity_description"] as string;
                descriptionList.Add(description);
            }

            for (int i = 0; i < descriptionList.Count; i++)
            {
                Console.WriteLine($"{i}. {descriptionList[i]}");
            }

            int descriptionIndex = Validation.IntValidation("Enter the number to the left of the category to select it: ");
            while (descriptionIndex > descriptionList.Count - 1)
            {
                descriptionIndex = Validation.IntValidation("Your selection is out of bounds. Please try again.");

            }

            int descriptionID = descriptionIndex + 1;
            descRdr.Close();

            Console.WriteLine($"You have chosen {descriptionList[descriptionIndex].ToString()}.");
            ViewDescription(descriptionID, _currentUserID);
        }

        //Shows the user everything with the selected description
        void ViewDescription(int _selectedDescriptionID, int _currentUserID)
        {
            double _timeSpent = 0;
            double _totalTime = 0;
            string viewDescQuery = "SELECT tracked_calendar_dates.calendar_date, activity_categories.category_description, activity_descriptions.activity_description, activity_times.time_spent_on_activity FROM activity_log JOIN activity_categories ON activity_categories.activity_category_id = activity_log.category_description " +
                "JOIN activity_descriptions ON activity_descriptions.activity_description_id = activity_log.activity_description " +
                "JOIN activity_times ON activity_times.activity_time_id = activity_log.time_spent_on_activity " +
                "JOIN tracked_calendar_dates ON tracked_calendar_dates.calendar_date_id = activity_log.calendar_date WHERE activity_log.activity_description = @_descriptionID AND user_id = @_currentUserID";

            // Prepare SQL Statement
            MySqlCommand viewDescCmd = new MySqlCommand(viewDescQuery, connection);
            viewDescCmd.Parameters.AddWithValue("@_descriptionID", _selectedDescriptionID);
            viewDescCmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);

            MySqlDataReader viewDescRdr;

            viewDescRdr = viewDescCmd.ExecuteReader();
            if (viewDescRdr.HasRows)
            {
                Console.WriteLine("Entries for Selected Description: \r\n");
                while (viewDescRdr.Read())
                {
                    string date = viewDescRdr["calendar_date"].ToString();
                    string category = viewDescRdr["category_description"].ToString();
                    string description = viewDescRdr["activity_description"].ToString();

                    string _timeSpentString = viewDescRdr["time_spent_on_activity"].ToString();
                    if (!double.TryParse(_timeSpentString, out _timeSpent))
                    {
                        Console.WriteLine("Cannot convert to number");
                    }


                    Console.WriteLine($"{date} - {category} - {description} - {_timeSpent} hour(s)\r\n");
                    _totalTime = _totalTime + _timeSpent;


                }
                Console.WriteLine($"Total Time Logged: {_totalTime} hour(s).");

            }
            else
            {
                Console.WriteLine("No data to display!");
            }
            viewDescRdr.Close();

        }

        //Shows the user everything with the selected date
        void ViewAllDate(int _chosenDateID, int _currentUserID)
        {
            double _timeSpent = 0;
            double _totalTime = 0;
            string descDateQuery = "SELECT activity_categories.category_description, activity_descriptions.activity_description, activity_times.time_spent_on_activity from activity_log JOIN activity_categories ON activity_categories.activity_category_id = activity_log.category_description " +
                "JOIN activity_descriptions ON activity_descriptions.activity_description_id = activity_log.activity_description " +
                "JOIN activity_times ON activity_times.activity_time_id = activity_log.time_spent_on_activity WHERE calendar_date = @_dateID AND user_id = @_currentUserID";

            // Prepare SQL Statement
            MySqlCommand descDateCmd = new MySqlCommand(descDateQuery, connection);
            descDateCmd.Parameters.AddWithValue("@_dateID", _chosenDateID);
            descDateCmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);

            MySqlDataReader allDateRdr;

            allDateRdr = descDateCmd.ExecuteReader();
            if (allDateRdr.HasRows)
            {
                Console.WriteLine("Entries for Selected Date: \r\n");
                while (allDateRdr.Read())
                {
                    string category = allDateRdr["category_description"].ToString();
                    string description = allDateRdr["activity_description"].ToString();

                    string _timeSpentString = allDateRdr["time_spent_on_activity"].ToString();
                    if (!double.TryParse(_timeSpentString, out _timeSpent))
                    {
                        Console.WriteLine("Cannot convert to number");
                    }
                    

                    Console.WriteLine($"{category} - {description} - {_timeSpent} hour(s)\r\n");
                    _totalTime = _totalTime + _timeSpent;
                    

                }
                Console.WriteLine($"Total Time Logged: {_totalTime}");

            }
            else
            {
                Console.WriteLine("No data to display!");
            }
            allDateRdr.Close();

            
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

        //Tells the user how many entries under school category.
        void NumberOfSchoolEntries(int _currentUserID)
        {
            string numberSchoolEntriesQuery = ("SELECT COUNT(activity_times.time_spent_on_activity) FROM activity_log JOIN activity_times ON activity_times.activity_time_id = activity_log.time_spent_on_activity WHERE category_description = 6 AND user_id = @_currentUserID");

            MySqlCommand numSchoolEntriesCmd = new MySqlCommand(numberSchoolEntriesQuery, connection);
            numSchoolEntriesCmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);

            object numSchoolEntries = numSchoolEntriesCmd.ExecuteScalar();
            Console.WriteLine($"Total School Entries to App: {numSchoolEntries.ToString()}\r\n");
        }

        //Tells the user how many entries under Personal category
        void NumberOfPersonal(int _currentUserID)
        {
            string personalCountQuery = ("SELECT COUNT(activity_times.time_spent_on_activity) FROM activity_log JOIN activity_times ON activity_times.activity_time_id = activity_log.time_spent_on_activity WHERE category_description = 3 AND user_id = @_currentUserID");

            MySqlCommand personalCountCmd = new MySqlCommand(personalCountQuery, connection);
            personalCountCmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);

            object personalCount = personalCountCmd.ExecuteScalar();
            Console.WriteLine($"Total Personal Entries: {personalCount.ToString()}\r\n");
        }

        //Has the user choose a category and returns the database id of that category.
        int ChooseCategory()
        {
            List<string> categoryList = new List<string>();

            string chooseCatQuery = "SELECT category_description FROM activity_categories";
            MySqlCommand chooseCatCmd = new MySqlCommand(chooseCatQuery, connection);

            MySqlDataReader chooseCatRdr;

            chooseCatRdr = chooseCatCmd.ExecuteReader();

            while (chooseCatRdr.Read())
            {
                string category = chooseCatRdr["category_description"] as string;
                categoryList.Add(category);
            }

            chooseCatRdr.Close();

            for (int i = 0; i < categoryList.Count; i++)
            {
                Console.WriteLine($"{i}. {categoryList[i]}");
            }
            int categoryIndex = Validation.IntValidation("Enter your Selection: ");
            
            while (categoryIndex > categoryList.Count - 1)
            {
                Console.WriteLine("That selection was out of bounds, please try again.");
                categoryIndex = Validation.IntValidation("Enter your Selection: ");
            }

            int _categoryID = categoryIndex + 1;

            chooseCatRdr.Close();

            return _categoryID;
        }

        //has the user choose a description and returns the id of that chosen description.
        int ChooseDescription()
        {
            List<string> descriptionList = new List<string>();
            string chooseDescQuery = "SELECT activity_description FROM activity_descriptions";
            MySqlCommand chooseCatCmd = new MySqlCommand(chooseDescQuery, connection);

            MySqlDataReader chooseDescRdr;

            chooseDescRdr = chooseCatCmd.ExecuteReader();

            while (chooseDescRdr.Read())
            {
                string description = chooseDescRdr["activity_description"] as string;
                descriptionList.Add(description);
            }

            chooseDescRdr.Close();

            for (int i = 0; i < descriptionList.Count; i++)
            {
                Console.WriteLine($"{i}. {descriptionList[i]}");
            }
            int descIndex = Validation.IntValidation("Enter your Selection: ");

            while (descIndex > descriptionList.Count - 1)
            {
                Console.WriteLine("That selection was out of bounds, please try again.");
                descIndex = Validation.IntValidation("Enter your Selection: ");
            }

            int descriptionID = descIndex + 1;

            chooseDescRdr.Close();

            return descriptionID;
        }

        //has the user choose a date the activity was performed.
        int ChooseDate(List<string> _listOfDates)
        {
         
            for (int i = 0; i < _listOfDates.Count; i++)
            {
                Console.WriteLine($"{i}. {_listOfDates[i]}");
            }
            int dateIndex = Validation.IntValidation("Enter your Selection: ");

            while (dateIndex > _listOfDates.Count - 1)
            {
                Console.WriteLine("That selection was out of bounds, please try again.");
                dateIndex = Validation.IntValidation("Enter your Selection: ");
            }

            int _dateID = dateIndex + 1;
            

            
            return _dateID;
        }

        //the program chooses which numerical day based off of the chosen date.
        int MonthDay(int _dateID)
        {
            int _monthDayID = 0;
            string monthDayQuery = "SELECT calendar_numerical_day FROM tracked_calendar_days WHERE calendar_day_id = @_dateID";

            MySqlCommand monthDayCmd = new MySqlCommand(monthDayQuery, connection);
            monthDayCmd.Parameters.AddWithValue("@_dateID", _dateID);

            MySqlDataReader monthDayRdr;

            monthDayRdr = monthDayCmd.ExecuteReader();

            while (monthDayRdr.Read())
            {
                string monthDay = monthDayRdr["calendar_numerical_day"].ToString();
                int.TryParse(monthDay, out _monthDayID);
            }

            monthDayRdr.Close();

            return _monthDayID;

        }

        //Program chooses which day of the week based off the numerical day chosen.
        int DayOfWeek(int _monthDayID)
        {
            int dayOfWeek = 0;
            if (_monthDayID == 1 || _monthDayID == 8 || _monthDayID == 15 || _monthDayID == 22)
            {
                dayOfWeek = 1;
                return dayOfWeek;

            } else if (_monthDayID == 2 || _monthDayID == 9 || _monthDayID == 16 || _monthDayID == 23)
            {
                dayOfWeek = 2;
                return dayOfWeek;

            } else if (_monthDayID == 3 || _monthDayID == 10 || _monthDayID == 17 || _monthDayID == 24)
            {
                dayOfWeek = 3;
                return dayOfWeek;

            } else if (_monthDayID == 4 || _monthDayID == 11 || _monthDayID == 18 || _monthDayID == 25)
            {
                dayOfWeek = 4;
                return dayOfWeek;

            } else if (_monthDayID == 5 || _monthDayID == 12 || _monthDayID == 19 || _monthDayID == 26)
            {
                dayOfWeek = 5;
                return dayOfWeek;

            } else if (_monthDayID == 6 || _monthDayID == 13 || _monthDayID == 20)
            {
                dayOfWeek = 6;
                return dayOfWeek;

            } else if (_monthDayID == 7 || _monthDayID == 14 || _monthDayID == 21)
            {
                dayOfWeek = 7;
                return dayOfWeek;
            }
            else
            {
                Console.WriteLine("Not a valid date.");
                return dayOfWeek;
            }
            
        }

        //User enters how long they did the activity for
        int TimeSpent()
        {
            decimal timeSpent = Validation.DecimalValidation("How long did you do this activity for? (15 min Increment Format 0.00 - Example 15mins = 0.25)");
            int timeSpentID = 0;

            if (timeSpent == 0.25m)
            {
                timeSpentID = 1;
                return timeSpentID;

            } else if (timeSpent == 0.5m)
            {
                timeSpentID = 2;
                return timeSpentID;
            }
            else if (timeSpent == 0.75m)
            {
                timeSpentID = 3;
                return timeSpentID;
            }
            else if (timeSpent == 1m)
            {
                timeSpentID = 4;
                return timeSpentID;
            }
            else if (timeSpent == 1.25m)
            {
                timeSpentID = 5;
                return timeSpentID;
            }
            else if (timeSpent == 1.5m)
            {
                timeSpentID = 6;
                return timeSpentID;
            }
            else if (timeSpent == 1.75m)
            {
                timeSpentID = 7;
                return timeSpentID;
            }
            else if (timeSpent == 2m)
            {
                timeSpentID = 8;
                return timeSpentID;
            }
            else if (timeSpent == 2.25m)
            {
                timeSpentID = 9;
                return timeSpentID;
            }
            else if (timeSpent == 2.5m)
            {
                timeSpentID = 10;
                return timeSpentID;
            }
            else if (timeSpent == 2.75m)
            {
                timeSpentID = 11;
                return timeSpentID;
            }
            else if (timeSpent == 3m)
            {
                timeSpentID = 12;
                return timeSpentID;
            }
            else if (timeSpent == 3.25m)
            {
                timeSpentID = 13;
                return timeSpentID;
            }
            else if (timeSpent == 3.5m)
            {
                timeSpentID = 14;
                return timeSpentID;
            }
            else if (timeSpent == 3.75m)
            {
                timeSpentID = 15;
                return timeSpentID;
            }
            else if (timeSpent == 4m)
            {
                timeSpentID = 16;
                return timeSpentID;
            }
            else if (timeSpent == 4.25m)
            {
                timeSpentID = 17;
                return timeSpentID;
            }
            else if (timeSpent == 4.5m)
            {
                timeSpentID = 18;
                return timeSpentID;
            }
            else if (timeSpent == 4.75m)
            {
                timeSpentID = 19;
                return timeSpentID;
            }
            else if (timeSpent == 5.0m)
            {
                timeSpentID = 20;
                return timeSpentID;
            }
            else if (timeSpent == 5.25m)
            {
                timeSpentID = 21;
                return timeSpentID;
            }
            else if (timeSpent == 5.5m)
            {
                timeSpentID = 22;
                return timeSpentID;
            }
            else if (timeSpent == 5.75m)
            {
                timeSpentID = 23;
                return timeSpentID;
            }
            else if (timeSpent == 6m)
            {
                timeSpentID = 24;
                return timeSpentID;
            }
            else if (timeSpent == 6.25m)
            {
                timeSpentID = 25;
                return timeSpentID;
            }
            else if (timeSpent == 6.5m)
            {
                timeSpentID = 26;
                return timeSpentID;
            }
            else if (timeSpent == 6.75m)
            {
                timeSpentID = 27;
                return timeSpentID;
            }
            else if (timeSpent == 7m)
            {
                timeSpentID = 28;
                return timeSpentID;
            }
            else if (timeSpent == 7.25m)
            {
                timeSpentID = 29;
                return timeSpentID;
            }
            else if (timeSpent == 7.5m)
            {
                timeSpentID = 30;
                return timeSpentID;
            }
            else if (timeSpent == 7.75m)
            {
                timeSpentID = 31;
                return timeSpentID;
            }
            else if (timeSpent == 8m)
            {
                timeSpentID = 32;
                return timeSpentID;
            }
            else if (timeSpent == 8.25m)
            {
                timeSpentID = 33;
                return timeSpentID;
            }
            else if (timeSpent == 8.5m)
            {
                timeSpentID = 34;
                return timeSpentID;
            }
            else if (timeSpent == 8.75m)
            {
                timeSpentID = 35;
                return timeSpentID;
            }
            else if (timeSpent == 9m)
            {
                timeSpentID = 36;
                return timeSpentID;
            }
            else if (timeSpent == 9.25m)
            {
                timeSpentID = 37;
                return timeSpentID;
            }
            else if (timeSpent == 9.5m)
            {
                timeSpentID = 38;
                return timeSpentID;
            }
            else if (timeSpent == 9.75m)
            {
                timeSpentID = 39;
                return timeSpentID;
            }
            else if (timeSpent == 10m)
            {
                timeSpentID = 40;
                return timeSpentID;
            }
            else if (timeSpent == 10.25m)
            {
                timeSpentID = 41;
                return timeSpentID;
            }
            else if (timeSpent == 10.5m)
            {
                timeSpentID = 42;
                return timeSpentID;
            }
            else if (timeSpent == 10.75m)
            {
                timeSpentID = 43;
                return timeSpentID;
            }
            else if (timeSpent == 11m)
            {
                timeSpentID = 44;
                return timeSpentID;
            }
            else if (timeSpent == 11.25m)
            {
                timeSpentID = 45;
                return timeSpentID;
            }
            else if (timeSpent == 11.5m)
            {
                timeSpentID = 46;
                return timeSpentID;
            }
            else if (timeSpent == 11.75m)
            {
                timeSpentID = 47;
                return timeSpentID;
            }
            else if (timeSpent == 12m)
            {
                timeSpentID = 48;
                return timeSpentID;
            }
            else if (timeSpent == 12.25m)
            {
                timeSpentID = 49;
                return timeSpentID;
            }
            else if (timeSpent == 12.5m)
            {
                timeSpentID = 50;
                return timeSpentID;
            }
            else if (timeSpent == 12.75m)
            {
                timeSpentID = 51;
                return timeSpentID;
            }
            else if (timeSpent == 13m)
            {
                timeSpentID = 52;
                return timeSpentID;
            }
            else if (timeSpent == 13.25m)
            {
                timeSpentID = 53;
                return timeSpentID;
            }
            else if (timeSpent == 13.5m)
            {
                timeSpentID = 54;
                return timeSpentID;
            }
            else if (timeSpent == 13.75m)
            {
                timeSpentID = 55;
                return timeSpentID;
            }
            else if (timeSpent == 14m)
            {
                timeSpentID = 56;
                return timeSpentID;
            }
            else if (timeSpent == 14.25m)
            {
                timeSpentID = 57;
                return timeSpentID;
            }
            else if (timeSpent == 14.5m)
            {
                timeSpentID = 58;
                return timeSpentID;
            }
            else if (timeSpent == 14.75m)
            {
                timeSpentID = 59;
                return timeSpentID;
            }
            else if (timeSpent == 15.0m)
            {
                timeSpentID = 60;
                return timeSpentID;
            }
            else if (timeSpent == 15.25m)
            {
                timeSpentID = 61;
                return timeSpentID;
            }
            else if (timeSpent == 15.5m)
            {
                timeSpentID = 62;
                return timeSpentID;
            }
            else if (timeSpent == 15.75m)
            {
                timeSpentID = 63;
                return timeSpentID;
            }
            else if (timeSpent == 16m)
            {
                timeSpentID = 64;
                return timeSpentID;
            }
            else if (timeSpent == 16.25m)
            {
                timeSpentID = 65;
                return timeSpentID;
            }
            else if (timeSpent == 16.5m)
            {
                timeSpentID = 66;
                return timeSpentID;
            }
            else if (timeSpent == 16.75m)
            {
                timeSpentID = 67;
                return timeSpentID;
            }
            else if (timeSpent == 17m)
            {
                timeSpentID = 68;
                return timeSpentID;
            }
            else if (timeSpent == 17.25m)
            {
                timeSpentID = 69;
                return timeSpentID;
            }
            else if (timeSpent == 17.5m)
            {
                timeSpentID = 70;
                return timeSpentID;
            }
            else if (timeSpent == 17.75m)
            {
                timeSpentID = 71;
                return timeSpentID;
            }
            else if (timeSpent == 18m)
            {
                timeSpentID = 72;
                return timeSpentID;
            }
            else if (timeSpent == 18.25m)
            {
                timeSpentID = 73;
                return timeSpentID;
            }
            else if (timeSpent == 18.5m)
            {
                timeSpentID = 74;
                return timeSpentID;
            }
            else if (timeSpent == 18.75m)
            {
                timeSpentID = 75;
                return timeSpentID;
            }
            else if (timeSpent == 19m)
            {
                timeSpentID = 76;
                return timeSpentID;
            }
            else if (timeSpent == 19.25m)
            {
                timeSpentID = 77;
                return timeSpentID;
            }
            else if (timeSpent == 19.5m)
            {
                timeSpentID = 78;
                return timeSpentID;
            }
            else if (timeSpent == 19.75m)
            {
                timeSpentID = 79;
                return timeSpentID;
            }
            else if (timeSpent == 20m)
            {
                timeSpentID = 80;
                return timeSpentID;
            }
            else if (timeSpent == 20.25m)
            {
                timeSpentID = 81;
                return timeSpentID;
            }
            else if (timeSpent == 20.5m)
            {
                timeSpentID = 82;
                return timeSpentID;
            }
            else if (timeSpent == 20.75m)
            {
                timeSpentID = 83;
                return timeSpentID;
            }
            else if (timeSpent == 21m)
            {
                timeSpentID = 84;
                return timeSpentID;
            }
            else if (timeSpent == 21.25m)
            {
                timeSpentID = 85;
                return timeSpentID;
            }
            else if (timeSpent == 21.5m)
            {
                timeSpentID = 86;
                return timeSpentID;
            }
            else if (timeSpent == 21.75m)
            {
                timeSpentID = 87;
                return timeSpentID;
            }
            else if (timeSpent == 22m)
            {
                timeSpentID = 88;
                return timeSpentID;
            }
            else if (timeSpent == 22.25m)
            {
                timeSpentID = 89;
                return timeSpentID;
            }
            else if (timeSpent == 22.5m)
            {
                timeSpentID = 90;
                return timeSpentID;
            }
            else if (timeSpent == 22.75m)
            {
                timeSpentID = 91;
                return timeSpentID;
            }
            else if (timeSpent == 23m)
            {
                timeSpentID = 92;
                return timeSpentID;
            }
            else if (timeSpent == 23.25m)
            {
                timeSpentID = 93;
                return timeSpentID;
            }
            else if (timeSpent == 23.5m)
            {
                timeSpentID = 94;
                return timeSpentID;
            }
            else if (timeSpent == 23.75m)
            {
                timeSpentID = 95;
                return timeSpentID;
            }
            else if (timeSpent == 24m)
            {
                timeSpentID = 96;
                return timeSpentID;
            }
            else
            {
                timeSpentID = 0;
                Console.WriteLine($"Your entry of {timeSpent} was invalid.");
                return timeSpentID;
            }
        }

        //Pushes new log entry to DB
        void EnterActivityToDB(int _currentUserID, int _monthDayID, int _dateID, int _categoryID, int _descriptionID, int _dayOfWeek, int _timeSpentID)
        {
            string enterActivityQuery = "INSERT INTO activity_log(user_id, calendar_day, calendar_date, day_name, category_description, activity_description, time_spent_on_activity) " +
                "VALUES (@_currentUserID, @_monthDayID, @_dateID, @_dayOfWeek, @_categoryID, @_descriptionID, @_timeSpentID)";

            MySqlCommand enterCmd = new MySqlCommand(enterActivityQuery, connection);
            enterCmd.Parameters.AddWithValue("@_currentUserID",_currentUserID );
            enterCmd.Parameters.AddWithValue("@_monthDayID", _monthDayID);
            enterCmd.Parameters.AddWithValue("@_dateID", _dateID);
            enterCmd.Parameters.AddWithValue("@_dayOfWeek", _dayOfWeek);
            enterCmd.Parameters.AddWithValue("@_categoryID", _categoryID);
            enterCmd.Parameters.AddWithValue("@_descriptionID", _descriptionID);
            enterCmd.Parameters.AddWithValue("@_timeSpentID", _timeSpentID);

            MySqlDataReader enterRdr;

            enterRdr = enterCmd.ExecuteReader();
            enterRdr.Close();

            Console.WriteLine("The new activity has been logged!");
        }

        //Populates list of dates as strings.
        List<string> PopulateDateList()
        {
            List<string> _listOfDates = new List<string>();

            string stm = "SELECT calendar_date from tracked_calendar_dates";

            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, connection);

            MySqlDataReader rdr;

            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string _dates = rdr["calendar_date"].ToString();
                _listOfDates.Add(_dates);
            }
            rdr.Close();
            return _listOfDates;
        }

        //View dates for selected Category
        List<string> SeeDatesCat(int _categoryID, int _currentUserID)
        {
            List<string> _dateList = new List<string>();
            string stm = "SELECT tracked_calendar_dates.calendar_date from activity_log JOIN tracked_calendar_dates ON tracked_calendar_dates.calendar_date_id = activity_log.calendar_date WHERE category_description = @_categoryID AND user_id = @_currentUserID";

            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, connection);
            cmd.Parameters.AddWithValue("@_categoryID",_categoryID);
            cmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);

            MySqlDataReader dateRdr;

            dateRdr = cmd.ExecuteReader();
            if (dateRdr.HasRows)
            {
                while (dateRdr.Read())
                {
                    string date = dateRdr["calendar_date"].ToString();
                    _dateList.Add(date);

                }

                for (int i = 0; i < _dateList.Count; i++)
                {
                    Console.WriteLine($"{i}. {_dateList[i].ToString()}");
                }
            }
            else
            {
                Console.WriteLine("No data to display!");
            }
            dateRdr.Close();
            return _dateList;
        }

        //View dates for selected Category
        List<string> SeeDatesDesc(int _descID, int _currentUserID)
        {
            List<string> _dateList = new List<string>();
            string descDateQuery = "SELECT tracked_calendar_dates.calendar_date from activity_log JOIN tracked_calendar_dates ON tracked_calendar_dates.calendar_date_id = activity_log.calendar_date WHERE activity_description = @_descID AND user_id = @_currentUserID";

            // Prepare SQL Statement
            MySqlCommand descDateCmd = new MySqlCommand(descDateQuery, connection);
            descDateCmd.Parameters.AddWithValue("@_descID", _descID);
            descDateCmd.Parameters.AddWithValue("@_currentUserID", _currentUserID);

            MySqlDataReader dateRdr;

            dateRdr = descDateCmd.ExecuteReader();
            if (dateRdr.HasRows)
            {
                while (dateRdr.Read())
                {
                    string date = dateRdr["calendar_date"].ToString();
                    _dateList.Add(date);

                }

                for (int i = 0; i < _dateList.Count; i++)
                {
                    Console.WriteLine($"{i}. {_dateList[i].ToString()}");
                }
            }
            else
            {
                Console.WriteLine("No data to display!");
            }
            dateRdr.Close();
            return _dateList;
        }


    }
}
