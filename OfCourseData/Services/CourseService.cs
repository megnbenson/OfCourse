using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfCourseData.Services
{
    public class CourseService : ICourseService
    {
        public Course GetCourseById(int courseId)
        {
            using (var db = new OfCourseContext())
            {
                return db.Courses.Where(c => c.CourseId == courseId).FirstOrDefault();
                
            }
        }

        public List<Course> GetCourseList()
        {
            using (var db = new OfCourseContext())
            {
                return db.Courses.ToList();
            }
        }

        public void SaveCourseChanges()
        {
            using (var db = new OfCourseContext())
            {
                
                db.SaveChanges();
            }
        }
    }
}
