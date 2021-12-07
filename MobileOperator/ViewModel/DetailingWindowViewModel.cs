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
    class DetailingWindowViewModel : INotifyPropertyChanged
    {
        private int userId, status;
        private ClientModel client;
        //private ServiceListModel allServices = new ServiceListModel();
        //public ObservableCollection<ServiceModel> Services { get; set; }

        public DetailingWindowViewModel(int userId, int status)
        {
            this.userId = userId;
            this.status = status;
            if (status == 2)
                client = new ULModel(userId);
            else
                client = new FLModel(userId);

            //Services = new ObservableCollection<ServiceModel> { };
            //foreach (int serviceId in allServices.ClientServices(client.Id))
            //    Services.Add(new ServiceModel(serviceId));
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
        private RelayCommand openServices;
        public RelayCommand OpenServices
        {
            get
            {
                return openServices ??
                  (openServices = new RelayCommand(obj =>
                  {
                      ServicesWindow services = new ServicesWindow(userId, status);
                      services.Show();
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
