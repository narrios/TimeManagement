using System;
using System.Windows;
using TimeManagement.Classes;

namespace TimeManagement
{
    /// <summary>
    /// Логика взаимодействия для expandEventWindow.xaml
    /// </summary>
    public partial class expandEventWindow : Window
    {
        LocalEvents updateEvent;
        public expandEventWindow(LocalEvents e)
        {
            InitializeComponent();
            if (e != null)
            {
                updateEvent = e;
                name.Text = e.EventName;
                description.Text = e.EventDescription;
                if (e.EventStart != null)
                {
                    startDatePicker.SelectedDate = e.EventStart.Value.Date;
                    endDatePicker.SelectedDate = e.EventEnd.Value.Date;
                    startTimePicker.SelectedTime = e.EventStart.Value;
                    endTimePicker.SelectedTime = e.EventEnd.Value;
                }
                addButton.Content = "Adaugă detalii sarcină";
                deleteButton.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow main = Owner as mainWindow;
            if (name.Text != "" && description.Text != "" && startDatePicker.SelectedDate != null && startTimePicker.SelectedTime != null && endDatePicker.SelectedDate != null && endTimePicker.SelectedTime != null)
            {
                DateTime d1 = startDatePicker.SelectedDate.Value.Add(startTimePicker.SelectedTime.Value.TimeOfDay);
                DateTime d2 = endDatePicker.SelectedDate.Value.Add(endTimePicker.SelectedTime.Value.TimeOfDay);
                if (updateEvent == null)
                {
                    LocalEvents newEvent = new LocalEvents()
                    {
                        EventName = name.Text,
                        EventDescription = description.Text,
                        EventStart = d1,
                        EventEnd = d2,
                    };
                    main.levents.InsertOnSubmit(newEvent);
                    main.mf.DB.SubmitChanges();
                }
                else
                {
                    string time1 = d1.ToString().Substring(0, d1.ToString().Length - 3);
                    string time2 = d2.ToString().Substring(0, d2.ToString().Length - 3);
                    updateEvent.EventName = name.Text;
                    updateEvent.EventDescription = description.Text;
                    updateEvent.EventStart = d1;
                    updateEvent.EventEnd = d2;
                    main.mf.DB.SubmitChanges();
                }
                if (main.user != null) main.syncButton.IsEnabled = true;
                main.refreshData();
                Close();
            }
        }
        private void DeleteEventButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow main = Owner as mainWindow;
            main.levents.DeleteOnSubmit(updateEvent);
            main.mf.DB.SubmitChanges();
            if (main.user != null) main.syncButton.IsEnabled = true;
            main.refreshData();
            Close();
        }
    }
}
