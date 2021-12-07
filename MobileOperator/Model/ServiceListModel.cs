﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DAL;

namespace MobileOperator.Model
{
    class ServiceListModel : INotifyPropertyChanged
    {
        private List<ServiceModel> allServices;
        private List<ServiceHistory> clientServices;
        private DAL.MobileOperator db = new DAL.MobileOperator();
        public ServiceListModel()
        {
            allServices = db.Service.ToList().Select(i => new ServiceModel(i)).ToList();
        }

        public List<ServiceModel> AllServices
        {
            get { return allServices; }
        }

        public List<int> ClientServices(int clientId)
        {
            clientServices = db.ServiceHistory.Where(i => i.clientId == clientId && i.tillDate == null).ToList();
            List<int> clientServicesId = new List<int>();
            foreach (ServiceHistory service in clientServices)
                clientServicesId.Add(service.serviceId);
            return clientServicesId;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
