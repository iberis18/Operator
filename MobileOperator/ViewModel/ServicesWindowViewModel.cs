using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileOperator.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MobileOperator.ViewModel
{
    class ServicesWindowViewModel : INotifyPropertyChanged
    {
        private int userId, status;
        private ServiceListModel services = new ServiceListModel();
        private ClientModel client;
        public ObservableCollection<ServiceViewModel> ConnectionServices { get; set; }
        public ObservableCollection<ServiceViewModel> AvailableServices { get; set; }

        public ServicesWindowViewModel(int userId, int status)
        {
            this.userId = userId;
            this.status = status;
            ConnectionServices = new ObservableCollection<ServiceViewModel> { };
            AvailableServices = new ObservableCollection<ServiceViewModel> { };
            client = new ClientModel(userId);

            foreach (int serviseId in services.ClientServices(userId))
                ConnectionServices.Add(new ServiceViewModel(serviseId) { ClientId = userId, ServicesList = this });

            bool flag;
            foreach (ServiceModel service in services.AllServices)
            {
                flag = false;
                foreach (int serviseId in services.ClientServices(userId))
                    if (service.Id == serviseId)
                        flag = true;
                if (!flag)
                    AvailableServices.Add(new ServiceViewModel()
                    {
                        Name = service.Name,
                        Cost = service.Cost,
                        ConnectionCost = service.ConnectionCost,
                        Conditions = service.Conditions,
                        Id = service.Id,
                        ClientId = userId,
                        ServicesList = this
                    });
            }
        }

        private RelayCommand changeRateCommand;
        public RelayCommand ChangeRateCommand
        {
            get
            {
                return changeRateCommand ??
                  (changeRateCommand = new RelayCommand(obj =>
                  {
                      ChangeRateWindow changeRate = new ChangeRateWindow(userId);
                      changeRate.Show();
                  }));
            }
        }

        private RelayCommand openMainWindow;
        public RelayCommand OpenMainWindow
        {
            get
            {
                return openMainWindow ??
                  (openMainWindow = new RelayCommand(obj =>
                  {
                      MainWindow main = new MainWindow(userId, status);
                      main.Show();
                  }));
            }
        }
        private RelayCommand openRates;
        public RelayCommand OpenRates
        {
            get
            {
                return openRates ??
                  (openRates = new RelayCommand(obj =>
                  {
                      RatesWindow rates = new RatesWindow(userId, status);
                      rates.Show();
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
