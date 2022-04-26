using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TimeManagement
{
    /// <summary>
    /// Interaction logic for messageBox.xaml
    /// </summary>
    public partial class messageBox : Window
    {
        public messageBox(string title, string message, string type)
        {
            InitializeComponent();
            this.Title = title;
            this.message.Text = message;
            if (type == "Ok") { buttonOk.Visibility = Visibility.Visible; buttonNo.Visibility = Visibility.Hidden; buttonYes.Visibility = Visibility.Hidden; }
            else { buttonOk.Visibility = Visibility.Hidden; buttonNo.Visibility = Visibility.Visible; buttonYes.Visibility = Visibility.Visible; }
        }

        private void buttonNo_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonYes_Click(object sender, RoutedEventArgs e)
        {
            /*mainWindow main = Owner as mainWindow;
            main.MessageBoxResult = 1;*/
            Close();
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
