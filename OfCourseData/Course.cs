using System;
using System.Collections.Generic;
using System.Text;

namespace OfCourseData
{
    public partial class Course
    {
       

        public Course()
        {
            //Title = title;
            //Description = description;
            //City = city;
            //PostCode = postcode;
            //PricePerSession = pricePerSession;
            CourseSessionDetailList = new HashSet<CourseSessionDetails>();
            BookedCustomers = new HashSet<Customer>();
        }

        public int CourseId { get; set; }
        public int? CategoryId { get; set; }
        public int? TrainerId { get; set; }
        public String Title { get; set; }
        public String? Description { get; set; }
       
        public bool IsApproved {
            get => default;
            set
            {
                value = false;
            }
        }
        public float PricePerSession { get; set; }
        public int MaxPeople { get; set; }
        public int TotalSessions { get; set; }
        public int SessionLengthMinutes { get; set; }

        public string City { get; set; }
        public string? PostCode { get; set; }

        public virtual Category Category { get; set; }

        public virtual Trainer Trainer { get; set; }
        public virtual ICollection<CourseSessionDetails> CourseSessionDetailList { get; set; }
        public virtual ICollection<Customer> BookedCustomers { get; set; }




        //public void ToSring()
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}