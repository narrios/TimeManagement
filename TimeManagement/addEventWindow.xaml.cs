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
            if (name.Text == "")
            {
                messageBox message = new messageBox("Atenție", "Nu puteți crea o sarcină fără denumire", "Ok");
                message.ShowDialog();
            }
            else if (description.Text == "")
            {
                messageBox message = new messageBox("Atenție", "Nu puteți crea o sarcină fără descriere", "Ok");
                message.ShowDialog();
            }
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
                main.refreshDash();
                if(main.user != null) main.syncButton.IsEnabled = true;
                messageBox message = new messageBox("Atenție", "Sarcina " + name.Text + " a fost adăugată cu succes!", "Ok");
                message.ShowDialog();
                Close();
            }
        }
    }
}
