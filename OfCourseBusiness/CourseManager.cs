using System;
using System.Collections.Generic;
using System.Linq;
using OfCourseData;
using OfCourseData.Services;
using Microsoft.EntityFrameworkCore;

namespace OfCourseBusiness
{
    /// <summary>
    /// TO REFACTOR:
    ///  - Have a login class
    ///  - Have a TrainerManager Class
    ///  - Have a CustomerManager class
    ///  - Make a booking tab class - to refer to courseManager make an instance of courseManager.Create e.g.
    ///  
    /// - FOR LOGIN
    /// - Have a find cust / trainer / admin methods, to simplify nested loops
    /// 
    /// </summary>
    public class CourseManager
    {
        private ICourseService _service;

        // This is dependency injection, rather than making a new
        // course manager we inject it in
        // new is glue
        // the customerService will go do things for us
        // breaking the dependency between db and course manager
        public CourseManager(ICourseService service)
        {
            _service = service;
        }

        public CourseManager()
        {
            _service = new CourseService();
        }

        //used to change it in the db
        public Course SelectedCourse { get; set; }

        public void Create(int trainerId, string categorytitle, string title, string description, string city, string postcode, double pricePerSession, int sessionLengthMinutes, int totalSessions, DateTime availableDate, string availableTime, int maxPeople)
        {
             
                 int catId = _service.GetCategoryIdFromTitle(categorytitle);
                var newCourse = new Course() { AvailableTime = availableTime, AvailableDate = availableDate, TrainerId = trainerId, CategoryId = catId, Title = title, Description = description, City = city, PostCode = postcode, PricePerSession = pricePerSession, SessionLengthMinutes = sessionLengthMinutes, TotalSessions = totalSessions };
                 _service.Add(newCourse);
                _service.SaveCourseChanges();
 
        }

        // In General tab, Tuple Returns string of user type "C", "t", "a" or "E" for error
        public Tuple<string, int> Login(string username, string password)
        {
            Tuple<string, int> typeAndId;
            // if this is not "E" then 
            typeAndId = _service.CheckIfCustomer(username, password);

            while(typeAndId.Item2 < 0){
                typeAndId = _service.CheckIfTrainer(username, password);
                typeAndId = _service.CheckIfAdmin(username, password);
                break;
            }
            return typeAndId;

        }

        // For general, to say Hello Name of User
        public string GetName(Tuple<string, int> value)
        {
            string name;

            name = _service.RetrieveName(value);
            return name;
        }


        // My Courses Tab

        //ICourseService being used
        public void Update(int courseId, string title, string description, string city, string postcode, double pricePerSession, int maxPeople, int minutes, int totalSessions)
        {

                SelectedCourse = _service.GetCourseById(courseId);
                SelectedCourse.Title = title;
                SelectedCourse.Description = description;
                SelectedCourse.City = city;
                SelectedCourse.PostCode = postcode;
                SelectedCourse.PricePerSession = pricePerSession;
                SelectedCourse.MaxPeople = maxPeople;
                SelectedCourse.SessionLengthMinutes = minutes;
                SelectedCourse.TotalSessions = totalSessions;
            // write changes to database
            _service.SaveCourseChanges();
            
        }

        public List<Course> RetrieveAll()
        {
            // implemented in ICourseService
            return _service.GetCourseList();

        }

        //My CourseTab:
        public void SetSelectedCourse(object selectedItem)
        {
            SelectedCourse = (Course)selectedItem;
        }

        public string CategoryTitleFromId(int id)
        {
            using (var db = new OfCourseContext())
            {
                var catName = db.Categories.Where(c => c.CategoryId == id).First().CategoryName;
                return catName;
            }
        }

        public List<Course> RetrieveTrainerCourses(int trainerId)
        {
            using (var db = new OfCourseContext())
            {
                var justTrainerCourses = db.Courses.Where(c => c.TrainerId == trainerId).ToList();
                return justTrainerCourses;
            }
        }

        /////////FOR BOOKING TAB
        ///

        public string GetCourseTitle(int id)
        {


            using (var db = new OfCourseContext())
            {

                return db.Courses.Find(id).Title;
            }

        }
        public string GetDate(int id)
        {


            using (var db = new OfCourseContext())
            {

                return String.Format("{0:ddd, MMM d, yyyy}", db.Courses.Find(id).AvailableDate);
            }

        }
        public string GetTime(int id)
        {


            using (var db = new OfCourseContext())
            {

                return db.Courses.Find(id).AvailableTime;
            }

        }

        public void BookCourse(int courseId, int custId)
        {
            using (var db = new OfCourseContext())
            {
                Course selectedCourse = db.Courses.Find(courseId);
                Customer selectedCustomer = db.Customers.Find(custId);


                db.Courses.Find(courseId).BookedCustomers.Add(selectedCustomer);

                db.Customers.Find(custId).BookedCourses.Add(selectedCourse);

                db.SaveChanges();

            }

        }


        public List<Course> RetrieveThisCustomersBookings(int custId)
        {
            using (var db = new OfCourseContext())
            {
                //IF error here, customer doesn't exist

                Customer selectedCustomer = db.Customers.Find(custId);
                List<Course> BookedCourses = db.Customers.Include(bc => bc.BookedCourses).Where(c => c.CustomerId == custId).FirstOrDefault().BookedCourses.ToList();

                return BookedCourses;
            }
        }





        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///FOR TESTS
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int IsCatTableEmptyReturnIdOfFirst()
        {
            using (var db = new OfCourseContext())
            {
                if (db.Categories.Any())
                {
                    return db.Categories.First().CategoryId;
                }
                else
                {
                    return -1;
                }
            }
        }

        public int IsCourseTableEmptyReturnIdOfFirst()
        {
            using (var db = new OfCourseContext())
            {
                if (db.Courses.Any())
                {
                    return db.Courses.First().CategoryId;
                }
                else
                {
                    return -1;
                }
            }
        }

        public void CreateCustomer(string firstName, string lastName, string username, string password, string city, string postCode)
        {
            var newCustomer = new Customer() { FirstName = firstName, LastName = lastName, Username = username, Password = password, City = city, PostCode = postCode };

            using (var db = new OfCourseContext())
            {
                db.Customers.Add(newCustomer);
                db.SaveChanges();
            }
        }

        //This works, as it is being deleted from the database, but it isn't being deleted in the list?
        public void DeleteSelectedBookedCourse(object course, int custId)
        {
            using (var db = new OfCourseContext())
            {
                Course selectedCourse = (Course)course;
                var courseId = selectedCourse.CourseId;

                //var selectedCustomer = db.Customers.Where(c => c.CustomerId == custId);


                List<Course> BookedCourseList = db.Customers.Include(bc => bc.BookedCourses).Where(c => c.CustomerId == custId).FirstOrDefault().BookedCourses.ToList();
                var courseOnList = BookedCourseList.Find(c => c.CourseId == courseId);


                // call remove range just in case there are multiple, useful to use if you don't know if its in the table or not.
                if (BookedCourseList.Contains(courseOnList))
                {
                    var oldList = db.Customers.Include(bc => bc.BookedCourses).Where(c => c.CustomerId == custId).FirstOrDefault().BookedCourses.Count();

                    //This returns true, so it does get removed
                    db.Customers.Include(bc => bc.BookedCourses).Where(c => c.CustomerId == custId).FirstOrDefault().BookedCourses.Remove(courseOnList);

                    var newBookedCourses = db.Customers.Include(bc => bc.BookedCourses).Where(c => c.CustomerId == custId).FirstOrDefault().BookedCourses.Count();

                    db.SaveChanges();
                }
            }
        }


        //FOR extension:
        //FOR the FILTER, trying to access only checked categories and then fill up that list with the selected cats
        public List<Course> RetrieveCategories()
        {
            using (var db = new OfCourseContext())
            {
                List<Course> coursesOfSelectedCats = new List<Course>();
                var selecteds = db.Categories.Where(c => c.isSelected == true);
                foreach (var item in selecteds)
                {
                    var catId = db.Categories.Where(c => c.CategoryName.Equals(item)).First().CategoryId;
                    coursesOfSelectedCats.Add(db.Courses.Find(catId));
                }

                return coursesOfSelectedCats.ToList();
            }
        }
        public List<String> GetAllCategoryNames()
        {
            List<string> catList = new List<string>();
            using (var db = new OfCourseContext())
            {
                var categoryQuery = GetAllCategories();
                foreach (var cat in categoryQuery)
                {
                    catList.Add(cat.CategoryName);
                }
            }
            return catList;
        }

        public List<Category> GetAllCategories()
        {

            using (var db = new OfCourseContext())
            {
                return db.Categories.ToList();
            }
        }


    }
}
