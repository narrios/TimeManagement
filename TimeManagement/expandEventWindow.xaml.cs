using System;
using System.Windows;
using System.Linq;
using System.Data.Linq;
using TimeManagement.Classes;

namespace TimeManagement
{
    /// <summary>
    /// Логика взаимодействия для expandEventWindow.xaml
    /// </summary>
    public partial class expandEventWindow : Window
    {
        LocalEvents updateEvent;
        LocalEndedEvents localEndedEvent;
        DateTime? d1, d2;
        public expandEventWindow(LocalEvents e, LocalEndedEvents ee)
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
                addButton.Content = "Adaugă detalii";
                deleteButton.Visibility = Visibility.Visible;
                endButton.Visibility = Visibility.Visible;
            }
            else if(ee != null)
            {
                updateEvent = new LocalEvents()
                {
                    EventName = ee.EventName,
                    EventDescription = ee.EventDescription,
                    EventStart = ee.EventStart,
                    EventEnd = ee.EventEnd,
                };
                localEndedEvent = ee;
                name.Text = ee.EventName;
                description.Text = ee.EventDescription;
                if (ee.EventStart != null)
                {
                    startDatePicker.SelectedDate = ee.EventStart.Value.Date;
                    endDatePicker.SelectedDate = ee.EventEnd.Value.Date;
                    startTimePicker.SelectedTime = ee.EventStart.Value;
                    endTimePicker.SelectedTime = ee.EventEnd.Value;
                }
                addButton.Content = "Adaugă detalii";
                deleteButton.Visibility = Visibility.Visible;
            }
            else
            {
                startDatePicker.SelectedDate = DateTime.Now;
                endDatePicker.SelectedDate = DateTime.Now;
                startTimePicker.SelectedTime = DateTime.MinValue;
                endTimePicker.SelectedTime = DateTime.MinValue;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow main = Owner as mainWindow;
            if(endDatePicker.SelectedDate < DateTime.Now.Date)
            {
                messageBox message = new messageBox("Atenție", "Nu puteți crea o sarcină cu termenul deja finalizat", "Ok");
                message.ShowDialog();
            }
            else if(name.Text == "")
            {
                messageBox message = new messageBox("Atenție", "Nu puteți crea o sarcină fără denumire", "Ok");
                message.ShowDialog();
            }
            else if (description.Text == "")
            {
                messageBox message = new messageBox("Atenție", "Nu puteți crea o sarcină fără descriere", "Ok");
                message.ShowDialog();
            }
            else if (name.Text != "" && description.Text != "" )
            {
                if(startDatePicker.SelectedDate != null && startTimePicker.SelectedTime != null && endDatePicker.SelectedDate != null && endTimePicker.SelectedTime != null)
                {
                    d1 = startDatePicker.SelectedDate.Value.Add(startTimePicker.SelectedTime.Value.TimeOfDay);
                    d2 = endDatePicker.SelectedDate.Value.Add(endTimePicker.SelectedTime.Value.TimeOfDay);
                }
                
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
                    messageBox message = new messageBox("Atenție", "Sarcina " + name.Text + " a fost adăugată cu succes!", "Ok");
                    message.ShowDialog();
                }
                else
                {
                    updateEvent.EventName = name.Text;
                    updateEvent.EventDescription = description.Text;
                    updateEvent.EventStart = d1;
                    updateEvent.EventEnd = d2;
                    if(localEndedEvent != null)
                    {
                        main.leevents.DeleteOnSubmit(localEndedEvent);
                        main.levents.InsertOnSubmit(updateEvent);
                    }
                    messageBox message = new messageBox("Atenție", "Sarcina " + name.Text + " a fost actualizată cu succes!", "Ok");
                    message.ShowDialog();
                }
                main.mf.DB.SubmitChanges();

                if (main.user != null) main.syncButton.IsEnabled = true;
                main.refreshCalendarEvents();
                main.refreshDash();
                
                Close();
            }
        }
        private void DeleteEventButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow main = Owner as mainWindow;
            if(updateEvent != null && localEndedEvent == null)
            {
                main.levents.DeleteOnSubmit(updateEvent);
            }
            else
            {
                main.leevents.DeleteOnSubmit(localEndedEvent);
            }
            main.mf.DB.SubmitChanges();
            if (main.user != null) main.syncButton.IsEnabled = true;
            main.refreshDash();
            main.refreshCalendarEvents();
            messageBox message = new messageBox("Atenție", "Sarcina " + name.Text + " a fost ștearsă cu succes!", "Ok");
            message.ShowDialog();
            Close();
        }
        private void endButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow main = Owner as mainWindow;

            if (updateEvent != null && localEndedEvent == null)
            {
                main.levents.DeleteOnSubmit(updateEvent);

                LocalEndedEvents ev = new LocalEndedEvents
                {
                    EventName = updateEvent.EventName,
                    EventDescription = updateEvent.EventDescription,
                    EventStart = updateEvent.EventStart,
                    EventEnd = updateEvent.EventEnd,
                };
                main.leevents.InsertOnSubmit(ev);
                main.mf.DB.SubmitChanges();

                main.refreshDash();
                main.syncButton.IsEnabled = true;

                messageBox message = new messageBox("Atenție", "Sarcina " + name.Text + " a fost finalizată!", "Ok");
                message.ShowDialog();
                Close();
            }
        }
        private void endDatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(startDatePicker.SelectedDate > endDatePicker.SelectedDate) startDatePicker.SelectedDate = endDatePicker.SelectedDate;
        }

        private void endTimePicker_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if(startDatePicker.SelectedDate == endDatePicker.SelectedDate && startTimePicker.SelectedTime > endTimePicker.SelectedTime) startTimePicker.SelectedTime = endTimePicker.SelectedTime;
        }
    }
}
