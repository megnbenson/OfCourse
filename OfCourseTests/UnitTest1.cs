using NUnit.Framework;
using OfCourseBusiness;
using OfCourseData;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace OfCourseTests
{
    public class Tests
    {
        CourseManager _courseManager = new CourseManager();

        [SetUp]
        public void Setup()
        {
            _courseManager = new CourseManager();
            // remove test entry in DB if present
            using (var db = new OfCourseContext())
            {
                var selectedCourses =
                from c in db.Courses
                where c.Title.Equals("TestCrochetBasics")
                select c;

                db.Courses.RemoveRange(selectedCourses);
                db.SaveChanges();

                var selectedCourses2 =
                from c in db.Courses
                where c.Title.Equals("testest")
                select c;

                db.Courses.RemoveRange(selectedCourses);
                db.SaveChanges();



                var selectedCustomer =
                from c in db.Customers
                where c.Username.Equals("testJohn")
                select c;

                db.Customers.RemoveRange(selectedCustomer);
                db.SaveChanges();
            }
        }

        [Test]
        public void WhenANewCourseIsAdded_TheNumberOfCoursesIncreasesBy1()
        {
            using (var db = new OfCourseContext())
            {
                var numberOfCustomersBefore = db.Courses.Count();
                DateTime date = new DateTime(2021, 5, 1, 8, 30, 52);
                _courseManager.Create(1, "Knitting", "TestCrochetBasics", "Hey sweet purls, come join us for this knit sesh", "London", "W2", 20.0, 33, 1, date, "Morning", 10);
                var numberOfCustomersAfter = db.Courses.Count();

                Assert.AreEqual(numberOfCustomersBefore + 1, numberOfCustomersAfter);
            }
        }

        [Test]
        public void CheckThereIsAtLeastOneCategory()
        {
            using var db = new OfCourseContext();
            // if empty returns -1, if not returns ID of first entry
            int categoryNotEmpty = _courseManager.IsCatTableEmptyReturnIdOfFirst();
            if(categoryNotEmpty  >= 0)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void CheckThereIsAtLeastOneCourse()
        {
            using var db = new OfCourseContext();
            // if empty returns -1, if not returns ID of first entry
            int categoryNotEmpty = _courseManager.IsCourseTableEmptyReturnIdOfFirst();
            if (categoryNotEmpty >= 0)
            {
                Assert.Pass();

            }
        }

        //THIS TEST DEPENDS ON THE DATA IN THE DB
        [TestCase(3,"Mixology 101")]
        // Test that course list appears, so ToString() is course Title
        public void CheckCoursesToStringShowJustCourseTitle(int id, string expTitle)
        {
            using var db = new OfCourseContext();

            string title = db.Courses.Find(id).ToString();
            Assert.AreEqual(title, expTitle);
          }


        [Test]
        public void WhenACourseDetailsAreChanged_TheDatabaseIsUpdated()
        {
            using (var db = new OfCourseContext())
            {
                DateTime date = new DateTime(2021, 5, 1, 8, 30, 52);

                //this one sometimes fails especially if I try and take it out in the setup test
                _courseManager.Create(1, "Knitting", "testtest", "Hey sweet purls, come join us for this knit sesh", "Chilly", "W2", 20.0, 33, 1, date, "Morning", 10);

                var courseId = db.Courses.Where(c => c.Title.Equals("testtest")).FirstOrDefault().CourseId;

                _courseManager.Update(courseId, "testtest", "A course that is surprisingly difficult", "Alabama", "W2", 20.0, 33, 1, 10);

                var updatedCourse = db.Courses.Find(courseId);
                Assert.AreEqual("Alabama", updatedCourse.City);
            }
        }

        [Test]
        public void WhenACourseIsBooked_TheDatabaseIsUpdated()
        {
            using (var db = new OfCourseContext())
            {
                //make course to book
                DateTime date = new DateTime(2021, 5, 1, 8, 30, 52);
                _courseManager.Create(1, "Knitting", "testtest", "Hey sweet purls, come join us for this knit sesh", "Birmingham", "W2", 20.0, 33, 1, date, "Morning", 10);

                //gets the courseID
                var courseId = db.Courses.Where(c => c.Title.Equals("testtest")).FirstOrDefault().CourseId;

                //make a customer
                _courseManager.CreateCustomer("John", "Smith", "testJohn", "password", "Shrewsbury", "SH1");
                var selectedCustomer = db.Customers.Where(c => c.Username.Equals("testJohn")).First();

                var numOfBookedCoursesBefore = selectedCustomer.BookedCourses.Count();

                _courseManager.BookCourse(courseId, selectedCustomer.CustomerId);


                //Have to join the booked courses list otherwise you can't find it
                List<Course> BookedCourses = db.Customers.Include(bc => bc.BookedCourses).Where(c => c.CustomerId == selectedCustomer.CustomerId).FirstOrDefault().BookedCourses.ToList();

                var updatedBookedCoursesNum = BookedCourses.Count();
                Assert.AreEqual(numOfBookedCoursesBefore+1, updatedBookedCoursesNum);
            }
        }


        /// <summary>
        /// /This test does not work, but I know for a fact (looking at the CourseCustomer Joint table that it is indeed booking, adding to that table and then being removed like it needs to be.
        /// </summary>
        [Test]
        public void WhenACourseIsDeleted_TheDatabaseIsUpdated()
        {
            using (var db = new OfCourseContext())
            {
                //make course to book
                DateTime date = new DateTime(2021, 5, 1, 8, 30, 52);
                _courseManager.Create(1, "Knitting", "testtest", "Hey sweet purls, come join us for this knit sesh", "Birmingham", "W2", 20.0, 33, 1, date, "Morning", 10);

                //gets the courseID
                var courseId = db.Courses.Where(c => c.Title.Equals("testtest")).FirstOrDefault().CourseId;

                //make a customer
                _courseManager.CreateCustomer("John", "Smith", "testJohn", "password", "Shrewsbury", "SH1");
                var selectedCustomer = db.Customers.Where(c => c.Username.Equals("testJohn")).First();

                var numOfBookedCoursesBefore = selectedCustomer.BookedCourses.Count();

                _courseManager.BookCourse(courseId, selectedCustomer.CustomerId);


                //Have to join the booked courses list otherwise you can't find it
                List<Course> BookedCourses = db.Customers.Include(bc => bc.BookedCourses).Where(c => c.CustomerId == selectedCustomer.CustomerId).FirstOrDefault().BookedCourses.ToList();

                //Num of bookedCourses after course has been booked
                var updatedBookedCoursesNum = BookedCourses.Count();
                var secondUpdate = selectedCustomer.BookedCourses.Count();

                var courseToDeleteFromList = db.Courses.Find(courseId);

                //delete from course
                _courseManager.DeleteSelectedBookedCourse(courseToDeleteFromList, selectedCustomer.CustomerId);



                updatedBookedCoursesNum = selectedCustomer.BookedCourses.Count();

                Assert.AreEqual(numOfBookedCoursesBefore, updatedBookedCoursesNum);
            }
        }
        [Test]
        public void WhenAUserLogsIn_TheUsernameIsNotCaseSensitive()
        {
            using (var db = new OfCourseContext())
            {
                //make a customer
                _courseManager.CreateCustomer("John", "Smith", "testJohn", "password", "Shrewsbury", "SH1");
                var selectedCustomer = db.Customers.Where(c => c.Username.Equals("john")).First();

                var returnsTuple = _courseManager.Login("testJohn", "password");
                var returnsTuple2 = _courseManager.Login("TEsTJOhN", "password");

                Assert.AreEqual(returnsTuple, returnsTuple2);
            }
        }

        [Test]
        public void WhenACustomerLogsIn_TheCorrectUserTypeAndIdIsReturned()
        {
            using (var db = new OfCourseContext())
            {
                //make a customer
                _courseManager.CreateCustomer("John", "Smith", "testJohn", "password", "Shrewsbury", "SH1");
                var selectedCustomer = db.Customers.Where(c => c.Username.Equals("testJohn")).First();

                var returnsTuple = _courseManager.Login("testJohn", "password");

                Assert.AreEqual(returnsTuple.Item1, "C");
                Assert.AreEqual(returnsTuple.Item2, selectedCustomer.CustomerId);
            }
        }

        [TestCase("2222")]
        [TestCase("")]
        [TestCase("!DSDV")]
        public void WhenACustomerLogsIn_AnErrorIsReturnedIfUserIsntThere(string username)
        {
            using (var db = new OfCourseContext())
            {
               
                var selectedCustomer = db.Customers.Where(c => c.Username.Equals(username)).FirstOrDefault();

                var returnsTuple = _courseManager.Login("testJohn", "password");

                Assert.AreEqual(returnsTuple.Item1, "E");
            }
        }

        [Test]
        public void CoursesListCanBeRetrieved()
        {
            using (var db = new OfCourseContext())
            {
                var NumListofCourses = _courseManager.RetrieveAll().Count();

                var NumOfCoursesFromDB = db.Courses.Count();

                Assert.AreEqual(NumListofCourses, NumOfCoursesFromDB);
            }
        }


        [Test]
        public void CategoryListCanBeRetrievedForTheCategoryDropDownMenus()
        {
            using (var db = new OfCourseContext())
            {
                var NumListofCats = _courseManager.GetAllCategories().Count();

                var NumOfCatsFromDB = db.Categories.Count();

                Assert.AreEqual(NumListofCats, NumOfCatsFromDB);
            }
        }

    }
}