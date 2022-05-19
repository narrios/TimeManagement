using System;
using System.IO;
using System.Windows;
using System.Linq;
using System.Data.Linq;
using System.Net;

namespace TimeManagement.Classes
{
    public class mainFunctions
    {
        //string mainDBconn = @"Data Source=localhost,1433;Initial Catalog=MainDB;Persist Security Info=True;User ID=sa;Password=v0rn13M4x";
        string mainDBconn = @"Data Source=ToDo-DataBase.mssql.somee.com;Persist Security Info=False;Initial Catalog=ToDo-DataBase;User=n4rr_SQLLogin_1;Password=d6ff4nrxwd;";
        public DataContext MainDB;
        public localDB DB;
        Table<Settings> setari;

        public void createDB()
        {
            string dir = @"C:\ToDo";
            if (!Directory.Exists(dir))
            {
                DirectoryInfo di = Directory.CreateDirectory(dir);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

            DB = new localDB(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\ToDo\database.mdf';Integrated Security=True");
            if (!DB.DatabaseExists()) DB.CreateDatabase();

            setari = DB.GetTable<Settings>();
            if(setari.ToList().Count() == 0)
            {
                Settings setare = new Settings()
                {
                    Id = 1,
                    EndOn = 7,
                    Timer = 5,
                    Theme = "Light"
                };
                setari.InsertOnSubmit(setare);
                DB.SubmitChanges();
            }
        }

        public void connectDBs()
        {
            MainDB = new DataContext(mainDBconn);
            createDB();
        }

        public void changeTheme(Settings setting)
        {
            if(setting.Theme == "Light")
            {
                Application.Current.Resources.MergedDictionaries[0].Source = new Uri("/Themes/light.xaml", UriKind.RelativeOrAbsolute);
                Application.Current.Resources.MergedDictionaries[1].Source = new Uri("/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.light.xaml", UriKind.RelativeOrAbsolute);
            }
            else
            {
                Application.Current.Resources.MergedDictionaries[0].Source = new Uri("/Themes/dark.xaml", UriKind.RelativeOrAbsolute);
                Application.Current.Resources.MergedDictionaries[1].Source = new Uri("/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.dark.xaml", UriKind.RelativeOrAbsolute);
            }
        }

        public bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
