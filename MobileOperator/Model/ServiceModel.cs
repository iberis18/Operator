using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DAL;

namespace MobileOperator.Model
{
    public class ServiceModel : INotifyPropertyChanged
    {
        private Service service = new Service();
        DAL.MobileOperator db = new DAL.MobileOperator();

        public ServiceModel(Service s)
        {
            service = s;
        }
        public ServiceModel(int id)
        {
            DAL.MobileOperator db = new DAL.MobileOperator();
            service = db.Service.Find(id);
        }

        public ServiceModel() { }

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
            get { return (decimal)service.cost; }
            set { service.cost = value; }
        }
        public decimal ConnectionCost
        {
            get { return (decimal)service.connectionCost; }
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
