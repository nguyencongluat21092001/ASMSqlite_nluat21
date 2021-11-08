using ASMTEST.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMTEST.Helpers
{
    public class DatabaseHelperClass
    {
        public ObservableCollection<Contacts> ReadAllContacts()
        {
            using (SQLite.Net.SQLiteConnection conn
                = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                List<Contacts> listContacts = conn.Table<Contacts>().ToList<Contacts>();
                ObservableCollection<Contacts> collectionContacts = new ObservableCollection<Contacts>(listContacts);
                return collectionContacts;
            }
        }

        public Contacts ReadContact(int contactid)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var existingconact = conn.Query<Contacts>("select * from Contacts where Id =" + contactid).FirstOrDefault();
                return existingconact;
            }
        }

        public void Insert(Contacts objContact)
        {
            using (SQLite.Net.SQLiteConnection conn
                = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Insert(objContact);
                });
            }
        }

        public void Update(Contacts objContact) // Contact contains new Name or new Phone number
        {
            using (SQLite.Net.SQLiteConnection conn
                = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var existingContact = conn.Query<Contacts>("SELECT * FROM Contacts WHERE Id = " + objContact.Id).FirstOrDefault(); //LINQ
                if (existingContact != null)
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Update(objContact);
                    });
                }
            }

        }

        public void Delete(int id) // Contact contains new Name or new Phone number
        {
            using (SQLite.Net.SQLiteConnection conn
                = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var existingContact = conn.Query<Contacts>("SELECT * FROM Contacts WHERE Id = " + id).FirstOrDefault(); //LINQ
                if (existingContact != null)
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Delete(existingContact);
                    });
                }
            }

        }
    }

}
