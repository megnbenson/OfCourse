using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using OfCourseBusiness;
using System.Diagnostics;

namespace OfCourseWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CourseManager _courseManager = new CourseManager();
        public List<string> categoryNames = new List<string>();
        private LoginWindow loginWindow;
        private SessionWindow bookSessionWindow;
        private bool loggedIn = false;


        public MainWindow()
        {
            InitializeComponent();
            PopulateListBox();
            DataBindingCheckBoxes.DataContext = this;
        }

        //General fill list of courses

        private void PopulateListBox()
        {
            //Fill the category filters
            var categoryNames = _courseManager.GetAllCategoryNames();
            categoryDataBinding.ItemsSource = categoryNames;
            tCategory.ItemsSource = categoryNames;

            var bindingCheckBoxesList = _courseManager.GetAllCategories();

            ListBoxCourse.ItemsSource = _courseManager.RetrieveAll();
            TimePreferenceBind.ItemsSource = _courseManager.GetAllCourseTimes();
        }

         // FILTER list by checked category
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var selectedCategory = DataBindingCheckBoxes.Items;
            var selectedCategory2 = DataBindingCheckBoxes.SelectedItem;
            var selectedCategory3 = DataBindingCheckBoxes.SelectedItems;
            ListBoxCourse.ItemsSource = _courseManager.RetrieveCategories();
            //categoryDataBinding.SelectedItem
        }


        //General fill fields of selected course
        private void PopulateGeneralCourseFields()
        {
            if (_courseManager.SelectedCourse != null)
            {
                TextId.Text = _courseManager.SelectedCourse.CourseId.ToString();
                TextName.Text = _courseManager.SelectedCourse.Title;
                TextDescription.Text = _courseManager.SelectedCourse.Title;
                TextCity.Text = _courseManager.SelectedCourse.City;
                TextPostCode.Text = _courseManager.SelectedCourse.PostCode;
                TextPostCode.Text = _courseManager.SelectedCourse.PostCode;
                TextPrice.Text = _courseManager.SelectedCourse.PricePerSession.ToString();
                TextMaxPeople.Text = _courseManager.SelectedCourse.MaxPeople.ToString();
                TextMinutes.Text = _courseManager.SelectedCourse.SessionLengthMinutes.ToString();
                TextTotalSessions.Text = _courseManager.SelectedCourse.TotalSessions.ToString();

            }
        }

        //General course list selection changed
        private void ListBoxCourse_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ListBoxCourse.SelectedItem != null)
            {
                _courseManager.SetSelectedCourse(ListBoxCourse.SelectedItem);
                PopulateGeneralCourseFields();

                if (loggedIn)
                {
                    ButtonBook.Visibility = Visibility.Visible;

                }
            }
        }

        //BOOKING LIST 
        private void BookingsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (BookingsList.SelectedItem != null)
            {
                _courseManager.SetSelectedCourse(BookingsList.SelectedItem);
                PopulateBookingFields();
                
            }
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            _courseManager.Create(Int32.Parse(TextId.Text), tCategory.Text, TName.Text, TDescription.Text, TCity.Text, TPostCode.Text, Convert.ToDouble(TPrice.Text), Int32.Parse(TMinutes.Text), Int32.Parse(TTotalSessions.Text), AvailableDatesCalendar.DisplayDate, AvailableTimes.Text, Int32.Parse(TMaxPeople.Text))  ;
            ListBoxCourse.ItemsSource = null;
            // put back the screen data
            PopulateListBox();
            PopulateMyListBoxOnMyCoursesTab();
            ListBoxCourse.SelectedItem = _courseManager.SelectedCourse;
            PopulateGeneralCourseFields();
            PopulateMyCourseFields();
            //Puts tab to general after adding a new course
            MainTabs.SelectedIndex = 0;

            //Take list of selected dates, and Available time
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            _courseManager.Update(Int32.Parse(MyTextId.Text), MyTextName.Text, MyTextDescription.Text, MyTextCity.Text, MyTextPostCode.Text, Convert.ToDouble(MyTextPrice.Text), Int32.Parse(MyTextMaxPeople.Text), Int32.Parse(MyTextMinutes.Text), Int32.Parse(MyTextTotalSessions.Text));
            ListBoxCourse.ItemsSource = null;

            // put back the screen data
            PopulateListBox();
            ListBoxCourse.SelectedItem = _courseManager.SelectedCourse;
            PopulateGeneralCourseFields();

            MyTextName.IsEnabled = false;
            MyTextName.IsReadOnlyCaretVisible = true;

            MyTextCity.IsEnabled = false;
            MyTextCity.IsReadOnlyCaretVisible = true;

            MyTextDescription.IsEnabled = false;
            MyTextDescription.IsReadOnlyCaretVisible = true;

            MyTextMaxPeople.IsEnabled = false;
            MyTextMaxPeople.IsReadOnlyCaretVisible = true;

            MyTextMinutes.IsEnabled = false;
            MyTextMinutes.IsReadOnlyCaretVisible = true;

            MyTextPostCode.IsEnabled = false;
            MyTextPostCode.IsReadOnlyCaretVisible = true;

            MyTextPrice.IsEnabled = false;
            MyTextPrice.IsReadOnlyCaretVisible = true;

            MyTextTotalSessions.IsEnabled = false;
            MyTextTotalSessions.IsReadOnlyCaretVisible = true;

            ButtonEdit.Visibility = Visibility.Visible;
            ButtonUpdate.Visibility = Visibility.Hidden;
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            // make a login pop up
            // is in system, yes no, if yes out with ID?
            //https://stackoverflow.com/questions/58198741/how-to-change-textbox-value-when-i-change-it-from-another-window
            // ^ used for sending this into new window, to send selectedUser info to original class
            loginWindow = new LoginWindow(this);
            loginWindow.Show();
            loginWindow.Login += new EventHandler(UserLoggedIn);


        }

        private void UserLoggedIn(object sender, EventArgs e)
        {
            // is user customer or trainer
            //
            loggedIn = true;
            ButtonLogin.Background = Brushes.Yellow;
            loginWindow.Close();
            ButtonLogin.Visibility = Visibility.Hidden;
            PopulateMyListBoxOnMyCoursesTab();
            
            
        }

        public void SetSelectedUserTextboxes(Tuple<string, int> value)
        {

            MyTextHello.Text = TextHello.Text = "hello " + _courseManager.GetName(value);
            MyTextType.Text = TextType.Text = value.Item1;
            MyTextId.Text = TextId.Text = value.Item2.ToString();

            if (value.Item1 == "C")
            {
                AddCourseTab.Visibility = Visibility.Hidden;
                MyCoursesTab.Visibility = Visibility.Hidden;
                BookingsTab.Visibility = Visibility.Visible;

                PopulateBookingsList();
            }
            else
            {
                AddCourseTab.Visibility = Visibility.Visible;
                AddCourseTab.Background = Brushes.Red;
                AddCourseTab.Foreground = Brushes.Yellow;
                MyCoursesTab.Foreground = Brushes.Blue;
                MyCoursesTab.Visibility = Visibility.Visible;
                BookingsTab.Visibility = Visibility.Visible;
            }
        }



        //for MY COURSES TAB:

        //As a trainer, My courses Tab populates MyListBoxd
        private void PopulateMyListBoxOnMyCoursesTab()
        {

            MyCoursesListBox.ItemsSource = _courseManager.RetrieveTrainerCourses(Int32.Parse(MyTextId.Text));

        }

        // Fill the fields of As A Trainer, My Courses Tab
        private void PopulateMyCourseFields()
        {
            if (_courseManager.SelectedCourse != null)
            {
                MyTextId.Text = _courseManager.SelectedCourse.CourseId.ToString();
                MyTextName.Text = _courseManager.SelectedCourse.Title;
                MyTextDescription.Text = _courseManager.SelectedCourse.Title;
                MyTextCity.Text = _courseManager.SelectedCourse.City;
                MyTextPostCode.Text = _courseManager.SelectedCourse.PostCode;
                MyTextPostCode.Text = _courseManager.SelectedCourse.PostCode;
                MyTextPrice.Text = _courseManager.SelectedCourse.PricePerSession.ToString();
                MyTextMaxPeople.Text = _courseManager.SelectedCourse.MaxPeople.ToString();
                MyTextMinutes.Text = _courseManager.SelectedCourse.SessionLengthMinutes.ToString();
                MyTextTotalSessions.Text = _courseManager.SelectedCourse.TotalSessions.ToString();
                MyCategoryTextBox.Text = _courseManager.CategoryTitleFromId(_courseManager.SelectedCourse.CategoryId);

            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            ButtonEdit.Visibility = Visibility.Hidden;
            ButtonUpdate.Visibility = Visibility.Visible;
            //MyCategoryTextBox.Visibility = Visibility.Hidden;
            //MyCategory.Visibility = Visibility.Visible;

            MyTextName.IsEnabled = true;
            MyTextName.IsReadOnlyCaretVisible = false;

            MyTextCity.IsEnabled = true;
            MyTextCity.IsReadOnlyCaretVisible = false;

            MyTextDescription.IsEnabled = true;
            MyTextDescription.IsReadOnlyCaretVisible = false;

            MyTextMaxPeople.IsEnabled = true;
            MyTextMaxPeople.IsReadOnlyCaretVisible = false;

            MyTextMinutes.IsEnabled = true;
            MyTextMinutes.IsReadOnlyCaretVisible = false;

            MyTextPostCode.IsEnabled = true;
            MyTextPostCode.IsReadOnlyCaretVisible = false;

            MyTextPrice.IsEnabled = true;
            MyTextPrice.IsReadOnlyCaretVisible = false;

            MyTextTotalSessions.IsEnabled = true;
            MyTextTotalSessions.IsReadOnlyCaretVisible = false;


        }

        //As A trainer, in My courses tab, List box, when clicking different courses
        private void MyCoursesListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (MyCoursesListBox.SelectedItem != null)
            {
                _courseManager.SetSelectedCourse(MyCoursesListBox.SelectedItem);
                PopulateMyCourseFields();
            }
        }


        ////////BOOKING TAB
        ///

        private void PopulateBookingsList()
        {

            BookingsList.ItemsSource = _courseManager.RetrieveThisCustomersBookings(Int32.Parse(MyTextId.Text));

            //this is GeneralTransform filling:
            //ListBoxCourse.ItemsSource = _courseManager.RetrieveAll();

        }

        // Fill the fields of As A Trainer, My Courses Tab
        private void PopulateBookingFields()
        {
            if (_courseManager.SelectedCourse != null)
            {
                BookingTextId.Text = _courseManager.SelectedCourse.CourseId.ToString();
                BookingTextName.Text = _courseManager.SelectedCourse.Title;
                BookingTextDescription.Text = _courseManager.SelectedCourse.Title;
                BookingTextCity.Text = _courseManager.SelectedCourse.City;
                BookingTextPostCode.Text = _courseManager.SelectedCourse.PostCode;
                BookingTextPostCode.Text = _courseManager.SelectedCourse.PostCode;
                BookingTextPrice.Text = _courseManager.SelectedCourse.PricePerSession.ToString();
                BookingTextMaxPeople.Text = _courseManager.SelectedCourse.MaxPeople.ToString();
                BookingTextMinutes.Text = _courseManager.SelectedCourse.SessionLengthMinutes.ToString();
                BookingTextTotalSessions.Text = _courseManager.SelectedCourse.TotalSessions.ToString();
                BookingTextAvailableDate.Text = _courseManager.SelectedCourse.AvailableDate.ToString();
                BookingTextAvailableTime.Text = _courseManager.SelectedCourse.AvailableTime.ToString();
                //BookingCategoryTextBox.Text = _courseManager.CategoryTitleFromId(_courseManager.SelectedCourse.CategoryId);

            }
        }
        // In General, as customer, Book a course
        private void ButtonBook_Click(object sender, RoutedEventArgs e)
        {
            // Book the selected course
            _courseManager.SetSelectedCourse(ListBoxCourse.SelectedItem);
            // New window, so you can set time of session, date of session, course ID
            bookSessionWindow = new SessionWindow(this, _courseManager.SelectedCourse.CourseId, Int32.Parse(MyTextId.Text));
            bookSessionWindow.Show();
            bookSessionWindow.Book += new EventHandler(CourseBooked);
            PopulateBookingsList();

        }


        private void CourseBooked(object sender, EventArgs e)
        {
            // is user customer or trainer
            //
            //ButtonLogin.Background = Brushes.Yellow;
            PopulateBookingsList();
            bookSessionWindow.Close();
            //ButtonLogin.Visibility = Visibility.Hidden;
            //PopulateMyListBoxOnMyCoursesTab();
        }

        private void ButtonCancelBooking_Click(object sender, RoutedEventArgs e)
        {
            /// Cancel Not Working, Shelved for now
            //_courseManager.DeleteSelectedBookedCourse(BookingsList.SelectedItem, Int32.Parse(MyTextId.Text));
            //PopulateBookingsList();
        }

     
    }

}

    

