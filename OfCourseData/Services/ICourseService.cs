using System;
using System.Collections.Generic;
using System.Text;

namespace OfCourseData.Services
{
    public interface ICourseService
    {
        List<Course> GetCourseList();
        Course GetCourseById(int courseId);
        void SaveCourseChanges();
    }
}
