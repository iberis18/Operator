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
    class UserListModel : INotifyPropertyChanged
    {
        private List<ClientModel> allClients;
        private List<AdminModel> allAdmins;
        private DAL.MobileOperator db = new DAL.MobileOperator();
        public UserListModel()
        {
            checkDB(db);
            allAdmins = db.Admin.ToList().Select(i => new AdminModel(i, i.User)).ToList();
            allClients = db.Client.ToList().Select(i => new ClientModel(i.User, i)).ToList();
        }
        public List<ClientModel> AllClients
        {
            get { return allClients; }
        }
        public List<AdminModel> AllAdmins
        {
            get { return allAdmins; }
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
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
