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
    public class ServiceModel : INotifyPropertyChanged
    {
        private Service service = new Service();
        DAL.MobileOperator db = new DAL.MobileOperator();

        public ServiceModel(Service s)
        {
            checkDB(db);
            service = s;
        }
        public ServiceModel(int id)
        {
            checkDB(db);
            service = db.Service.Find(id);
        }

        public ServiceModel() { checkDB(db); }

        public int Id
        {
            get { return service.id; }
            set { service.id = value; }
        }
        public string Name
        {
            get { return service.name; }
            set { service.name = value; }
        }

        public decimal Cost
        {
            get { if (service.cost != null) return (decimal)service.cost; else return 0; }
            set { service.cost = value; }
        }
        public decimal ConnectionCost
        {
            get { if (service.connectionCost != null) return (decimal)service.connectionCost; else return 0; }
            set { service.connectionCost = value; }
        }

        public string Conditions
        {
            get { return service.conditions; }
            set { service.conditions = value; }
        }

        public bool DisconnectService(int clientId)
        {
            ServiceHistory selectService = new ServiceHistory();
            selectService = db.ServiceHistory.Where(i => i.clientId == clientId && i.serviceId == service.id && i.tillDate == null).FirstOrDefault();
            if (selectService != null)
                selectService.tillDate = DateTime.Now;
            if (db.SaveChanges() != 0)
                return true;
            else return false;
        }

        public bool ConnectService(int clientId)
        {
            db.ServiceHistory.Add(new ServiceHistory() { clientId = clientId, serviceId = service.id, fromDate = DateTime.Now });
            if (db.SaveChanges() != 0)
                return true;
            else return false;
        }

        public bool Save()
        {
            Service s = db.Service.Find(Id);
            if (s != null)
            {
                s.conditions = this.Conditions;
                s.connectionCost = this.ConnectionCost;
                s.cost = this.Cost;
                s.name = this.Name;
            }
            else
            {
                s = new Service()
                {
                    conditions = this.Conditions,
                    connectionCost = this.ConnectionCost,
                    cost = this.Cost,
                    name = this.Name
                };
                db.Service.Add(s);
            }
            if (db.SaveChanges() != 0)
                return true;
            else return false;
        }

        public bool Remove()
        {
            Service s = db.Service.Find(Id);
            db.Service.Remove(s);

            List<ServiceHistory> serviceHistory;
            serviceHistory = db.ServiceHistory.Where(i => i.serviceId == s.id).ToList();
            foreach (ServiceHistory service in serviceHistory)
                db.ServiceHistory.Remove(service);

            if (db.SaveChanges() != 0)
                return true;
            else return false;
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
