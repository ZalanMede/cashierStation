using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cashierStation_zaroProject.Models
{
    internal class GenericRepository<T> : InterFGenRepo<T> where T : new()
    {
        private readonly string databasePath;

        public GenericRepository(string databasePath)
        {
            this.databasePath = databasePath;
        }

        public List<T> GetAll()
        {
            using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(databasePath))
            {
                connection.CreateTable<T>();
                return connection.Table<T>().ToList();
            }
        }
        public void Insert(T item)
        {
            using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(databasePath))
            {
                try
                {
                    connection.CreateTable<T>();
                    connection.Insert(item);
                }
                catch (Exception)
                {

                }

            }
        }
        public void Update(T item)
        {
            using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(databasePath))
            {
                connection.CreateTable<T>();
                connection.Update(item);
            }
        }
        public void Delete(T item)
        {
            using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(databasePath))
            {
                connection.CreateTable<T>();
                connection.Delete(item);
            }
        }
    }
}
