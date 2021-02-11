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

        public Course SelectedCourse { get; set; }
        public Customer SelectedCustomer { get; set; }
        public Trainer SelectedTrainer { get; set; }
        public Admin SelectedAdmin { get; set; }


        
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

        public List<Course> RetrieveCategories(IList checkedCategories)
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
            //throw new NotImplementedException();
        }

        public Tuple<string,int> Login(string username, string password)
        {
            
            using(var db = new OfCourseContext())
            {
                var findCustomer = db.Customers.Where(c => c.Username.Equals(username));
                if(findCustomer.ToList().Count == 0)
                {
                    var findTrainer = db.Trainers.Where(t => t.Username.Equals(username));
                    if(findTrainer.ToList().Count == 0)
                    {
                        Debug.WriteLine("NO Trainer or Customer WITH THAT USERNAME");
                        var findAdmin = db.Admins.Where(t => t.Username.Equals(username));
                        if (findAdmin.ToList().Count == 0)
                        {
                            Debug.WriteLine("NO USER WITH THAT USERNAME");
                        }
                        else
                        {
                            if(findAdmin.First().Password == password)
                            {
                                SetSelectedAdmin(findAdmin.First());
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
                            SetSelectedTrainer(findTrainer.First());
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
                        SetSelectedCustomer(findCustomer.First());
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
                var categoryQuery = db.Categories;
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

        

        public void Delete()
        {

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

        public void SetSelectedTrainer(object selected)
        {
            SelectedTrainer = (Trainer)selected;
        }

        public void SetSelectedCustomer(object selectedCustomer)
        {
            SelectedCustomer = (Customer)selectedCustomer;
        }

        public void SetSelectedAdmin(object selected)
        {
            SelectedAdmin = (Admin)selected;
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

        public void DeleteSelectedBookedCourse(object selectedCourse, int custId)
        {
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
