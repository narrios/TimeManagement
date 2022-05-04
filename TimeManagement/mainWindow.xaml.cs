using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using TimeManagement.Classes;

namespace TimeManagement
{
    /// <summary>
    /// Логика взаимодействия для mainWindow.xaml
    /// </summary>
    public partial class mainWindow : Window
    {
        public DispatcherTimer timer = new DispatcherTimer();
        public ObservableCollection<DisplayEvent> displayEvents = new ObservableCollection<DisplayEvent>();
        public ObservableCollection<DisplayEvent> displayEndedEvents = new ObservableCollection<DisplayEvent>();
        public ObservableCollection<DisplayEvent> calendarEvents = new ObservableCollection<DisplayEvent>();

        Nullable<DateTime> calSelectedMaxValue, calSelectedMinValue;

        public mainFunctions mf = new mainFunctions();
        public Table<DB.Events> events;
        public Table<DB.EndedEvents> eevents;
        public Table<LocalEvents> levents;
        public Table<LocalEndedEvents> leevents;
        public Settings setare;
        public DB.Users user;

        public mainWindow(DB.Users user)
        {
            InitializeComponent();

            refreshData();
            
            if(user != null)
            {
                this.user = user;
                accountName.Text = user.Name;
            }
            else
            {
                this.user = user;
                accountName.Text = "Guest";
            }

            timer.Interval = new TimeSpan(0, setare.Timer, 0);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(user.UserId == mf.DB.GetTable<LocalUser>().Where(x => x.LocalUserId == 1).Single().UserId)
            {
                messageBox message = new messageBox("Atenție", "Doriți să faceți refresh la sarcini?", "YesNo");
                message.buttonYes.Click += ButtonYes_Click;
                message.ShowDialog();
            }
            else
            {
                refreshLocalDatabase();
            }

            addEndedEvents();
            refreshDashboard();
            refreshEndedEventsDashboard();
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            refreshLocalDatabase();
        }

        //=============================Refresh la toate tabelele din ambele baze de date=============================//
        public void refreshData()
        {
            mf.connectDBs();

            events = mf.MainDB.GetTable<DB.Events>();
            eevents = mf.MainDB.GetTable<DB.EndedEvents>();
            levents = mf.DB.GetTable<LocalEvents>();
            leevents = mf.DB.GetTable<LocalEndedEvents>();
            setare = mf.DB.GetTable<Settings>().Where(x => x.Id == 1).First();
        }
        public void refreshDash()
        {
            refreshData();
            refreshDashboard();
            refreshEndedEventsDashboard();
        }
        //=============================Stergerea datelor din baza de date locala si preluarea acestora din baza de date globala=============================//
        public void refreshLocalDatabase()
        {
            foreach (var row in levents)
            {
                levents.DeleteOnSubmit(row);
            }
            foreach (var row in leevents)
            {
                leevents.DeleteOnSubmit(row);
            }
            if (this.user != null)
            {
                foreach (var eventNet in events.Where(ev => ev.UserId == user.UserId))
                {
                    LocalEvents n = new LocalEvents
                    {
                        EventId = eventNet.EventId,
                        EventName = eventNet.EventName,
                        EventDescription = eventNet.EventDescription,
                        EventStart = eventNet.EventStart,
                        EventEnd = eventNet.EventEnd,
                    };
                    levents.InsertOnSubmit(n);
                }
                foreach (var eventNet in eevents.Where(ev => ev.UserId == user.UserId))
                {
                    LocalEndedEvents n = new LocalEndedEvents
                    {
                        EndedEventId = eventNet.EndedEventId,
                        EventName = eventNet.EventName,
                        EventDescription = eventNet.EventDescription,
                        EventStart = eventNet.EventStart,
                        EventEnd = eventNet.EventEnd,
                    };
                    leevents.InsertOnSubmit(n);
                }
            }

            mf.DB.SubmitChanges();
        }
        //=============================Adugarea a sarcinilor terminate in tabelul EndedEventsLocal=============================//
        private void addEndedEvents()
        {
            foreach (var e in levents)
            {
                if (e.EventEnd < DateTime.Now && e.EventEnd != null)
                {
                    LocalEndedEvents newEndedEvent = new LocalEndedEvents()
                    {
                        EventName = e.EventName,
                        EventDescription = e.EventDescription,
                        EventEnd = e.EventEnd,
                        EventStart = e.EventStart,
                    };
                    leevents.InsertOnSubmit(newEndedEvent);
                    levents.DeleteOnSubmit(e);
                }
            }
            mf.DB.SubmitChanges();
        }
        //=============================Functia de sincronizare a tuturor datelor din ambele baze de date=============================//
        public void sync()
        {
            if (syncButton.IsEnabled == true)
            {
                addEndedEvents();

                //Sincronizarea evenimentelor 
                refreshData();
                foreach (var localEvent in levents)
                {
                    //Modificarea evenimentelor
                    DB.Events updateEvent = events.FirstOrDefault(ev => ev.EventId == localEvent.EventId);
                    if (updateEvent != null)
                    {
                        updateEvent.EventName = localEvent.EventName;
                        updateEvent.EventDescription = localEvent.EventDescription;
                        updateEvent.EventStart = localEvent.EventStart;
                        updateEvent.EventEnd = localEvent.EventEnd;
                    }
                    else
                    {
                        //Adaugarea evenimentelor noi
                        DB.Events ev = new DB.Events
                        {
                            UserId = user.UserId,
                            EventName = localEvent.EventName,
                            EventDescription = localEvent.EventDescription,
                            EventStart = localEvent.EventStart,
                            EventEnd = localEvent.EventEnd,
                        };

                        events.InsertOnSubmit(ev);
                    }
                }
                //Stergerea evenimentelor
                for (int i = events.ToList().Count - 1; i >= 0; i--)
                {
                    int id = events.ToList()[i].EventId;
                    if (levents.FirstOrDefault(ev => ev.EventId == id) == null)
                    {
                        events.DeleteOnSubmit(events.ToList()[i]);
                    }
                }
                //Sincronizarea evenimentelor ne actuale
                foreach (var localEndedEvent in leevents)
                {
                    //Modificarea evenimentelor
                    DB.EndedEvents updateEndedEvent = eevents.FirstOrDefault(ev => ev.EndedEventId == localEndedEvent.EndedEventId);
                    if (updateEndedEvent != null)
                    {
                        updateEndedEvent.EventName = localEndedEvent.EventName;
                        updateEndedEvent.EventDescription = localEndedEvent.EventDescription;
                        updateEndedEvent.EventStart = localEndedEvent.EventStart;
                        updateEndedEvent.EventEnd = localEndedEvent.EventEnd;
                    }
                    else
                    {
                        //Adaugarea evenimentelor noi
                        DB.EndedEvents ev = new DB.EndedEvents
                        {
                            UserId = user.UserId,
                            EventName = localEndedEvent.EventName,
                            EventDescription = localEndedEvent.EventDescription,
                            EventStart = localEndedEvent.EventStart,
                            EventEnd = localEndedEvent.EventEnd,
                        };

                        eevents.InsertOnSubmit(ev);
                    }
                }
                //Stergerea evenimentelor
                for (int i = eevents.ToList().Count - 1; i >= 0; i--)
                {
                    int id = eevents.ToList()[i].EndedEventId;
                    if (leevents.FirstOrDefault(ev => ev.EndedEventId == id) == null)
                    {
                        eevents.DeleteOnSubmit(eevents.ToList()[i]);
                    }
                }
                //Salvare modificari din baza de date 
                mf.DB.SubmitChanges();
                mf.MainDB.SubmitChanges();
                syncButton.IsEnabled = false;
            }
        }
        //=============================Refresh la sarcinile din tabelul cu sarcini=============================//
        public void refreshDashboard()
        {
            refreshData();
            displayEvents.Clear();
            List<LocalEvents> Events3Days = levents.Where(e => e.EventEnd <= DateTime.Now.AddDays(setare.EndOn) || e.EventEnd == null).ToList();
            foreach (LocalEvents e in Events3Days)
            {
                bool show = e.EventStart != null;
                string time1 = "", time2 = "";
                if (e.EventStart != null)
                {
                    time1 = e.EventStart.ToString().Substring(0, e.EventStart.ToString().Length - 3);
                    time2 = e.EventEnd.ToString().Substring(0, e.EventEnd.ToString().Length - 3);
                }
                displayEvents.Add(new DisplayEvent
                {
                    Id = e.LocalEventId,
                    Name = e.EventName,
                    Description = e.EventDescription,
                    Start = time1,
                    End = time2,
                    ShowTime = show,
                });
            }
            justEvents.ItemsSource = timeEvents.ItemsSource = null;
            justEvents.ItemsSource = displayEvents.Where(e => !e.ShowTime);
            timeEvents.ItemsSource = displayEvents.Where(e => e.ShowTime);
        }
        //=============================Refresh la sarcinile din tabelul cu sarcini terminate=============================//
        public void refreshEndedEventsDashboard()
        {
            refreshData();
            displayEndedEvents.Clear();
            List<LocalEndedEvents> Events3Days = leevents.Where(e => e.EventEnd <= DateTime.Now.AddDays(setare.EndOn) || e.EventEnd == null).ToList();
            foreach (LocalEndedEvents e in Events3Days)
            {
                string time1 = "", time2 = "";

                time1 = e.EventStart.ToString().Substring(0, e.EventStart.ToString().Length - 3);
                time2 = e.EventEnd.ToString().Substring(0, e.EventEnd.ToString().Length - 3);
                displayEndedEvents.Add(new DisplayEvent
                {
                    Id = e.LocalEndedEventId,
                    Name = e.EventName,
                    Description = e.EventDescription,
                    Start = time1,
                    End = time2,
                    ShowTime = true,
                });
            }
            timeEvents1.ItemsSource = null;
            timeEvents1.ItemsSource = displayEndedEvents.Where(e => e.ShowTime);
        }
        //=============================Refresh la sarcinile din calendar=============================//
        public void refreshCalendarEvents()
        {
            refreshData();
            eventsFromDate.ItemsSource = null;
            calendarEvents.Clear();
            //=============================Afisarea sarcinilor finalizate din calendar=============================//
            List<LocalEndedEvents> endedEventsToShow = leevents.Where(ev => ev.EventStart <= calSelectedMaxValue && ev.EventEnd >= calSelectedMinValue && ev.EventStart != null).ToList();
            foreach (var ev in endedEventsToShow)
            {
                bool show = ev.EventStart != null;
                string time1 = "", time2 = "";
                if (ev.EventStart != null)
                {
                    time1 = ev.EventStart.ToString().Substring(0, ev.EventStart.ToString().Length - 3);
                    time2 = ev.EventEnd.ToString().Substring(0, ev.EventEnd.ToString().Length - 3);
                }
                calendarEvents.Add(new DisplayEvent
                {
                    Id = ev.LocalEndedEventId,
                    Name = ev.EventName,
                    Description = ev.EventDescription,
                    Start = time1,
                    End = time2,
                    ShowTime = show,
                });
            }
            //=============================Afisarea sarcinilor ce nu s-au finalizar inca din calendar=============================//
            List<LocalEvents> eventsToShow = levents.Where(ev => ev.EventStart <= calSelectedMaxValue && ev.EventEnd >= calSelectedMinValue && ev.EventStart != null).ToList();
            foreach (var ev in eventsToShow)
            {
                bool show = ev.EventStart != null;
                string time1 = "", time2 = "";
                if (ev.EventStart != null)
                {
                    time1 = ev.EventStart.ToString().Substring(0, ev.EventStart.ToString().Length - 3);
                    time2 = ev.EventEnd.ToString().Substring(0, ev.EventEnd.ToString().Length - 3);
                }
                calendarEvents.Add(new DisplayEvent
                {
                    Id = ev.LocalEventId,
                    Name = ev.EventName,
                    Description = ev.EventDescription,
                    Start = time1,
                    End = time2,
                    ShowTime = show,
                });
            }
            eventsFromDate.ItemsSource = calendarEvents;
        }
        //=============================Sincronizarea la apasarea butonului respectiv=============================//
        private void sync_Click(object sender, RoutedEventArgs e)
        {
            sync();
            refreshData();
            refreshDashboard();
            refreshEndedEventsDashboard();
            messageBox message = new messageBox("Atenție", "A avut loc sincronizarea datelor", "Ok");
            message.ShowDialog();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if(syncButton.IsEnabled == true)
            {
                sync();
                refreshData();
                refreshDashboard();
                refreshEndedEventsDashboard();
            }  
        }
        //=============================Adaugarea unei sarcini simple la apasarea butonului respectiv=============================//
        private void addEventButton_Click(object sender, RoutedEventArgs e)
        {
            addEventWindow add = new addEventWindow()
            {
                Owner = this,
            };
            add.Show();
        }
        //=============================Adaugarea unei sarcini dezvoltate la apasarea butonului respectiv=============================//
        private void expandEventButton_MouseRightClick(object sender, RoutedEventArgs e)
        {
            expandEventWindow expand = new expandEventWindow(null, null)
            {
                Owner = this,
            };
            expand.Show();
        }
        //=============================Editarea sarcinilor=============================//
        private void updateEventButton_Click(object sender, RoutedEventArgs e)
        {
            int Id = int.Parse(((sender as Button).Content as DockPanel).Children.OfType<TextBlock>().FirstOrDefault().Text);
            expandEventWindow updateEvent = new expandEventWindow(levents.FirstOrDefault(ev => ev.LocalEventId == Id), leevents.FirstOrDefault(ev => ev.LocalEndedEventId == Id))
            {
                Owner = this,
            };
            updateEvent.ShowDialog();
        }
        private void updateEventsFromCalendar_Click(object sender, RoutedEventArgs e)
        {
            int Id = int.Parse(((sender as Button).Content as DockPanel).Children.OfType<TextBlock>().FirstOrDefault().Text);
            expandEventWindow updateEvent = new expandEventWindow(levents.FirstOrDefault(ev => ev.LocalEventId == Id), leevents.FirstOrDefault(ev => ev.LocalEndedEventId == Id))
            {
                Owner = this,
            };
            updateEvent.ShowDialog();
        }
        //=============================Deschiderea meniurilor=============================//
        //=============================Meniul principal din partea stanga=============================//
        private void OpenMenuButton_Click(object sender, RoutedEventArgs e)
        {
            closeMenuButton.Visibility = Visibility.Visible;
            openMenuButton.Visibility = Visibility.Collapsed;
        }
        private void CloseMenuButton_Click(object sender, RoutedEventArgs e)
        {
            closeMenuButton.Visibility = Visibility.Collapsed;
            openMenuButton.Visibility = Visibility.Visible;
        }
        //=============================Meniul calendarului din partea dreapta=============================//
        private void OpenRightMenuButton_Click(object sender, RoutedEventArgs e)
        {
            refreshCalendarEvents();
            calContainer.Visibility = Visibility.Visible;
            closeRightMenuButton.Visibility = Visibility.Visible;
            openRightMenuButton.Visibility = Visibility.Collapsed;
        }
        private void CloseRightMenuButton_Click(object sender, RoutedEventArgs e)
        {
            calContainer.Visibility = Visibility.Collapsed;
            closeRightMenuButton.Visibility = Visibility.Collapsed;
            openRightMenuButton.Visibility = Visibility.Visible;
        }
        //=============================Butoane barei din stanga=============================//
        private void openDashboard_Click(object sender, RoutedEventArgs e)
        {
            tabs.SelectedIndex = 0;
            tabName.Text = "Sarcini";
        }
        private void openEndedEventsDashboard_Click(object sender, RoutedEventArgs e)
        {
            tabs.SelectedIndex = 1;
            tabName.Text = "Sarcini finalizate";
        }
        private void openCalendar_Click(object sender, RoutedEventArgs e)
        {
            tabs.SelectedIndex = 2;
            tabName.Text = "Calendar";
        }
        //=============================Butoanele de pe bara de sus=============================//
        private void openAccount_Click(object sender, RoutedEventArgs e)
        {
            settingsWindow settings = new settingsWindow(user)
            {
                Owner = this,
            };
            settings.tabs.SelectedIndex = 0;
            settings.ShowDialog();
        }
        private void openSettings_Click(object sender, RoutedEventArgs e)
        {
            settingsWindow settings = new settingsWindow(user)
            {
                Owner = this,
            };
            settings.tabs.SelectedIndex = 1;
            settings.ShowDialog();
        }
        private void openAbout_Click(object sender, RoutedEventArgs e)
        {
            about about = new about();
            about.ShowDialog();
        }
        private void unlogin_Click(object sender, RoutedEventArgs e)
        {
            startWindow start = new startWindow();
            start.Show();
            Close();
        }
        
        //=============================Alte functii=============================//
        private void selectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            calSelectedMaxValue = cal.SelectedDate.Value.Date.Add(new TimeSpan(23, 59, 59));
            calSelectedMinValue = cal.SelectedDate.Value.Date.Add(new TimeSpan(0, 0, 0));
        }
        private void onClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(user != null) sync();
        }
    }
}
