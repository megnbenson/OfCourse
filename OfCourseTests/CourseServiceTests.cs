using System;
using System.Collections.Generic;
using System.Text;
//added:
using Microsoft.EntityFrameworkCore;
using OfCourseData;
using OfCourseData.Services;
using NUnit.Framework;

namespace OfCourseTests
{
    // mocking info 
    //https://www.codeproject.com/articles/991028/using-moq-for-unit-testing
    class CourseServiceTests
    {
        //CourseService _customerService;
        //OfCourseContext _db;

        //[OneTimeSetUp]
        //public void OneTimeSetUp()
        //{
        //    var options = new DbContextOptionsBuilder<OfCourseContext>()
        //        .UseInMemoryDatabase(databaseName: "OfCourse_Fake")
        //        .Options;
        //    _db = new OfCourseContext(options);
        //    _customerService = new CourseService(_db);
        //}

        //[SetUp]
        //public void Setup()
        //{

        //    var selectedCustomers =
        //    from c in _db.Customers
        //    where c.CustomerId == "MAND"
        //    select c;

        //    _db.Customers.RemoveRange(selectedCustomers);
        //    _db.SaveChanges();
        //}

        //[Test]
        //public void WhenACustomerIsSearchedFor_TheCorrectObjectIsReturned()
        //{
        //    _customerService.Add(new Customer() { CustomerId = "MAND", ContactName = "Nish Mandal", CompanyName = "Sparta Global", City = "Paris" });
        //    var result = _customerService.GetCustomerById("MAND");

        //    Assert.That(result.ContactName, Is.EqualTo("Nish Mandal"));
        //}

        //[TearDown]
        //public void TearDown()
        //{

        //    var selectedCustomers =
        //    from c in _db.Customers
        //    where c.CustomerId == "MAND"
        //    select c;

        //    _db.Customers.RemoveRange(selectedCustomers);
        //    _db.SaveChanges();
        //}
    }
}


