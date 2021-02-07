using System;
using System.Collections.Generic;
using System.Text;

namespace OfCourseData
{
    public abstract class User
    {
        public string PaymentDetails
        {
            get => default;
            set
            {
            }
        }

        public string[] InterestTags
        {
            get => default;
            set
            {
            }
        }

        public string Location
        {
            get => default;
            set
            {
            }
        }

        public int TimePreferred
        {
            get => default;
            set
            {
            }
        }

        public ICollection<Course> Courses
        {
            get => default;
            set
            {
            }
        }
    }
}