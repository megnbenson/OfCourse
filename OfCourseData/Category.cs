using System;
using System.Collections.Generic;
using System.Text;

namespace OfCourseData
{
    public partial class Category
    {
        public Category()
        {
            Courses = new HashSet<Course>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
       
        public virtual ICollection<Course> Courses { get; set; }
    }
}