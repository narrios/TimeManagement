using System.Data.Linq;
using System.IO;

namespace TimeManagement.Classes
{
    public class mainFunctions
    {
        string mainDBconn = @"Data Source=localhost,1433;Initial Catalog=MainDB;Persist Security Info=True;User ID=sa;Password=v0rn13M4x";
        public DataContext MainDB;
        public localDB DB;

        public void createDB()
        {
            string dir = @"C:\ToDo";
            if (!Directory.Exists(dir))
            {
                DirectoryInfo di = Directory.CreateDirectory(dir);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

            DB = new localDB(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\ToDo\2.mdf';Integrated Security=True");
            if (!DB.DatabaseExists()) DB.CreateDatabase();
        }

        public void connectDBs()
        {
            MainDB = new DataContext(mainDBconn);
            createDB();
        }
    }
}
