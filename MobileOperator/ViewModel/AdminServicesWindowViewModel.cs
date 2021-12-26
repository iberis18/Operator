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
    class AdminServicesWindowViewModel : INotifyPropertyChanged
    {
        private ServiceListModel allServises = new ServiceListModel();
        public ObservableCollection<ServiceModel> Services { get; set; }
        private Window window;

        public AdminServicesWindowViewModel(Window window)
        {
            this.window = window;
            Services = new ObservableCollection<ServiceModel> { };
            foreach (ServiceModel service in allServises.AllServices)
                Services.Add(new ServiceModel()
                {
                    Name = service.Name,
                    Conditions = service.Conditions,
                    ConnectionCost = service.ConnectionCost,
                    Cost = service.Cost,
                    Id = service.Id
                });
        }

        private RelayCommand viewServiceCommand;
        public RelayCommand ViewServiceCommand
        {
            get
            {
                return viewServiceCommand ??
                  (viewServiceCommand = new RelayCommand(obj =>
                  {
                      ServiceModel selectedService = obj as ServiceModel;

                      if (selectedService != null)
                      {
                          AdminSelectedServiceWindow service = new AdminSelectedServiceWindow(selectedService.Id);
                          service.WindowState = window.WindowState;
                          service.Top = window.Top;
                          service.Left = window.Left;
                          service.Height = window.Height;
                          service.Width = window.Width;
                          service.Show();
                          window.Close();
                      }
                  }));
            }
        }
        private RelayCommand addServiceCommand;
        public RelayCommand AddServiceCommand
        {
            get
            {
                return addServiceCommand ??
                  (addServiceCommand = new RelayCommand(obj =>
                  {
                      AdminSelectedServiceWindow service = new AdminSelectedServiceWindow();
                      service.WindowState = window.WindowState;
                      service.Top = window.Top;
                      service.Left = window.Left;
                      service.Height = window.Height;
                      service.Width = window.Width;
                      service.Show();
                      window.Close();
                  }));
            }
        }

        private RelayCommand openAdminMainWindow;
        public RelayCommand OpenAdminMainWindow
        {
            get
            {
                return openAdminMainWindow ??
                  (openAdminMainWindow = new RelayCommand(obj =>
                  {
                      AdminMainWindow main = new AdminMainWindow();
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
        private RelayCommand openAdminRatesWindow;
        public RelayCommand OpenAdminRatesWindow
        {
            get
            {
                return openAdminRatesWindow ??
                  (openAdminRatesWindow = new RelayCommand(obj =>
                  {
                      AdminRatesWindow rates = new AdminRatesWindow();
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

        private RelayCommand openAdminDetailWindow;
        public RelayCommand OpenAdminDetailWindow
        {
            get
            {
                return openAdminDetailWindow ??
                  (openAdminDetailWindow = new RelayCommand(obj =>
                  {
                      AdminDetailingWindow detailing = new AdminDetailingWindow();
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
        private RelayCommand openAdminDetailWindow2;
        public RelayCommand OpenAdminDetailWindow2
        {
            get
            {
                return openAdminDetailWindow2 ??
                  (openAdminDetailWindow2 = new RelayCommand(obj =>
                  {
                      AdminDetailingWindow2 detailing = new AdminDetailingWindow2();
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
