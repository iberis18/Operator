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
        private Window window;
        public ObservableCollection<ServiceViewModel> ConnectionServices { get; set; }
        public ObservableCollection<ServiceViewModel> AvailableServices { get; set; }

        public ServicesWindowViewModel(int userId, int status, Window window)
        {
            this.userId = userId;
            this.status = status;
            this.window = window;
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
                      changeRate.WindowState = window.WindowState;
                      changeRate.Top = window.Top;
                      changeRate.Left = window.Left;
                      changeRate.Height = window.Height;
                      changeRate.Width = window.Width;
                      changeRate.Show();
                      window.Close();
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
                      main.WindowState = window.WindowState;
                      main.Top = window.Top;
                      main.Left = window.Left;
                      main.Height = window.Height;
                      main.Width = window.Width;
                      main.Show();
                      window.Close();
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
                      rates.WindowState = window.WindowState;
                      rates.Top = window.Top;
                      rates.Left = window.Left;
                      rates.Height = window.Height;
                      rates.Width = window.Width;
                      rates.Show();
                      window.Close();
                  }));
            }
        }
        private RelayCommand openDetailing;
        public RelayCommand OpenDetailing
        {
            get
            {
                return openDetailing ??
                  (openDetailing = new RelayCommand(obj =>
                  {
                      DetailingWindow detailing = new DetailingWindow(userId, status);
                      detailing.WindowState = window.WindowState;
                      detailing.Top = window.Top;
                      detailing.Left = window.Left;
                      detailing.Height = window.Height;
                      detailing.Width = window.Width;
                      detailing.Show();
                      window.Close();
                  }));
            }
        }
        private RelayCommand openDetailing2;
        public RelayCommand OpenDetailing2
        {
            get
            {
                return openDetailing2 ??
                  (openDetailing2 = new RelayCommand(obj =>
                  {
                      DetailingWindow2 detailing = new DetailingWindow2(userId, status);
                      detailing.WindowState = window.WindowState;
                      detailing.Top = window.Top;
                      detailing.Left = window.Left;
                      detailing.Height = window.Height;
                      detailing.Width = window.Width;
                      detailing.Show();
                      window.Close();
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
