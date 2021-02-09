using System;
using System.Collections.Generic;
using System.Windows;
using OfCourseBusiness;

namespace OfCourseWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CourseManager _courseManager = new CourseManager();
        public List<string> categoryNames = new List<string>();

            

            public MainWindow()
            {
                InitializeComponent();
                PopulateListBox();
            }
            private void PopulateListBox()
            {
                categoryNames = _courseManager.GetAllCategories();
                categoryDataBinding.ItemsSource = categoryNames;

                ListBoxCourse.ItemsSource = _courseManager.RetrieveAll();
            }

            private void PopulateCustomerFields()
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

            private void ListBoxCustomer_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
            {
                if (ListBoxCourse.SelectedItem != null)
                {
                    _courseManager.SetSelectedCustomer(ListBoxCourse.SelectedItem);
                    PopulateCustomerFields();
                }
            }

            private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
            {
                _courseManager.Update(Int32.Parse(TextId.Text), TextName.Text, TextDescription.Text, TextCity.Text, TextPostCode.Text, Convert.ToDouble(TextPrice.Text), Int32.Parse(TextMaxPeople.Text), Int32.Parse(TextMinutes.Text), Int32.Parse(TextTotalSessions.Text));
                ListBoxCourse.ItemsSource = null;
                // put back the screen data
                PopulateListBox();
                ListBoxCourse.SelectedItem = _courseManager.SelectedCourse;
                PopulateCustomerFields();
            }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            // make a login pop up
            // is in system, yes no, if yes out with ID?
            //_courseManager.Login()

        }

        //public class ListViewModel : INotifyPropertyChanged
        //{
        //    // Raise OnPropertyChanged on the setter for game items.. also create backing property
        //    public ObservableCollection<GameItem> GameItems { get; set; }
        //}
    }
    }

    

