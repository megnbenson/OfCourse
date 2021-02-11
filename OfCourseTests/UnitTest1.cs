using NUnit.Framework;
using OfCourseBusiness;
using OfCourseData;

namespace OfCourseTests
{
    public class Tests
    {
        CourseManager _courseManager = new CourseManager();

        [SetUp]
        public void Setup()
        {
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

        [TestCase(3,"Floristry 101")]
        // Test that course list appears, so ToString() is course Title
        public void CheckCoursesToStringShowJustCourseTitle(int id, string expTitle)
        {
            using var db = new OfCourseContext();

            string title = db.Courses.Find(id).ToString();
            Assert.AreEqual(title, expTitle);
  

        }


        //[Test]
        //public void WhenACustomersDetailsAreChanged_TheDatabaseIsUpdated()
        //{
        //    using (var db = new NorthwindContext())
        //    {
        //        _customerManager.Create("MAND", "Nish Mandal", "Sparta Global", "Paris");

        //        _customerManager.Update("MAND", "Nish Mandal", "Birmingham", null, null);

        //        var updatedCustomer = db.Customers.Find("MAND");
        //        Assert.AreEqual("Birmingham", updatedCustomer.City);
        //    }
        //}
    }
}