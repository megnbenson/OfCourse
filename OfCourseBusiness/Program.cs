using System;
using System.Collections.Generic;
using System.Linq;
using OfCourseData;

namespace OfCourseBusiness
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var db = new OfCourseContext())
            {

                Console.WriteLine(db.ContextId);
                Console.WriteLine("creating new customer");
                //READ

                //iterate over and output all customers
                // has to be in the using statement
                //foreach (var c in db.Customers)
                //{
                //    Console.WriteLine($"Customer {c.FirstName} had ID {c.CustomerId} and lives in {c.City}");
                //}

                //CREATE(add a new customer)
                //construct a new customer
                //var newCategory = new Category()
                //{
                //    CategoryName = "Engineering"
                    
                //};
                //var newCategory3 = new Category()
                //{
                //    CategoryName = "Mixology"

                //};
                //var newCategory1 = new Category()
                //{
                //    CategoryName = "Floristry"

                //};
                var newTrainer = new Trainer()
                {
                    PostCode = "W1",
                    City = "London",
                    Username = "megTrainer",
                    Password = "password",
                    FirstName = "Megan",
                    LastName = "Benson"
                };
                var newCustomer = new Customer()
                {
                    
                    PostCode = "TN13",
                    City = "Sevenoaks",
                    Username = "meg",
                    Password = "password",
                    FirstName = "Megan",
                    LastName = "Benson"
                };
                var newCourse = new Course()
                {
                    CategoryId = 2,
                    TrainerId = 2,
                    PostCode = "W1",
                    Title = "Floristry 101",
                    Description = "Learn basic Floristry  techniques in just two sessions!",
                    City = "London",
                    PricePerSession = 15.0,
                    MaxPeople = 5,
                    TotalSessions = 2,
                    SessionLengthMinutes = 60
                };

                //var newAdmin = new Admin()
                //{

                //    City = "London",
                //    Username = "MegAdmin",
                //    Password = "password",
                //    FirstName = "Megan",
                //    LastName = "Benson"
                //};



                ////};
                ////DELETE a customer
                //// this line DEFINES the query (LINQ steps)
                //var selectedCustomer = db.Customers.Where(c => c.CustomerId == "BLOB");
                //// call remove range just in case there are multiple, useful to use if you don't know if its in the table or not.
                //db.Customers.RemoveRange(selectedCustomer);
                //db.SaveChanges();

                //ADD the customer
                //made a new customer, adding it to the customers database adn then saving.
                //// using properties instead of fields
                db.Customers.Add(newCustomer);
                //db.Categories.Add(newCategory);
                //db.Categories.Add(newCategory1);
                //db.Categories.Add(newCategory3);
                //db.Trainers.Add(newTrainer);
                db.Courses.Add(newCourse);
              // db.Admins.Add(newAdmin);
                db.SaveChanges();

                //UPDATE a customer
                //get the first customer with the id of blob
                // this line EXCEUTES the query (LINQ steps)
                //var customer = db.Customers.Where(c => c.CustomerId == "BLOB").FirstOrDefault();
                ////change that one's city to Paris
                //customer.City = "Paris";
                //db.SaveChanges();


                ///// LINQ queries
                ////define the query
                //var query = db.Customers.Where(c => c.CustomerId == "BONAP");
                ////execute the query
                //// this below line can be called many times, so you could have many queries saved and ready to check for them
                //var selected = query.FirstOrDefault();

                ////To do both at the same time, defne and execute (if you don't wan tot keep your query
                //var selected2 = db.Customers.Where(c => c.CustomerId == "BONAP").FirstOrDefault();

                ////LINQ to SQL
                //// Find is shorthand for ID
                //var selected3 = db.Customers.Find("BLOG");

                ////Query syntax
                //var query1 =
                //    from c in db.Customers
                //    where c.City == "London"
                //    orderby c.ContactName
                //    select c;

                //foreach (var cust in query1)
                //{
                //    Console.WriteLine(cust.GetFullName());
                //}

                ////method syntax
                //IEnumerable<Customer> query2 = db.Customers
                //    .Where(c => c.City == "London")
                //    .OrderBy(c => c.ContactName);


            }
        }
    }
}
