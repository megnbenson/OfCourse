using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfCourseData.Services
{
    public class CourseService : ICourseService
    {
        //These three are making a constructor of the database, so we don't have to use the using db's
        private readonly OfCourseContext _context;

        public CourseService(OfCourseContext context)
        {
            _context = context;
        }
        public CourseService()
        {
            _context = new OfCourseContext();
        }

        public void Add(Course c)
        {
            _context.Add(c);
            _context.SaveChanges();
        }

        public Course GetCourseById(int courseId)
        {

                return _context.Courses.Where(c => c.CourseId == courseId).FirstOrDefault();

        }

        public List<Course> GetCourseList()
        {

                return _context.Courses.ToList();
            
        }

        public void SaveCourseChanges()
        {

                
                _context.SaveChanges();
            
        }

        public int GetCategoryIdFromTitle(string categorytitle)
        {
            return _context.Categories.Where(c => c.CategoryName.Equals(categorytitle)).First().CategoryId;
        }

        public Tuple<string, int> CheckIfCustomer(string username, string password)
        {
            Tuple<string, int> userPass = Tuple.Create("E", -1); 
            var findCustomer = _context.Customers.Where(c => c.Username.ToLower().Equals(username.ToLower()));
            if (findCustomer.ToList().Count != 0)
            {
                if (findCustomer.First().Password == password)
                {

                    userPass = Tuple.Create("C", findCustomer.First().CustomerId);
                    
                }
            }
            
            return userPass;
        }

        public Tuple<string, int> CheckIfTrainer(string username, string password)
        {
            Tuple<string, int> userPass = Tuple.Create("E", -1);
            var findTrainer = _context.Trainers.Where(t => t.Username.ToLower().Equals(username.ToLower()));
            if (findTrainer.ToList().Count != 0)
            {
                if (findTrainer.First().Password == password)
                {
                    userPass = Tuple.Create("T", findTrainer.First().TrainerId);
                    
                }
            }
            return userPass;
        }

        public Tuple<string, int> CheckIfAdmin(string username, string password)
        {
            Tuple<string, int> userPass = Tuple.Create("E", -1);
            var findAdmin = _context.Admins.Where(t => t.Username.ToLower().Equals(username.ToLower()));
            if (findAdmin.ToList().Count != 0)
            {
                if (findAdmin.First().Password == password)
                {
                    userPass = Tuple.Create("A", findAdmin.First().AdminId);

                }
            }
            return userPass;
        }

        public string RetrieveName(Tuple<string, int> value)
        {
            string name;
            switch (value.Item1)
            {
                case "A":
                    name = _context.Admins.Find(value.Item2).FirstName;
                    break;
                case "T":
                    name = _context.Trainers.Find(value.Item2).FirstName;
                    break;
                case "C":
                    name = _context.Customers.Find(value.Item2).FirstName;
                    break;
                default:
                    name = " ERROR ";
                    break;
            }
            return name;
        }

        public string RetrieveCategoryTitleFromId(int id)
        {
            var catName = _context.Categories.Where(c => c.CategoryId == id).First().CategoryName;
            return catName;
        }
    }
}
