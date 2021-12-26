using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DAL;
using System.Windows;

namespace MobileOperator.Model
{
    public class AdminModel : UserModel
    {
        private DAL.MobileOperator db = new DAL.MobileOperator();
        private Admin admin = new Admin();
        public AdminModel() : base() { checkDB(db); }
        public AdminModel(Admin a, User u) : base(u)
        {
            checkDB(db);
            admin = a;
        }
        public AdminModel(int id) : base(id)
        {
            checkDB(db);
            admin = db.Admin
                .Where(i => i.userId == id).FirstOrDefault();
        }

        public string Login
        {
            get { return admin.login; }
            set
            {
                admin.login = value;
                OnPropertyChanged("AdminLogin");
            }
        }
        private void DBException()
        {
            MessageBox.Show("Ошибка подключения к базе, приложение будет закрыто", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            Environment.Exit(0);
        }

        private void checkDB(DAL.MobileOperator db)
        {
            try
            {
                if (!db.Database.Exists())
                {
                    DBException();
                }
            }
            catch (System.InvalidOperationException)
            {
                DBException();
            }
        }
    }
}
