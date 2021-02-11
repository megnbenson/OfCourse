using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using OfCourseData;
using System.Windows;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace OfCourseBusiness
{
    public class CourseManager
    {

        //used to change it in the db
        public Course SelectedCourse { get; set; }
        


        
        public void Create(int trainerId, string categorytitle, string title, string description, string city, string postcode, double pricePerSession, int sessionLengthMinutes, int totalSessions, DateTime availableDate, string availableTime, int maxPeople)
        {
            using (var db = new OfCourseContext())
            {
                var catId = db.Categories.Where(c => c.CategoryName.Equals(categorytitle)).First().CategoryId;
                var newCourse = new Course() { AvailableTime = availableTime, AvailableDate=availableDate, TrainerId = trainerId, CategoryId = catId, Title = title, Description = description, City = city, PostCode = postcode, PricePerSession = pricePerSession, SessionLengthMinutes = sessionLengthMinutes, TotalSessions = totalSessions };

                db.Courses.Add(newCourse);
                db.SaveChanges();
            }
        }

       
        public void CreateCustomer(string firstName, string lastName, string username, string password, string city, string postCode)
        {
            using (var db = new OfCourseContext())
            {
               
                var newCustomer = new Customer(){ FirstName = firstName, LastName = lastName, Username = username, Password = password, City = city, PostCode = postCode };

                db.Customers.Add(newCustomer);
                db.SaveChanges();
            }
        }

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

        public Tuple<string,int> Login(string username, string password)
        {
            
            using(var db = new OfCourseContext())
            {
                var findCustomer = db.Customers.Where(c => c.Username.ToLower().Equals(username.ToLower()));
                if(findCustomer.ToList().Count == 0)
                {
                    var findTrainer = db.Trainers.Where(t => t.Username.ToLower().Equals(username.ToLower()));
                    if(findTrainer.ToList().Count == 0)
                    {
                        Debug.WriteLine("NO Trainer or Customer WITH THAT USERNAME");
                        var findAdmin = db.Admins.Where(t => t.Username.ToLower().Equals(username.ToLower()));
                        if (findAdmin.ToList().Count == 0)
                        {
                            Debug.WriteLine("NO USER WITH THAT USERNAME");
                        }
                        else
                        {
                            if(findAdmin.First().Password == password)
                            {
                                var userPass = Tuple.Create("A", findAdmin.First().AdminId);
                                return userPass;
                                
                            }
                            else
                            {
                                Debug.WriteLine("THROW ERROR WRONG PASSWORD, USERNAME FOUND, ADMIN");
                            }
                            
                        }
                    }
                    else
                    {
                        if (findTrainer.First().Password == password)
                        {
                            var userPass = Tuple.Create("T", findTrainer.First().TrainerId);
                            return userPass;
                        }
                        else
                        {
                            Debug.WriteLine("THROW ERROR WRONG PASSWORD, USERNAME FOUND, Trainer");
                        }
                        
                    }

                }
                else
                {
                    if (findCustomer.First().Password == password)
                    {
                       
                        var userPass = Tuple.Create("C", findCustomer.First().CustomerId);
                        return userPass;
                    }
                    else
                    {
                        Console.WriteLine("THROW ERROR WRONG PASSWORD, USERNAME FOUND, Customer");
                    }
                    
                }
                var userPast = Tuple.Create("E", -1);
                return userPast;
            }
            
        }

        

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

        public string GetName(Tuple<string, int> value)
        {
            string name;
            
            using (var db = new OfCourseContext())
            {

                switch (value.Item1)
                {
                    case "A":
                        name = db.Admins.Find(value.Item2).FirstName;
                        break;
                    case "T":
                        name = db.Trainers.Find(value.Item2).FirstName;
                        break;
                    case "C":
                        name = db.Customers.Find(value.Item2).FirstName;
                        break;
                    default:
                        name = " ERROR ";
                        break;
                }
            }
            return name;
        }
        public HashSet<String> GetAllCourseTimes()
        {

            using (var db = new OfCourseContext())
            {
                var availableTimes = new HashSet<string>();
                foreach (var item in db.Courses)
                {
                    availableTimes.Add(item.AvailableTime);
                }
                return availableTimes;
            }
        }

        public List<Category> GetAllCategories()
        {
            
            using(var db = new OfCourseContext())
            {
                return db.Categories.ToList();
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

        // if you're updating in WPF, you want to 
        public void Update(int courseId, string title, string description, string city, string postcode, double pricePerSession, int maxPeople, int minutes, int totalSessions)
        {
            using (var db = new OfCourseContext())
            {
                SelectedCourse = db.Courses.Where(c => c.CourseId == courseId).FirstOrDefault();
                SelectedCourse.Title = title;
                SelectedCourse.Description = description;
                SelectedCourse.City = city;
                SelectedCourse.PostCode = postcode;
                SelectedCourse.PricePerSession = pricePerSession;
                SelectedCourse.MaxPeople = maxPeople;
                SelectedCourse.SessionLengthMinutes = minutes;
                SelectedCourse.TotalSessions = totalSessions;
                // write changes to database
                db.SaveChanges();
            }
        }

    

        public List<Course> RetrieveAll()
        {
            using (var db = new OfCourseContext())
            {
                return db.Courses.ToList();
            }
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

               // selectedCourse.BookedCustomers.Add(selectedCustomer);
                db.Courses.Find(courseId).BookedCustomers.Add(selectedCustomer);

                db.Customers.Find(custId).BookedCourses.Add(selectedCourse);
                //selectedCustomer.BookedCourses.Add(selectedCourse);

                

                db.SaveChanges();
                Debug.WriteLine(selectedCourse.BookedCustomers.First());
                Debug.WriteLine(selectedCustomer.BookedCourses.First().Title);
                Debug.WriteLine(selectedCustomer.BookedCourses);
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

        public void DeleteSelectedBookedCourse(object course, int custId)
        {
            using(var db = new OfCourseContext())
            {
                Course selectedCourse = (Course)course;
                var courseId = selectedCourse.CourseId;

                //var selectedCustomer = db.Customers.Where(c => c.CustomerId == custId);


                List<Course> BookedCourseList = db.Customers.Include(bc => bc.BookedCourses).Where(c => c.CustomerId == custId).FirstOrDefault().BookedCourses.ToList();
                var courseOnList = BookedCourseList.Find(c => c.CourseId == courseId); // NEED


                // call remove range just in case there are multiple, useful to use if you don't know if its in the table or not.
                if (BookedCourseList.Contains(courseOnList) )
                {
                    //found course in customers bookedCoursesList
                    
                    db.Customers.Include(bc => bc.BookedCourses).Where(c => c.CustomerId == custId).FirstOrDefault().BookedCourses.ToList().Remove(courseOnList);

                    db.SaveChanges();
                }
            }




            //using (var db = new OfCourseContext())
            //{
            //    Course selected = (Course)selectedCourse; //NEED
            //    Customer cust = db.Customers.Find(custId);
            //    var courseId = selected.CourseId; //NEED



            //    List<Course> BookedCourseList = db.Customers.Include(bc => bc.BookedCourses).Where(c => c.CustomerId == custId).FirstOrDefault().BookedCourses.ToList(); //NEED

            //    var courseOnList = BookedCourseList.Find(c => c.CourseId == courseId); // NEED

            //    List<Customer> BookedCustomerList = db.Courses.Include(bc => bc.BookedCustomers).Where(c => c.CourseId == courseId).FirstOrDefault().BookedCustomers.ToList();

            //    var customerOnList = BookedCustomerList.Find(c => c.CustomerId == custId);

            //    if (BookedCourseList.Contains(courseOnList) && BookedCustomerList.Contains(customerOnList))
            //    {
            //        //found course in customers bookedCoursesList
            //        db.Customers.Include(bc => bc.BookedCourses).Where(c => c.CustomerId == custId).FirstOrDefault().BookedCourses.ToList().Remove(courseOnList);

            //        db.Courses.Include(bc => bc.BookedCustomers).Where(c => c.CourseId == courseId).FirstOrDefault().BookedCustomers.ToList().Remove(customerOnList);
            //        db.SaveChanges();
            //    }

            //    db.SaveChanges();
            //}
        }
    }
}
