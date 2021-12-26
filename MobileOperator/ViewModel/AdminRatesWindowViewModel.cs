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
    class AdminRatesWindowViewModel : INotifyPropertyChanged
    {
        private RateListModel allRates = new RateListModel();
        public ObservableCollection<RateModel> Rates { get; set; }
        private Window window;

        public AdminRatesWindowViewModel(Window window)
        {
            this.window = window;
            Rates = new ObservableCollection<RateModel> { };
            foreach (RateModel rate in allRates.AllRates)
                Rates.Add(new RateModel()
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
                    Corporate = rate.Corporate
                });
        }

        private RelayCommand viewRateCommand;
        public RelayCommand ViewRateCommand
        {
            get
            {

                return viewRateCommand ??
                  (viewRateCommand = new RelayCommand(obj =>
                  {
                      RateModel selectRate = obj as RateModel;

                      if (selectRate != null)
                      {
                          AdminSelectedRateWindow rate = new AdminSelectedRateWindow(selectRate.Id);
                          rate.WindowState = window.WindowState;
                          rate.Top = window.Top;
                          rate.Left = window.Left;
                          rate.Height = window.Height;
                          rate.Width = window.Width;
                          rate.Show();
                          window.Close();
                      }
                  }));
            }
        }
        private RelayCommand addRateCommand;
        public RelayCommand AddRateCommand
        {
            get
            {
                return addRateCommand ??
                  (addRateCommand = new RelayCommand(obj =>
                  {
                      AdminSelectedRateWindow rate = new AdminSelectedRateWindow();
                      rate.WindowState = window.WindowState;
                      rate.Top = window.Top;
                      rate.Left = window.Left;
                      rate.Height = window.Height;
                      rate.Width = window.Width;
                      rate.Show();
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
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

}
