using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DAL;
using System.Data.Entity;
using System.Windows;

namespace MobileOperator.Model
{
    public class ServiceHistoryModel
    {
        private ServiceHistory service = new ServiceHistory();
        private DAL.MobileOperator db = new DAL.MobileOperator();
        public ServiceHistoryModel() { checkDB(db); }
        public ServiceHistoryModel(ServiceHistory s)
        {
            checkDB(db);
            service = s;
        }
        public ServiceHistoryModel(int id)
        {
            checkDB(db);
            service = db.ServiceHistory.Find(id);
        }

        public int Id
        {
            get { return service.id; }
            set { service.id = value; }
        }
        public int ClientId
        {
            get { return service.clientId; }
            set { service.clientId = value; }
        }
        public int ServiceId
        {
            get { return service.serviceId; }
            set { service.serviceId = value; }
        }
        public string ServiceName
        {
            get
            {
                DAL.Service serviceName = db.Service.Find(service.serviceId);
                return serviceName.name;
            }
            set { }
        }
        public DateTime FromDate
        {
            get { return service.fromDate; }
            set { service.fromDate = value; }
        }
        public string FromDateToString
        {
            get { return service.fromDate.ToString("G"); }
            set { }
        }
        public DateTime TillDate
        {
            get { return (DateTime)service.tillDate; }
            set { service.tillDate = value; }
        }
        public string TillDateToString
        {
            get { if (service.tillDate != null) return ((DateTime)service.tillDate).ToString("G"); else return ""; }
            set { }
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
