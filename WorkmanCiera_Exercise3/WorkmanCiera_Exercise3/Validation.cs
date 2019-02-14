using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkmanCiera_Exercise3

{
    class Validation
    {
        public static string StringValidation(string message)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("This field cannot be blank. Please try again.");
                input = Console.ReadLine();
            }
            return input;
        }

        public static int IntValidation(string message)
        {
            Console.WriteLine(message);
            int numberInput;
            string numberString = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(numberString) || !int.TryParse(numberString, out numberInput) || numberInput < 0 )
            {
                Console.WriteLine("This field cannot be blank and must be a number at or above 0. Please try again.");
                numberString = Console.ReadLine();
            }
            return numberInput;
            
        }


        public static decimal DecimalValidation(string message)
        {
            Console.WriteLine(message);
            decimal numberInput;
            string numberString = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(numberString) || !decimal.TryParse(numberString, out numberInput) || numberInput < 0 || numberInput > 100)
            {
                Console.WriteLine("This field cannot be blank and must be a number between 0 and 100. Please try again.");
                numberString = Console.ReadLine();
            }
            return numberInput;

        }

        public static float FloatValidation(string message)
        {
            Console.WriteLine(message);
            float numberInput;
            string numberString = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(numberString) || !float.TryParse(numberString, out numberInput) || numberInput < 0)
            {
                Console.WriteLine("This field cannot be blank and must be a number above 0. Please try again.");
                numberString = Console.ReadLine();
            }
            return numberInput;

        }


    }
}
