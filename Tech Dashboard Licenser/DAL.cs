using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Tech_Dashboard_Licenser.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Tech_Dashboard_Licenser
{
    public class DAL
    {
        private SQLiteConnection _database;

        public DAL()
        {
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Job Ops") == false)
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Job Ops");
            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Job Ops\\licensing.db"))
            {
                SQLiteConnection.CreateFile(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Job Ops\\licensing.db");
            }
            _database = new SQLiteConnection(@"Data Source="+Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Job Ops\\licensing.db; Version=3; FailIfMissing=True; Foreign Keys=True;");
        }

        public bool TableExists<T>()
        {
            string cmdText = "SELECT name FROM sqlite_master WHERE type='table' AND name=@name";
            var cmd = _database.CreateCommand();
            cmd.CommandText = cmdText;
            cmd.Parameters.AddWithValue("@name", typeof(T).Name);
            bool returnTable = false;
            try
            {
                _database.Open();
                returnTable = cmd.ExecuteScalar() != null;
                _database.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                _database.Close();
            }
            return returnTable;
        }

        public void CheckTables()
        {
            if (!TableExists<License>())
            {
                string createLicenseTableSql = @"CREATE TABLE License (" +
                    "LicenseID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE," +
                    "CompanyName TEXT NOT NULL," +
                    "TypeOfLicense TEXT, " +
                    "TotalLicenseCount INTEGER," +
                    "LicenseIssueDate DATETIME," +
                    "LicenseExpirationDate DATETIME," +
                    "LicenseEncodedString TEXT )";
                SQLiteCommand cmd = _database.CreateCommand();
                cmd.CommandText = createLicenseTableSql;
                try
                {
                    _database.Open();
                    cmd.ExecuteNonQuery();
                    _database.Close();
                }
                catch(Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    _database.Close();
                }
            }
        }
    }

    public class LicenserContext : DbContext
    {
        public DbSet<License> Licenses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<LicenserContext>(null);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
