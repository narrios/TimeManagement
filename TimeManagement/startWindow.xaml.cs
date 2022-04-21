using System.Data.Linq;
using System.Linq;
using System.Windows;
using TimeManagement.Classes;

namespace TimeManagement
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class startWindow : Window
    {
        mainFunctions mf = new mainFunctions();
        Table<DB.Users> utilizatori;
        Table<LocalUser> localuser;
        LocalUser user;

        public startWindow()
        {
            InitializeComponent();
            mf.connectDBs();
            utilizatori = mf.MainDB.GetTable<DB.Users>();
            localuser = mf.DB.GetTable<LocalUser>();
            user = (from x in localuser where x.LocalUserId.Equals(1) select x).FirstOrDefault();
            if (user != null)
            {
                fastLog.Text = "Intră ca " + user.UserName;
                fastLogButton.IsEnabled = true;
            }
            else
            {
                fastLog.Text = "Nu ai fost logat încă";
                fastLogButton.IsEnabled = false;
            }
        }
        private void login_Click(object sender, RoutedEventArgs e)
        {
            loginWindow log = new loginWindow()
            {
                Owner = this,
            };
            log.ShowDialog();
        }

        private void signup_Click(object sender, RoutedEventArgs e)
        {
            signupWindow sign = new signupWindow()
            {
                Owner = this,
            };
            sign.ShowDialog();
        }

        private void fastLog_Click(object sender, RoutedEventArgs e)
        {
            DB.Users User = (from ev in utilizatori where ev.Email == user.Email && ev.Password == user.Password select ev).First();
            mainWindow main = new mainWindow(User);
            Close();
            main.Show();
        }

        private void guest_Click(object sender, RoutedEventArgs e)
        {
            mainWindow main = new mainWindow(null);
            main.Show();
            Close();
        }
    }
}
