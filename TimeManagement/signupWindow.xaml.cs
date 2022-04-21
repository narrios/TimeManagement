using System.Data.Linq;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using TimeManagement.Classes;

namespace TimeManagement
{
    /// <summary>
    /// Логика взаимодействия для signupWindow.xaml
    /// </summary>
    public partial class signupWindow : Window
    {
        mainFunctions mf = new mainFunctions();
        Table<DB.Users> utilizatori;
        bool show = false, show1 = false;
        public signupWindow()
        {
            InitializeComponent();
            mf.connectDBs();
            utilizatori = mf.MainDB.GetTable<DB.Users>();
        }
        private void GoToLog_Click(object sender, RoutedEventArgs e)
        {
            loginWindow log = new loginWindow();
            log.Show();
            Close();
        }

        private void signUp_Click(object sender, RoutedEventArgs e)
        {
            bool validation = true;
            if (name.Text == "" || !new Regex("^[A-Z]{1}[A-z]+([\\s][A-Z]{1}[A-z]+)+$").IsMatch(name.Text)) { name.BorderThickness = new Thickness(2); errorName.Visibility = Visibility.Visible; validation = false; }
            if (email.Text == "" || !new Regex("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$").IsMatch(email.Text)) { email.BorderThickness = new Thickness(2); errorEmail.Visibility = Visibility.Visible; errorEmail.Content = "Format email incorect"; validation = false; }
            else
            {
                if (utilizatori.Where(user => user.Email == email.Text).Count() > 0)
                {
                    email.BorderThickness = new Thickness(2); errorEmail.Visibility = Visibility.Visible;
                    errorEmail.Content = "Email-ul deja există";
                    validation = false;
                }
            }
            if (password.Password.Length < 6) { password.BorderThickness = new Thickness(2); passwordSymbols.BorderThickness = new Thickness(2); errorPassword.Visibility = Visibility.Visible; validation = false; }
            if (confirmpassword.Password != password.Password || confirmpassword.Password == "") { confirmpassword.BorderThickness = new Thickness(2); confirmPasswordSymbols.BorderThickness = new Thickness(2); errorConfirmPassword.Visibility = Visibility.Visible; validation = false; }
            if (validation)
            {
                DB.Users user = new DB.Users { Name = name.Text, Email = email.Text, Password = password.Password };
                utilizatori.InsertOnSubmit(user);
                mf.MainDB.SubmitChanges();
                this.Close();
                MessageBox.Show(this, "Utilizatorul a fost creat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        //Numele
        private void Name_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            name.BorderThickness = new Thickness(1); errorName.Visibility = Visibility.Hidden;
        }
        //Email
        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            email.BorderThickness = new Thickness(1); errorEmail.Visibility = Visibility.Hidden;
        }
        //Parola
        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            password.BorderThickness = new Thickness(1); errorPassword.Visibility = Visibility.Hidden;
        }
        private void passwordSymbols_TextChanged(object sender, TextChangedEventArgs e)
        {
            passwordSymbols.BorderThickness = new Thickness(1); errorPassword.Visibility = Visibility.Hidden;
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
        // Confirmarea parola
        private void confirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            confirmpassword.BorderThickness = new Thickness(1); errorConfirmPassword.Visibility = Visibility.Hidden;
        }
        private void confirmPasswordSymbols_TextChanged(object sender, TextChangedEventArgs e)
        {
            confirmpassword.BorderThickness = new Thickness(1); errorConfirmPassword.Visibility = Visibility.Hidden;
        }
        private void showConfirmPassword_Click(object sender, RoutedEventArgs e)
        {
            show1 = !show1;

            if (show1)
            {
                confirmpassword.Visibility = Visibility.Hidden;
                confirmPasswordSymbols.Visibility = Visibility.Visible;
                confirmPasswordSymbols.Text = confirmpassword.Password;
            }
            else
            {
                confirmpassword.Visibility = Visibility.Visible;
                confirmPasswordSymbols.Visibility = Visibility.Hidden;
                confirmpassword.Password = confirmPasswordSymbols.Text;
            }
        }
    }
}
