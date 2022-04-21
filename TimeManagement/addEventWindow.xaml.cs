using System.Windows;
using TimeManagement.Classes;

namespace TimeManagement
{
    /// <summary>
    /// Логика взаимодействия для addEventWindow.xaml
    /// </summary>
    public partial class addEventWindow : Window
    {
        LocalEvents events;
        public addEventWindow()
        {
            InitializeComponent();
        }
        private void addEvent_Click(object sender, RoutedEventArgs e)
        {
            mainWindow main = Owner as mainWindow;
            if (name.Text == "") MessageBox.Show("Nu este introdusă denumirea sarcinii", "Atenție", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (description.Text == "") MessageBox.Show("Nu este introdusă descrierea sarcinii", "Atenție", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                events = new LocalEvents()
                {
                    EventName = name.Text,
                    EventDescription = description.Text,
                    EventStart = null,
                    EventEnd = null
                };
                main.levents.InsertOnSubmit(events);
                main.mf.DB.SubmitChanges();
                main.refreshData();
                if(main.user != null) main.syncButton.IsEnabled = true;
                Close();
            }
        }
    }
}
