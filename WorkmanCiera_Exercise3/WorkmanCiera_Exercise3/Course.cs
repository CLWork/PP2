using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkmanCiera_Exercise3
{
    class Course
    {
        private string title;
        private decimal GPA;

        public Course (string _title)
        {
            title = _title;
        }

        public decimal GetGPA { get { return GPA; } set { GPA = value; } }
        public string GetTitle { get { return title; } }

        private void ClassGPA()
        {

        }
    }
}
