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
        int GetCategoryIdFromTitle(string categorytitle);
        void Add(Course c);
        Tuple<string, int> CheckIfCustomer(string username,string password);
        Tuple<string, int> CheckIfTrainer(string username, string password);
        Tuple<string, int> CheckIfAdmin(string username, string password);
        string RetrieveName(Tuple<string, int> value);
    }
}
