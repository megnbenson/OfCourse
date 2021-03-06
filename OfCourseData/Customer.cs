﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OfCourseData
{
    public partial class Customer
    {
        
        public Customer()
        {
            
           
            BookedCourses = new HashSet<Course>();
            //LikedCourses = new HashSet<Course>();
        }

        public int CustomerId { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }


        public virtual ICollection<Course> BookedCourses { get; set; }
        //public virtual ICollection<Course> LikedCourses { get; set; }
    }
}