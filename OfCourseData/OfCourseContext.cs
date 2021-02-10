using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace OfCourseData
{
    public partial class OfCourseContext : DbContext
    {
        //public OfCourseContext()
        //{

        //}
        //public OfCourseContext(DbContextOptions<OfCourseContext> options)
        //    : base(options)
        //{
        //}
        public static OfCourseContext Instance { get; } = new OfCourseContext();

        public DbSet<Course> Courses { get; set; }
        //public DbSet<CourseSessionDetails> CourseSessionDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = OfCourseDataBase;");

            }
        }

    }
}




