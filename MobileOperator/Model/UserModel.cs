using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DAL;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;

namespace MobileOperator.Model
{
    public class UserModel : INotifyPropertyChanged
    {
        protected User user = new User();
        private int status;
        protected DAL.MobileOperator db = new DAL.MobileOperator();

        public UserModel() { checkDB(db); }
        public UserModel(User u)
        {
            checkDB(db);
            user = u;
            status = CheckStatus();
        }
        public UserModel(int id)
        {
            checkDB(db);
            user = db.User
                .Where(i => i.id == id).FirstOrDefault();
            status = CheckStatus();
        }

        private int CheckStatus()
        {
            if (user.Admin != null)
                return 1;
            else if (user.Client.UL != null)
                return 2;
            else return 3;
        }

        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public int Id
        {
            get { return user.id; }
            set { user.id = value; }
        }

        public string Password
        {
            get { return user.password; }
            set { user.password = value; }
        }

        public virtual string Number { get; set; }

        virtual public bool Save() { return false; }
        virtual public bool Remove()
        {
            return false;
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
