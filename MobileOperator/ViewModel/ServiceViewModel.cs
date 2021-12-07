using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MobileOperator.Model;
using System.Windows;

namespace MobileOperator.ViewModel
{
    class ServiceViewModel : INotifyPropertyChanged
    {
        private ServiceModel service = new ServiceModel();
        public ServiceViewModel() { }
        public ServiceViewModel(ServiceModel s)
        {
            service = s;
        }
        public ServiceViewModel(int id)
        {
            service = new ServiceModel(id);
        }
        private ServicesWindowViewModel servicesList;
        public ServicesWindowViewModel ServicesList
        {
            get { return servicesList; }
            set { servicesList = value; }
        }
        private int clientId;
        public int ClientId
        {
            get { return clientId; }
            set { clientId = value; }
        }
        public int Id
        {
            get { return service.Id; }
            set { service.Id = value; }
        }
        public string Name
        {
            get { return service.Name; }
            set { service.Name = value; }
        }

        public decimal Cost
        {
            get { return (decimal)service.Cost; }
            set { service.Cost = value; }
        }
        public decimal ConnectionCost
        {
            get { return (decimal)service.ConnectionCost; }
            set { service.ConnectionCost = value; }
        }

        public string Conditions
        {
            get { return service.Conditions; }
            set { service.Conditions = value; }
        }

        private RelayCommand connectServiceCommand;
        public RelayCommand ConnectServiceCommand
        {
            get
            {
                return connectServiceCommand ??
                  (connectServiceCommand = new RelayCommand(obj =>
                  {
                      int? idService = obj as int?;
                      ClientModel client = new ClientModel(clientId);
                      if (idService != null)
                      {
                          ServiceModel selelectService = new ServiceModel((int)idService);
                          if (client.Balance < selelectService.ConnectionCost + selelectService.Cost)
                              MessageBox.Show("На вашем счету недостаточно средств для подключения услуги!");
                          else
                          {
                              client.Balance -= (selelectService.Cost + selelectService.ConnectionCost);
                              if (selelectService.ConnectService(clientId) && client.Save())
                              {
                                  servicesList.ConnectionServices.Add(this);
                                  servicesList.AvailableServices.Remove(this);
                                  MessageBox.Show("Подключение прошло успешно!");
                                  OnPropertyChanged("ConnectionServices");
                                  OnPropertyChanged("AvailableServices");
                              }
                              else
                                  MessageBox.Show("Произошла ошибка, попроюуйте еще раз!");
                          }
                      }
                  }));
            }

        }

        private RelayCommand disconnectServiceCommand;
        public RelayCommand DisconnectServiceCommand
        {
            get
            {
                return disconnectServiceCommand ??
                  (disconnectServiceCommand = new RelayCommand(obj =>
                  {
                      int? idService = obj as int?;
                      ClientModel client = new ClientModel(clientId);
                      if (idService != null)
                      {
                          ServiceModel selelectService = new ServiceModel((int)idService);
                          if (selelectService.DisconnectService(clientId))
                          {
                              servicesList.ConnectionServices.Remove(this);
                              servicesList.AvailableServices.Add(this);
                              OnPropertyChanged("ConnectionServices");
                              OnPropertyChanged("AvailableServices");
                              MessageBox.Show("Отключение прошло успешно!");
                          }
                          else
                              MessageBox.Show("Произошла ошибка, попроюуйте еще раз!");
                      }
                  }));
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
