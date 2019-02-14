using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkmanCiera_Exercise3
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Ciera Workman
             * Project & Portfolio 2
             * Exercise 3 - Calculate Grades
             */
            Program instance = new Program();
            List<Students> listOfStudents = new List<Students>();
            listOfStudents = CoursesAndStudents();

            bool programIsRunning = true;
            while (programIsRunning)
            {
                Console.Clear();
                instance.DisplayMenu();
                Console.Write("Enter your Selection: ");
                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "1":
                        {
                            //View Students
                            instance.ViewStudents(listOfStudents);
                            break;
                        }
                    case "2":
                        {
                            //View Class' GPA's.
                            break;
                        }
                    case "3":
                        {
                            //Edit student's grade
                            break;
                        }
                    case "4":
                        {
                            //Quit
                            programIsRunning = false;
                            break;
                        }

                    default:
                        Console.WriteLine($"Your input of {input} was invalid. Please try again.");
                        break;
                }
                Utility.PauseBeforeContinuing();
            }
        }
        void DisplayMenu()
        {
            Console.WriteLine("----------\r\n  Menu\r\n----------\r\n");
            Console.WriteLine("1. View Students\r\n" +
                "2. View Class GPA\r\n" +
                "3. Edit Student\r\n" +
                "4. Exit\r\n");
        }
        public static List<Students> CoursesAndStudents()
        {
            List<Course> courseList = new List<Course>();
            Course firstCourse = new Course("The History of Prada");
            courseList.Add(firstCourse);

            Course secondCourse = new Course("Success Tips From Louis Vutton");
            courseList.Add(secondCourse);

            Course thirdCourse = new Course("Plus Sizing by Torrid");
            courseList.Add(thirdCourse);

            Course fourthCourse = new Course("Calvin Klein: Men's Fashion");
            courseList.Add(fourthCourse);

            Course fifthCourse = new Course("Women's Fashion History");
            courseList.Add(fifthCourse);

            List<Students> newStudents = new List<Students>(5);
            List<decimal> listOfGrades1 = new List<decimal>();
            listOfGrades1.Add(70.87m);
            listOfGrades1.Add(80.64m);
            listOfGrades1.Add(88.00m);
            listOfGrades1.Add(90.99m);
            listOfGrades1.Add(87.87m);

            List<decimal> listOfGrades2 = new List<decimal>();
            listOfGrades1.Add(90.00m);
            listOfGrades1.Add(80.50m);
            listOfGrades1.Add(88.70m);
            listOfGrades1.Add(91.96m);
            listOfGrades1.Add(83.33m);

            List<decimal> listOfGrades3 = new List<decimal>();
            listOfGrades1.Add(100.00m);
            listOfGrades1.Add(50.50m);
            listOfGrades1.Add(82.20m);
            listOfGrades1.Add(70.43m);
            listOfGrades1.Add(81.79m);

            List<decimal> listOfGrades4 = new List<decimal>();
            listOfGrades1.Add(50.87m);
            listOfGrades1.Add(60.74m);
            listOfGrades1.Add(86.80m);
            listOfGrades1.Add(92.29m);
            listOfGrades1.Add(91.87m);

            List<decimal> listOfGrades5 = new List<decimal>();
            listOfGrades1.Add(71.87m);
            listOfGrades1.Add(84.64m);
            listOfGrades1.Add(81.02m);
            listOfGrades1.Add(99.59m);
            listOfGrades1.Add(89.77m);

            Students firstStudent = new Students("Casey", "Lowery", courseList, listOfGrades1);
            Students secondStudent = new Students("Crystal", "Harrison", courseList, listOfGrades2);
            Students thirdStudent = new Students("Meri", "Kingston", courseList, listOfGrades3);
            Students fourthStudent = new Students("Lillth", "Eden", courseList, listOfGrades4);
            Students fifthStudent = new Students("John", "Byrns", courseList, listOfGrades5);

            newStudents.Add(firstStudent);
            newStudents.Add(secondStudent);
            newStudents.Add(thirdStudent);
            newStudents.Add(fourthStudent);
            newStudents.Add(fifthStudent);

            return newStudents;
        }
        void ViewStudents(List<Students> _listOfStudents)
        {
            for (int i = 0; i < _listOfStudents.Count; i++)
            {
                foreach (Students student in _listOfStudents)
                {
                    Console.WriteLine($"{i}. Name: {student.FirstName.ToString()} {student.LastName.ToString()}");
                    Console.WriteLine("Enrolled Classes: \r\n" +
                        $"{student.CoursesTaken[i].ToString()} - {student.StudentClassGrades[i]} - {student.GetLetterGrade.ToString()}");
                    
                }
            }
        }
    }
}
