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
    class ChangeRateWindowViewModel : INotifyPropertyChanged
    {
        private int userId;
        private ClientModel client;
        private Window window;
        private RateListModel allRates = new RateListModel();
        public ObservableCollection<RateViewModel> Rates { get; set; }
        public ChangeRateWindowViewModel(int userId, Window window)
        {
            this.userId = userId;
            this.window = window;
            client = new ClientModel(userId);
            Rates = new ObservableCollection<RateViewModel> { };
            if (client.Status == 2)
                foreach (RateModel rate in allRates.AllCorporateRates)
                    Rates.Add(new RateViewModel()
                    {
                        Name = rate.Name,
                        ConnectionCost = rate.ConnectionCost,
                        Minutes = rate.Minutes,
                        GB = rate.GB,
                        SMS = rate.SMS,
                        Cost = rate.Cost,
                        Id = rate.Id,
                        CityCost = rate.CityCost,
                        IntercityCost = rate.IntercityCost,
                        InternationalCost = rate.InternationalCost,
                        SMSCost = rate.SMSCost,
                        GBCost = rate.GBCost,
                        Corporate = rate.Corporate,
                        ClientId = userId
                    });
            else if (client.Status == 3)
                foreach (RateModel rate in allRates.AllNotCorporateRates)
                    Rates.Add(new RateViewModel()
                    {
                        Name = rate.Name,
                        ConnectionCost = rate.ConnectionCost,
                        Minutes = rate.Minutes,
                        GB = rate.GB,
                        SMS = rate.SMS,
                        Cost = rate.Cost,
                        Id = rate.Id,
                        CityCost = rate.CityCost,
                        IntercityCost = rate.IntercityCost,
                        InternationalCost = rate.InternationalCost,
                        SMSCost = rate.SMSCost,
                        GBCost = rate.GBCost,
                        Corporate = rate.Corporate,
                        ClientId = userId
                    });
        }
        private RelayCommand openMainWindow;
        public RelayCommand OpenMainWindow
        {
            get
            {

                return openMainWindow ??
                  (openMainWindow = new RelayCommand(obj =>
                  {
                      MainWindow main = new MainWindow(userId, client.Status);
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
                      RatesWindow rates = new RatesWindow(userId, client.Status);
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
        private RelayCommand openServices;
        public RelayCommand OpenServices
        {
            get
            {
                return openServices ??
                  (openServices = new RelayCommand(obj =>
                  {
                      ServicesWindow services = new ServicesWindow(userId, client.Status);
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
        private RelayCommand openDetailing;
        public RelayCommand OpenDetailing
        {
            get
            {
                return openDetailing ??
                  (openDetailing = new RelayCommand(obj =>
                  {
                      DetailingWindow detailing = new DetailingWindow(userId, client.Status);
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
                      DetailingWindow2 detailing = new DetailingWindow2(userId, client.Status);
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
