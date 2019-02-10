using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkmanCiera_Exercise2
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Ciera Workman
             * Project & Portfolio 2
             * Exercise 2
             */

            //Monthly Topic - Feb - Clothing Brands
            //Clue game
            //answer = "Prada";

            Console.Clear();
            List<string> listOfClues = new List<string>();
            listOfClues = PopulateList();

            Console.WriteLine("Welcome to the Clue Game!\r\n");
            
            //This bool controls the loop.
            bool programIsRunning = true;
            while (programIsRunning)
            {
                bool correctAnswer;
                //Cycles through the list of clues to output them to the user.
                for (int i = 0; i < listOfClues.Count; i++)
                {
                    Console.WriteLine(listOfClues[i]);
                   correctAnswer = KnowAnswer(listOfClues);

                    //Ends the program if the last clue has been output and no correct answer has been entered.
                    if (i == 9 && correctAnswer == false)
                    {
                        Console.WriteLine("The correct answer is Prada.\r\nThanks for playing!");
                        programIsRunning = false;
                        break;
                    }
                    //Ends the program if the correct answer has been found.
                    if (correctAnswer == true)
                    {
                        Console.WriteLine("Thanks for playing!");
                        programIsRunning = false;
                        break;
                    }
                }

                Utility.PauseBeforeContinuing();
                
            }
        }

        //This function populates the list of clues.
        public static List<string> PopulateList()
        {
            List<string> _listOfClues = new List<string>();
            string clue1 = "CLUE 1: This clothing brand was founded in 1913.";
            _listOfClues.Add(clue1);
            string clue2 = "CLUE 2: The headquarters of this brand is in Milan, Italy.";
            _listOfClues.Add(clue2);
            string clue3 = "CLUE 3: In 1919, this brand is awarded Offical Supplier of the Italian Royal House.";
            _listOfClues.Add(clue3);
            string clue4 = "CLUE 4: In 1979, this brand's first collection of women's shoes is presented.";
            _listOfClues.Add(clue4);
            string clue5 = "CLUE 5: In the early 1990's, this brand expanded to China, Japan, and the USA.";
            _listOfClues.Add(clue5);
            string clue6 = "CLUE 6: This brand gains control of the Car Shoe in 2001.";
            _listOfClues.Add(clue6);
            string clue7 = "CLUE 7: This brand opens it's American Headquarters on 51st Street, New York in 2002.";
            _listOfClues.Add(clue7);
            string clue8 = "CLUE 8: This brand's fragrances for women were debuted in 2004. The men's line was debuted in 2006.";
            _listOfClues.Add(clue8);
            string clue9 = "CLUE 9: This brand released an LG touchscreen phone in 2007.";
            _listOfClues.Add(clue9);
            string clue10 = ("CLUE 10: The New York Costume Institute of the Metropolitan Museum of Art creates an exhibition between this brand and designer Schiaparelli.");
               
            _listOfClues.Add(clue10);


            return _listOfClues;
        }

        //This function asks the user if they know the answer, then directs them to the answer checker or to the next clue.
        public static bool KnowAnswer(List<string> _listOfClues)
        {
            bool answerIsFound = false;
            Console.Write("\r\nDo you know the answer? (yes/no): ");
            string input = Console.ReadLine().ToLower();
            switch (input)
            {
                case "y":
                case "yes":
                    {
                        answerIsFound = AnswerIsKnown();
                        return answerIsFound;
                        
                    }
                case "n":
                case "no":
                    {
                        return answerIsFound;
                        
                    }
                default:
                    Console.WriteLine($"Your input of {input} is invalid. Please enter yes/y or no/n.");
                    return answerIsFound;
                    
            }
        }

        //This function checks to see if the user's input matches the answer exactly.
        public static bool AnswerIsKnown()
        {
            bool isCorrect = false;
            Console.Write("Enter your answer: ");
            string userAnswer = Console.ReadLine();
            char[] userAnswerArray = userAnswer.ToCharArray();
            
            if (char.IsUpper(userAnswerArray[0]) && userAnswerArray[0] == 'P' && userAnswerArray[1] == 'r' && userAnswerArray[2] == 'a' && userAnswerArray[3] == 'd' && userAnswerArray[4] == 'a')
            {
                Console.WriteLine("That is correct!");
                isCorrect = true;
                return isCorrect;
            }
            else if (char.IsLower(userAnswerArray[0]) && userAnswerArray[0] == 'p' && userAnswerArray[1] == 'r' && userAnswerArray[2] == 'a' && userAnswerArray[3] == 'd' && userAnswerArray[4] == 'a')
            {
                Console.WriteLine("Your answer is almost right! It must begin with a captial letter.");
                return isCorrect;
            }
            else
            {
                Console.WriteLine("That is incorrect. Please try again.");
                return isCorrect;
            }
           
        }
        
    }
}
