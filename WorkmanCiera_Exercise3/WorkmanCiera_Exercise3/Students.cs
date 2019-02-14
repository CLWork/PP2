using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkmanCiera_Exercise3
{
    class Students
    {
        private string firstName;
        private string lastName;
        private List<Course> coursesTaken;
        private decimal overallGrade;
        private List<decimal> classGrades;
        private string letterGrade;

        public Students(string _firstName, string _lastName, List<Course> _coursesTaken, List<decimal> _classGrades)
        {
            firstName = _firstName;
            lastName = _lastName;
            coursesTaken = _coursesTaken;
            classGrades = _classGrades;
        }

        public string FirstName { get { return firstName; } }
        public string LastName { get { return lastName; } }
        public List<Course> CoursesTaken { get { return coursesTaken; } }
        public List<decimal> StudentClassGrades { get { return classGrades; } }
        public decimal OverAllGrade { get { return overallGrade; } set { overallGrade = value; } } 
        public string GetLetterGrade { get { return letterGrade; } set { letterGrade = value; } }

        public decimal UpdateOverallGPA()
        {
            decimal sumOfGrades = 0m;
            for (int i = 0; i < StudentClassGrades.Count; i++)
            {
                sumOfGrades += sumOfGrades + StudentClassGrades[i];
            }

            OverAllGrade = sumOfGrades / 5;
            return OverAllGrade;
            
        }

        public void UpdateClassGrade()
        {
            for(int i = 0; i < CoursesTaken.Count; i++)
            {
                foreach (Course crse in CoursesTaken)
                {
                    Console.WriteLine($"{i}. {crse.GetTitle}");
                }
                i++;
            }
            int index = Validation.IntValidation("Which class did you wish to update the grade for?");
            if (index > CoursesTaken.Count)
            {
                Console.WriteLine("That index does not exist, please try again.");
            }
            else
            {
                decimal newGrade = Validation.DecimalValidation("Enter the new grade (example: 77.87): ");
                CoursesTaken[index].GetGPA = newGrade;
                UpdateOverallGPA();
            }
            
        }

        public void UpdateLetterGrade()
        {
            if (OverAllGrade == 0 || OverAllGrade <= 69.4m)
            {
                letterGrade = "F";

            } else if (OverAllGrade >= 69.5m || OverAllGrade <= 72.4m)
            {
                letterGrade = "D";

            } else if (OverAllGrade >= 72.5m || OverAllGrade <= 79.4m)
            {
                letterGrade = "C";

            } else if (OverAllGrade >= 79.5m || OverAllGrade <= 89.4m)
            {
                letterGrade = "B";

            } else if (OverAllGrade >= 89.5m || OverAllGrade <= 100.00m)
            {
                letterGrade = "A";
            }
        }
        
    }
}
