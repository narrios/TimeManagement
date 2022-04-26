using System.Windows;
using System.Linq;
using System.Data.Linq;
using TimeManagement.Classes;
using System;

namespace TimeManagement
{
    /// <summary>
    /// Логика взаимодействия для settingsWindow.xaml
    /// </summary>
    public partial class settingsWindow : Window
    {
        mainFunctions mf = new mainFunctions();
        DB.Users user;
        Settings setare;

        public settingsWindow(DB.Users user)
        {
            InitializeComponent();

            mf.connectDBs();

            this.user = user;
            setare = (mf.DB.GetTable<Settings>()).Where(x => x.Id == 1).First();

            name.Text = user.Name;
            email.Text = user.Email;
            password.Password = user.Password;
            passwordContent.Text = password.Password;

            timer.Text = setare.Timer.ToString();
            endOn.Text = setare.EndOn.ToString();
            if(setare.Theme == "Light") theme.SelectedIndex = 0;
            else theme.SelectedIndex = 1;
        }
        //Modificarea datelor despre utilizatori
        private void modifyName_Click(object sender, RoutedEventArgs e)
        {
            name.IsEnabled = true;
            apply.IsEnabled = true;
        }

        private void modifyEmail_Click(object sender, RoutedEventArgs e)
        {
            email.IsEnabled = true;
            apply.IsEnabled = true;
        }

        private void modifyPassword_Click(object sender, RoutedEventArgs e)
        {
            password.Visibility = Visibility.Hidden;
            passwordContent.Visibility = Visibility.Visible;
            passwordContent.Text = password.Password;
            apply.IsEnabled = true;
        }

        private void apply_Click(object sender, RoutedEventArgs e)
        {
            mainWindow main = Owner as mainWindow;
            DB.Users lUser = (mf.MainDB.GetTable<DB.Users>()).Where(x => x.UserId == user.UserId).First();
            string initPass = user.Password, initEmail = user.Email, initName = user.Name;

            if(name.Text != "" && email.Text != "" && passwordContent.Text != "")
            {
                lUser.Name = name.Text; initName = name.Text;
                lUser.Email = email.Text; initEmail = email.Text;
                lUser.Password = passwordContent.Text; initPass = passwordContent.Text; password.Password = passwordContent.Text;
                mf.MainDB.SubmitChanges();
                main.accountName.Text = lUser.Name;
                messageBox message1 = new messageBox("Succes", "Modificarea utilizatorului a avut loc cu succes", "Ok");
                message1.ShowDialog();

                name.IsEnabled = false;
                email.IsEnabled = false;
                password.Visibility = Visibility.Visible;
                passwordContent.Visibility = Visibility.Hidden;
                apply.IsEnabled = false;
            }
            if (initPass != lUser.Password || initEmail != lUser.Email)
            {
                messageBox message2 = new messageBox("Atenție", "Veți fi mutat la fereastra de start", "Ok");
                message2.buttonOk.Click += ButtonOk_Click;
                message2.ShowDialog();
            }
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            startWindow start = new startWindow();
            start.Show();
            mainWindow main = Owner as mainWindow;
            Close();
            main.Close();
        }
        //Modifacarea setarilor timer-ului de sincronizare
        private void modifyTimer_Click(object sender, RoutedEventArgs e)
        {
            timer.IsEnabled = true;
            applyTimer.IsEnabled = true;
        }
        private void applyTimer_Click(object sender, RoutedEventArgs e)
        {
            mainWindow main = Owner as mainWindow;

            setare.Timer = Convert.ToInt32(timer.Text);
            timer.IsEnabled = false;
            applyTimer.IsEnabled = false;
            main.setare = setare;
            mf.DB.SubmitChanges();
        }
        //Modificarea setarilor distantei de afisare a sarcinilor
        private void modifyEnd_Click(object sender, RoutedEventArgs e)
        {
            endOn.IsEnabled = true;
            applyEnd.IsEnabled = true;
        }
        private void applyEnd_Click(object sender, RoutedEventArgs e)
        {
            mainWindow main = Owner as mainWindow;

            setare.EndOn = Convert.ToInt32(endOn.Text);
            endOn.IsEnabled = false;
            applyEnd.IsEnabled = false;
            main.setare = setare;
            mf.DB.SubmitChanges();
        }
        //Modificarea temei
        private void theme_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            applyTheme.IsEnabled = true;
        }
        private void applyTheme_Click(object sender, RoutedEventArgs e)
        {
            setare.Theme = theme.SelectedValue.ToString();
            applyTheme.IsEnabled = false;
            mf.DB.SubmitChanges();
            mf.changeTheme(setare);
        }

    }
}
