# OfCourse

# Project goal
To create a functioning system that connects students with trainers for face to face workshops.

# Definition of Done
Having a working application that allows students to select and book workshop courses, where the code is backed up through version control and all the documentation is complete with a user guide

# Sprint One
### Sprint Goal
Goal of sprint one is to create the class diagram to create a model-first approach to making the database. Once set up, To add data to the database, create a business CRUD layer and finally start the WPF.

### Output of Sprint Review
![sprint1 output](https://github.com/megnbenson/OfCourse/blob/main/PostSprintOne.png)

### Completed BackLog Items:
- [x] Created WPF layer with some basic functionality (list courses, fill screen)
- [x] Explored WPF toolbox
- [x] Created database (with five tables and one joint table)
- [x] Added some data to the database
### Actions for items not done
- [ ] Write Tests
- [ ] Figure out binding
- [ ] Add more CRUD functionality

## **Sprint one Retro**

### **What went well:**
I now know how databases work, model first. ReCreating a database many times from scratch but in doing so have got a working database, and has taken away the fear from it
Also explored the WPF toolbox, making use of checkboxes, stack panels and tabs

### **What could be improved**
Take breaks! Look carefully at the error messages and the migrations.
I could have drawn an ERD with an online tool, but instead I did it manually and then made a class diagram.
### **What is your action plan for next**
Action plan is to do tests, Add more CRUD operations and bind the category lists on the filters list.

# Sprint Two
![before sprint2](https://github.com/megnbenson/OfCourse/blob/main/PreSprint2.png)

### Sprint Goal
- Filter Courses by category, time and location
- Login Page
- Add a course
- Book a course
- View my bookings

### Output of Sprint Review
![output of sprint2](https://github.com/megnbenson/OfCourse/blob/main/PostSprintTwo.png)

### Completed BackLog Items:
- [x] Added Login Pop out Window
- [x] Added Login functionality that searches user/password and greets user by name
- [x] Added Create a course functionality
- [x] Added MyCourses tab that allows trainer to see created courses
- [x] Created a filter list, that binds categories to whichever categories are in the database
- [x] Created Edit functionality that allows trainer to edit the courses they've made
- [x] Create button in Create a Course Tab moves to General tab with the new course in the list
- [x] Edit button in MyCourses Tab makes the fields uneditable once finished with.
- [x] Created a my Bookings tab with a .BookCourse functionality (although not quite finished with it)

### Actions for items not done
- [ ] Write Tests
- [ ] Figured out binding, but couldn't get the filtered checkbox to connect and show only the selected courses of that category
- [ ] Did not attempt location
- [ ] COULD not work out how to Book the course, it was working in the database joint table, but not updating in the application

## **Sprint Two Retro**
### **What went well:**
By the end, I was using git branches and successfully reverted back to a previous git commit.

### **What could be improved**
I should have been commiting way more. I should have started the project with a priorities list of a minimum viable product and worked incrementally, adding tests with each part and neatening the edges as I went. The issue with my booking function was literally not including a 'using Microsoft.EntityFrameworkCore' which stripped 3 hours of my day. Taking frequent breaks and allowing frech eyes would have probably solved with quicker.

### **what is your action plan for next**
I commit more, to use branches for each part of a funcationality, and to strip back the project to its most crucial parts in time for the presentation. Take more frequent breaks!!

# Sprint Three
![before sprint3](https://github.com/megnbenson/OfCourse/blob/main/PreSprint3.png)

### Sprint Goal
- Finishing ordering so customer can see bookings
- TESTS
- Tidying up the app
- Making the colour scheme colour blind friendly


### Output of Sprint Review
![output of sprint3](https://github.com/megnbenson/OfCourse/blob/main/PostSprint3.png)

### Completed BackLog Items:
- [x] Changed Colour Scheme
- [x] Finished Ordering and Filling the fields of My Bookings Tab
- [x] Looked at Filter functionality - but have hidden it for the presentation
- [x] Looked at deleting a course - got it working in the db but not in application, have hidden for presentation
- [x] Tested most of the CRUD Business layer
- [x] Tidied code and filled in some data for the presentation

### Actions for items not done
- [ ] Write Tests
- [ ] Figured out binding, but couldn't get the filtered checkbox to connect and show only the selected courses of that category
- [ ] Did not attempt location
- [ ] COULD not work out how to Book the course, it was working in the database joint table, but not updating in the application

## **Sprint Three Retro**
### **What went well:**
Todaty was much better, More breaks made me more focussed, most of the tests went very smoothly and the priority of the application (for customers to order courses and sellers to create courses) has been fulfilled.

### **What could be improved**
Perhaps more tasks to be added, but with the documentation and testing the day was quite full.

### **What is your action plan for next**
Much more functionality to be added, but to test as I add! And to continue making branches for each additional function with frequent commits.

# Overall project retrospective 
### What have you learned?
  I've learnt:
 - Committing regularly should be second nature
 - That working out the priority of the MVP is most important and to start the sprint tasks with that (or an action plan to get to that)
 - Tasks are to be broken down into simple check offable tasks, Vague means time wasted as you're unclear what the task really is when you start
 - When doing a database, model first is good but needs tweaking with thought out structures of what you need each class to do
 - KISS. To work incrementally, and adding tests as you go, seems slow but makes for a more robust program at the end.
### What would you do differently next time?
I would follow my lessons learnt from above, take regular breaks so as to increase focus and maintain healthy stress levels. 
### What would you do next?
Next I would get the filters to work as well as the delete function, both I'm nearly there but not quite.

### Class diagrams included in my code project:
![Class Diagram](https://github.com/megnbenson/OfCourse/blob/main/ClassDiagram.png)
ClassDiagram1.cd in OfCourseData solution.


# User guide 
- Look at the class diagram to get a feel for how the classes interact.
- There is a CourseCustomer Table that is a joint table between courses and customers, you access this through the Courses.BookedCustomers List and Customers.BookedCourses list (many to many relationship). Use an SQL Query to have  a look at the databases
- Use the Program.cs in the OfCourseBusiness solution to manually add categories, customers, trainers, courses etc. In the application, you can only create and add courses.
- To edit the database, use the 'Add-Migration' and 'Update-Database' methods, make sure you install the EntityFrameworkCore, EntityFrameworkCore.SqlServer and EntityFrameworkCore.Tools Nuget Packages 
- Look at the OfCourseContext for further database insight.

