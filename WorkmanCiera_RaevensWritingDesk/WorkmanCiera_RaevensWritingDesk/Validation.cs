using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkmanCiera_RaevensWritingDesk

{
    class Validation
    {
        public static string StringValidation(string message)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("This field cannot be blank. Please try again.");
                Console.WriteLine(message);
                input = Console.ReadLine();
            }
            return input;
        }

        public static int IntValidation(string message)
        {
            Console.WriteLine(message);
            int numberInput;
            string numberString = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(numberString) || !int.TryParse(numberString, out numberInput) || numberInput < 0)
            {
                Console.WriteLine("This field cannot be blank and must be a number 0 or above. Please try again.");
                numberString = Console.ReadLine();
            }
            return numberInput;
            
        }


        public static decimal DecimalValidation(string message)
        {
            Console.WriteLine(message);
            decimal numberInput;
            string numberString = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(numberString) || !decimal.TryParse(numberString, out numberInput) || numberInput < 0)
            {
                Console.WriteLine("This field cannot be blank and must be a number above 0. Please try again.");
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
