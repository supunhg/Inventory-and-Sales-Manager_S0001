using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
using System.Reflection;
using System.Windows.Forms;

namespace fridayGUI_2
{
    internal class DatabaseHelper
    {
        private static readonly string dbFileName = "OrderDB.db";
        private static readonly string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbFileName);

        public static string connStr => $"Data Source={dbPath};";

        public static void InitializeDatabase()
        {
            if (!File.Exists(dbPath))
            {
                ExtractEmbeddedDatabase();
            }
        }

        private static void ExtractEmbeddedDatabase()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = "fridayGUI_2.Resources.OrderDB.db";

                using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (resourceStream == null)
                    {
                        throw new Exception("Resource not found: " + resourceName);
                    }

                    using (FileStream fileStream = new FileStream(dbPath, FileMode.Create))
                    {
                        resourceStream.CopyTo(fileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to extract database: " + ex.Message);
            }
        }

        public static SQLiteConnection GetConnection()
        {
            InitializeDatabase();
            return new SQLiteConnection(connStr);
        }
    }
}
