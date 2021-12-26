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
    class AdminMainWindowViewModel : INotifyPropertyChanged
    {
        private ClientListModel allClients = new ClientListModel();
        private RateModel rate;
        private Window window;
        public ObservableCollection<ClientViewModel> Clients { get; set; }

        public AdminMainWindowViewModel(Window window)
        {
            this.window = window;
            Clients = new ObservableCollection<ClientViewModel> { };
            foreach (ClientModel client in allClients.AllClients)
            {
                rate = new RateModel(client.Rate);
                if (client.Status == 2)
                    Clients.Add(new ClientViewModel
                    {
                        OrganizationName = client.OrganizationName,
                        Address = client.Address,
                        Balance = client.Balance,
                        Status = client.Status,
                        GB = client.GB,
                        Id = client.Id,
                        Minutes = client.Minutes,
                        Number = client.Number,
                        Rate = client.Rate,
                        SMS = client.SMS,
                        RateName = rate.Name,
                    });
                else if (client.Status == 3)
                    Clients.Add(new ClientViewModel
                    {
                        FIO = client.FIO,
                        PasportDetales = client.PasportDetales,
                        Balance = client.Balance,
                        Status = client.Status,
                        GB = client.GB,
                        Id = client.Id,
                        Minutes = client.Minutes,
                        Number = client.Number,
                        Rate = client.Rate,
                        SMS = client.SMS,
                        RateName = rate.Name
                    });
            }
        }

        private RelayCommand viewClientCommand;
        public RelayCommand ViewClientCommand
        {
            get
            {
                return viewClientCommand ??
                  (viewClientCommand = new RelayCommand(obj =>
                  {
                      ClientViewModel selectClient = obj as ClientViewModel;

                      if (selectClient != null)
                      {
                          ViewClientWindow client = new ViewClientWindow(selectClient.Id, selectClient.Status);
                          client.WindowState = window.WindowState;
                          client.Top = window.Top;
                          client.Left = window.Left;
                          client.Height = window.Height;
                          client.Width = window.Width;
                          client.Show();
                          window.Close();
                      }
                  }));
            }
        }

        private RelayCommand addClientCommand;
        public RelayCommand AddClientCommand
        {
            get
            {
                return addClientCommand ??
                  (addClientCommand = new RelayCommand(obj =>
                  {
                      ViewClientWindow client = new ViewClientWindow();
                      client.WindowState = window.WindowState;
                      client.Top = window.Top;
                      client.Left = window.Left;
                      client.Height = window.Height;
                      client.Width = window.Width;
                      client.Show();
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
        private RelayCommand openAdminServicesWindow;
        public RelayCommand OpenAdminServicesWindow
        {
            get
            {
                return openAdminServicesWindow ??
                  (openAdminServicesWindow = new RelayCommand(obj =>
                  {
                      AdminServicesWindow services = new AdminServicesWindow();
                      services.WindowState = window.WindowState;
                      services.Top = window.Top;
                      services.Left = window.Left;
                      services.Height = window.Height;
                      services.Width = window.Width;
                      services.Show();
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

        private RelayCommand logOutCommand;
        public RelayCommand LogOutCommand
        {
            get
            {
                return logOutCommand ??
                  (logOutCommand = new RelayCommand(obj =>
                  {
                      Login login = new Login();
                      login.Show();
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
