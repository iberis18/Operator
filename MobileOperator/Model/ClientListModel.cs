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
    class ClientListModel : INotifyPropertyChanged
    {
        private List<ClientModel> allClients = new List<ClientModel>();
        private List<UL> allULs;
        private List<FL> allFLs;
        private DAL.MobileOperator db = new DAL.MobileOperator();
        public ClientListModel()
        {
            checkDB(db);
            allFLs = db.FL.ToList();
            allULs = db.UL.ToList();
            foreach (UL ul in allULs)
                allClients.Add(new ULModel(ul));
            foreach (FL fl in allFLs)
                allClients.Add(new FLModel(fl));
            allClients = allClients.OrderBy(x => x.Id).ToList();
        }
        public ClientListModel(int rateId)
        {
            checkDB(db);
            allClients = db.Client.Where(i => i.rate == rateId).ToList().Select(i => new ClientModel(i)).ToList();
        }

        public List<ClientModel> AllClients
        {
            get { return allClients; }
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
