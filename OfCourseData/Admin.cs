using System;
using System.Collections.Generic;
using System.Text;

namespace OfCourseData
{
    public class Admin
    {
        public Admin()
        {
            
        }

        public int AdminId { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }


        public void Approve()
        {
            throw new System.NotImplementedException();
        }
    }
}