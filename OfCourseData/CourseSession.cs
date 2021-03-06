﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OfCourseData
{
    public partial class CourseSession
    {
        public int CourseSessionId { get; set; }
        public int CourseId { get; set; }
        public DateTime TimeOfSession { get; set; }
        public DateTime DateOfSession { get; set; }

        public virtual Course Course { get; set; }

        // CourseId, max people, booked customers
    }
}