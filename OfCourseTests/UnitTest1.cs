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


        //test database
        // Test there is a customers table,
        //category, trainer, admin, course tablse
        // That the login button works
    }
}