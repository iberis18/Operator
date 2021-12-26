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
    class Detailing2Model : INotifyPropertyChanged
    {
        private List<RateHistoryModel> allRates;
        private List<ServiceHistoryModel> allServices;
        private DAL.MobileOperator db = new DAL.MobileOperator();
        public Detailing2Model(int clientId, DateTime from, DateTime till)
        {
            checkDB(db);
            allRates = new List<RateHistoryModel> { };
            allServices = new List<ServiceHistoryModel> { };
            Search(clientId, from, till);
        }

        public Detailing2Model(string clientName, string clientNumber, DateTime from, DateTime till)
        {
            checkDB(db);
            allRates = new List<RateHistoryModel> { };
            allServices = new List<ServiceHistoryModel> { };
            ClientModel client = new ClientModel(); ;
            FL fl;
            UL ul;
            if (clientName != "" && clientNumber != "")
            {
                Client c = db.Client.Where(i => i.number == clientNumber).FirstOrDefault();
                if (c != null)
                    if (c.FL != null && c.FL.FIO == clientName)
                        client = new FLModel(c.userId);
                    else if (c.UL != null && c.UL.organizationName == clientName)
                        client = new ULModel(c.userId);
            }
            else
            {
                if (clientName != "")
                {
                    fl = db.FL.Where(i => i.FIO == clientName).FirstOrDefault();
                    if (fl != null)
                        client = new FLModel(fl);
                    else
                    {
                        ul = db.UL.Where(i => i.organizationName == clientName).FirstOrDefault();
                        if (ul != null)
                            client = new ULModel(ul);
                    }
                }
                if (clientNumber != "")
                {
                    Client c = db.Client.Where(i => i.number == clientNumber).FirstOrDefault();
                    if (c != null)
                        if (c.FL != null)
                            client = new FLModel(c.userId);
                        else
                            client = new ULModel(c.userId);
                }
            }
            if (client.Id != 0)
            {
                Search(client.Id, from, till);
            }
        }

        private void Search(int clientId, DateTime from, DateTime till)
        {
            allRates = db.RateHistory
                .Where(i => i.clientId == clientId && (i.fromDate <= till && i.fromDate >= from && i.tillDate <= till && i.tillDate >= from || i.tillDate == null))
                .ToList()
                .Select(i => new RateHistoryModel(i))
                .ToList();
            allServices = db.ServiceHistory
                .Where(i => i.clientId == clientId && (i.fromDate <= till && i.fromDate >= from && i.tillDate <= till && i.tillDate >= from || i.tillDate == null))
                .ToList()
                .Select(i => new ServiceHistoryModel(i))
                .ToList();
        }
        public List<RateHistoryModel> AllRates
        {
            get { return allRates; }
        }
        public List<ServiceHistoryModel> AllServices
        {
            get { return allServices; }
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
