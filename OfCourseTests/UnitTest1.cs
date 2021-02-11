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

                var selectedCourse =
                from c in db.Courses
                where c.Title.Equals("testtest")
                select c;

                db.Courses.RemoveRange(selectedCourse);
                db.SaveChanges();

                var selectedCustomer =
                from c in db.Customers
                where c.Username.Equals("testJohn")
                select c;

                db.Customers.RemoveRange(selectedCustomer);
                db.SaveChanges();
            }
        }
        // for when you have a create category
        //[Test]
        //public void WhenANewCategoryIsAdded_TheNumberOfCategoriesIncreasesBy1()
        //{
        //    using (var db = new OfCourseContext())
        //    {
        //        var numberOfCustomersBefore = db.Categories.Count();
        //        _courseManager.CreateCategory();
        //        var numberOfCustomersAfter = db.Categories.Count();

        //        Assert.AreEqual(numberOfCustomersBefore + 1, numberOfCustomersAfter);
        //    }
        //}


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

                _courseManager.Create(1, "Knitting", "testtest", "Hey sweet purls, come join us for this knit sesh", "Birmingham", "W2", 20.0, 33, 1, date, "Morning", 10);

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
                var selectedCustomer = db.Customers.Where(c => c.Username.Equals("john")).First();

                var numOfBookedCoursesBefore = selectedCustomer.BookedCourses.Count();

                _courseManager.BookCourse(courseId, selectedCustomer.CustomerId);


                //Have to join the booked courses list otherwise you can't find it
                List<Course> BookedCourses = db.Customers.Include(bc => bc.BookedCourses).Where(c => c.CustomerId == selectedCustomer.CustomerId).FirstOrDefault().BookedCourses.ToList();

                var updatedBookedCoursesNum = BookedCourses.Count();
                Assert.AreEqual(numOfBookedCoursesBefore+1, updatedBookedCoursesNum);
            }
        }




    }
}