using System;
using System.Collections.Generic;
using System.Text;

namespace OfCourseData
{
    public partial class CourseSessionDetails : Course
    {
        
        public DateTime TimeOfSession { get; set; }
        public DateTime DateOfSession { get; set; }

        // CourseId, max people, booked customers
    }
}