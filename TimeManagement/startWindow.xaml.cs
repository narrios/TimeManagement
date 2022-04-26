using System;
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
        Table<LocalUser> localuser;
        LocalUser user;

        public startWindow()
        {
            InitializeComponent();
            mf.connectDBs();
            localuser = mf.DB.GetTable<LocalUser>();
            Settings setare = (mf.DB.GetTable<Settings>()).Where(x => x.Id == 1).First();
            user = (from x in localuser where x.LocalUserId.Equals(1) select x).FirstOrDefault();

            mf.changeTheme(setare);

            if(user != null && (mf.MainDB.GetTable<DB.Users>()) != null)
            {
                if (user.UserName != ((mf.MainDB.GetTable<DB.Users>()).Where(x => x.UserId == user.UserId)).First().Name)
                {
                    user.UserName = ((mf.MainDB.GetTable<DB.Users>()).Where(x => x.UserId == user.UserId)).First().Name;
                }
                if (user != null && user.Email == ((mf.MainDB.GetTable<DB.Users>()).Where(x => x.UserId == user.UserId)).First().Email && user.Password == ((mf.MainDB.GetTable<DB.Users>()).Where(x => x.UserId == user.UserId)).First().Password)
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
            try
            {
                DB.Users User = (from ev in (mf.MainDB.GetTable<DB.Users>()) where ev.Email == user.Email && ev.Password == user.Password select ev).FirstOrDefault();
                mainWindow main = new mainWindow(User);
                Close();
                main.Show();
            }
            catch(Exception)
            {
                messageBox message = new messageBox("Atenție", "La moment nu aveți acces la rețea", "Ok");
                message.ShowDialog();
            }
        }

        private void guest_Click(object sender, RoutedEventArgs e)
        {
            mainWindow main = new mainWindow(null);
            main.Show();
            Close();
        }
    }
}
