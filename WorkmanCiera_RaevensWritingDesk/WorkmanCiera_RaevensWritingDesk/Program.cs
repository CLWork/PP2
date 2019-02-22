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
    }
}
