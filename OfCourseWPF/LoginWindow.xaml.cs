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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private CourseManager _courseManager = new CourseManager();
        public event EventHandler Login;
        private readonly MainWindow _mainWindow;
        
        public LoginWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //gives back user type and Id
            Tuple<string,int> userTypeId = _courseManager.Login(userNameBox.Text,passwordBox.Password);
            
            _mainWindow.SetSelectedUserTextboxes(userTypeId);
            
            Login(this, e);

            
        }
    }
}
