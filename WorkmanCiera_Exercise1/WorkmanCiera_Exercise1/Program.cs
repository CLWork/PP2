using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkmanCiera_Exercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Ciera Workman
             *  Project & Portfolio 2
             *  Exercise 1
             */

            //Monthly Topic - Feb - Clothing Brands
            List<string> listOfBrands = new List<string>();
            listOfBrands = PopulateList();

            //Set up boolean to control while loop
            bool programIsRunning = true;

            while (programIsRunning)
            {
                Console.Clear();
                DisplayMenu();
                Console.Write("Enter your selection: ");
                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "1":
                        {
                            //Sort list A-Z
                            if (listOfBrands == null)
                            {
                                Console.WriteLine("You must repopulate the List first!");
                            }
                            else
                            {
                                SortAZ(listOfBrands);
                            }
                            break;
                        }
                    case "2":
                        {
                            //Sort list Z-A
                            break;
                        }
                    case "3":
                        {
                            //View list without sorting
                            ViewList(listOfBrands);
                            break;
                        }
                    case "4":
                        {
                            //Remove all items from list one by one
                            listOfBrands = RemoveRandomBrands(listOfBrands);
                            break;
                        }
                    case "5":
                        {
                            //Repopulate list to start over
                            if (listOfBrands == null)
                            {
                                listOfBrands = PopulateList();
                                Console.WriteLine("The list has been restored.");
                            }
                            else
                            {
                                Console.WriteLine("You must use the Remove option first!");
                            }
                            break;
                        }
                    case "6":
                        {
                            //exit
                            programIsRunning = false;
                            break;
                        }
                    default:
                        Console.WriteLine($"Your entry of {input} is invalid. Please try again.");
                        break;
                }
                //Forces program to take a pause between each selection
                Utility.PauseBeforeContinuing();
            }
            

            
        }
        //The menu from the user to select from.
        public static void DisplayMenu()
        {
            Console.WriteLine("----------\r\n  Menu\r\n----------\r\n");
            Console.WriteLine("1. Sort List A-Z\r\n" +
                "2. Sort List Z-A\r\n" +
                "3. View List\r\n" +
                "3. Remove Items From List\r\n" +
                "4. Repopulate List\r\n" +
                "5. Exit\r\n");
        }

        //The method to populate the list of brands the user interacts with.
        public static List<string> PopulateList()
        {
            List<string> _listOfBrands = new List<string>();
            string brand1 = "Victoria's Secret";
            _listOfBrands.Add(brand1);
            string brand2 = "Louis Vutton";
            _listOfBrands.Add(brand2);
            string brand3 = "Nike";
            _listOfBrands.Add(brand3);
            string brand4 = "Torrid";
            _listOfBrands.Add(brand4);
            string brand5 = "Burberry";
            _listOfBrands.Add(brand5);
            string brand6 = "Tommy Hilfiger";
            _listOfBrands.Add(brand6);
            string brand7 = "Prada";
            _listOfBrands.Add(brand7);
            string brand8 = "Hermes";
            _listOfBrands.Add(brand8);
            string brand9 = "Gucci";
            _listOfBrands.Add(brand9);
            string brand10 = "Chanel";
            _listOfBrands.Add(brand10);
            string brand11 = "Ralph Lauren";
            _listOfBrands.Add(brand11);
            string brand12 = "House of Versace";
            _listOfBrands.Add(brand12);
            string brand13 = "Fendi";
            _listOfBrands.Add(brand13);
            string brand14 = "Armani";
            _listOfBrands.Add(brand14);
            string brand15 = "Under Armour";
            _listOfBrands.Add(brand15);
            string brand16 = "Calvin Klein";
            _listOfBrands.Add(brand16);
            string brand17 = "Levi's";
            _listOfBrands.Add(brand17);
            string brand18 = "Michael Kors";
            _listOfBrands.Add(brand18);
            string brand19 = "American Eagle";
            _listOfBrands.Add(brand19);
            string brand20 = "North Face";
            _listOfBrands.Add(brand20);

            return _listOfBrands;
        }

        //The method to sort the list A-Z
        public static void SortAZ(List<string> _listOfBrands)
        {
            _listOfBrands.Sort();
            for (int i = 0; i < _listOfBrands.Count; i++)
            {
                Console.WriteLine(_listOfBrands[i].ToString());
            }
            
        }
        public static void SortZA(List<string> _listOfBrands)
        {

        }

        //Method to remove a random index in the list until the list is depleted.
        public static List<string> RemoveRandomBrands(List<string> _listOfBrands)
        {
            Random rndm = new Random();
            if (_listOfBrands != null && _listOfBrands.Count > 0)   
            {
                for (int i = 0; i < _listOfBrands.Count; i++)
                {
                    int randomIndex = rndm.Next(0, _listOfBrands.Count - 1);
                    Console.WriteLine($"The brand {_listOfBrands[randomIndex]} removed.");
                    _listOfBrands.RemoveAt(randomIndex);

                    

                    i++;
                }
                
                
            }
            else
            {
                Console.WriteLine("Nothing to remove. Please repopulate the list.");
            }
            return _listOfBrands;
        }
        public static void ViewList(List<String> _listOfBrands)
        {
            int i = 0;
            for (i = 0; i < _listOfBrands.Count; i++)
            {
                Console.WriteLine($"{i}. {_listOfBrands[i]}");
            }
        }
        
    }
}
