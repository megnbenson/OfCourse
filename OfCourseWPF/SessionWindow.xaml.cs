using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OfCourseBusiness;

namespace OfCourseWPF
{
    /// <summary>
    /// Interaction logic for SessionWindow.xaml
    /// </summary>
    public partial class SessionWindow : Window
    {
        private CourseManager _courseManager = new CourseManager();
        public event EventHandler Book;
        private readonly MainWindow _mainWindow;
        private int _courseId;
        private int _custId;


        public SessionWindow(MainWindow mainWindow, int courseId, int custId)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
            _courseId = courseId;
            _custId = custId;
            SessionCourseTitleLabel.Content = _courseManager.GetCourseTitle(courseId);
            //availableDateLabel.Content = _courseManager.GetCourseTitle(courseId);
            availableDateLabel.Content = _courseManager.GetDate(courseId);
            availableTimeLabel.Content = _courseManager.GetTime(courseId);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            _courseManager.BookCourse(_courseId, _custId);



            Book(this, e);


        }
    }
}
