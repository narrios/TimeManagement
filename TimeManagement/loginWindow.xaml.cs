using System.Data.Linq;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TimeManagement.Classes;

namespace TimeManagement
{
    /// <summary>
    /// Логика взаимодействия для loginWindow.xaml
    /// </summary>
    public partial class loginWindow : Window
    {
        mainFunctions mf = new mainFunctions();
        Table<DB.Users> utilizatori;
        Table<LocalUser> localuser;
        mainWindow main;
        bool show = false;
        public loginWindow()
        {
            InitializeComponent();
            mf.connectDBs();
            utilizatori = mf.MainDB.GetTable<DB.Users>();
            localuser = mf.DB.GetTable<LocalUser>();

            email.Text = "";
            password.Password = "";
        }
        //=============================Trecerea la fereastra de registrare=============================//
        private void goToSign_Click(object sender, RoutedEventArgs e)
        {
            signupWindow sign = new signupWindow();
            sign.Show();
            Close();
        }
        //=============================Logarea la apasarea butonului respectiv=============================//
        private void login_Click(object sender, RoutedEventArgs e)
        {
            startWindow start = Owner as startWindow;
            DB.Users user = (from ev in utilizatori where ev.Email == email.Text && ev.Password == password.Password select ev).FirstOrDefault();
            if (user != null)
            {
                main = new mainWindow(user);
                if (memorizecheck.IsChecked == true)
                {
                    LocalUser l = (from x in localuser where x.LocalUserId.Equals(1) select x).FirstOrDefault();
                    if (l != null)
                    {
                        l.UserName = user.Name;
                        l.Email = user.Email;
                        l.Password = user.Password;
                        mf.DB.SubmitChanges();
                    }
                    else
                    {
                        LocalUser lu = new LocalUser { UserName = user.Name, Email = user.Email, Password = user.Password, UserId = user.UserId };
                        localuser.InsertOnSubmit(lu);
                        mf.DB.SubmitChanges();
                    }
                }
                start.Close();
                main.Show();
            }
            else
            {
                error.Visibility = Visibility.Visible;
                password.Password = "";
            }
        }
        //=============================Modificarea textului din caseta Email=============================//
        private void email_TextChanged(object sender, TextChangedEventArgs e)
        {
            error.Visibility = Visibility.Hidden;
        }
        //=============================Modificarea textului din caseta Password=============================//
        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            error.Visibility = Visibility.Hidden;
        }

        private void passwordSymbols_TextChanged(object sender, TextChangedEventArgs e)
        {
            error.Visibility = Visibility.Hidden;
        }

        private void showPassword_Click(object sender, RoutedEventArgs e)
        {
            show = !show;

            if (show)
            {
                password.Visibility = Visibility.Hidden;
                passwordSymbols.Visibility = Visibility.Visible;
                passwordSymbols.Text = password.Password;
            }
            else
            {
                password.Visibility = Visibility.Visible;
                passwordSymbols.Visibility = Visibility.Hidden;
                password.Password = passwordSymbols.Text;
            }
        }
    }
}
