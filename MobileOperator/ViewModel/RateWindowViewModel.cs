using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileOperator.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MobileOperator.ViewModel
{
    class RateWindowViewModel : INotifyPropertyChanged
    {
        private int userId, status;
        private RateModel rate;
        private ClientModel client;
        private RateListModel allRates = new RateListModel();
        public ObservableCollection<RateModel> Rates { get; set; }

        public RateWindowViewModel(int userId, int status)
        {
            this.userId = userId;
            this.status = status;
            Rates = new ObservableCollection<RateModel> { };
            if (status == 2)
                foreach (RateModel rate in allRates.AllCorporateRates)
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
            else if (status == 3)
                foreach (RateModel rate in allRates.AllNotCorporateRates)
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
            client = new ClientModel(userId);
            rate = new RateModel(client.Rate);
        }
        public string Rate
        {
            get { return rate.Name; }
            set { rate.Name = value; }
        }
        public decimal Cost
        {
            get { return rate.Cost; }
            set { rate.Cost = value; }
        }
        public int Minutes
        {
            get { return rate.Minutes; }
            set { rate.Minutes = value; }
        }
        public int SMS
        {
            get { return rate.SMS; }
            set { rate.SMS = value; }
        }
        public int GB
        {
            get { return rate.GB; }
            set { rate.GB = value; }
        }
        public decimal CityCost
        {
            get { return rate.CityCost; }
            set { rate.CityCost = value; }
        }
        public decimal IntercityCost
        {
            get { return rate.IntercityCost; }
            set { rate.IntercityCost = value; }
        }
        public decimal InternationalCost
        {
            get { return rate.InternationalCost; }
            set { rate.InternationalCost = value; }
        }
        public decimal SMSCost
        {
            get { return rate.SMSCost; }
            set { rate.SMSCost = value; }
        }
        public decimal GBCost
        {
            get { return rate.GBCost; }
            set { rate.GBCost = value; }
        }
        public string Corporate
        {
            get
            {
                if (rate.Corporate == true)
                    return "Корпоративный";
                else return "Некорпоративный";
            }
            set
            {
                if (value == "Корпоративный")
                    rate.Corporate = true;
                else if (value == "Некорпоративный")
                    rate.Corporate = false;
            }
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
                          ViewRateWindow rate = new ViewRateWindow(userId, selectRate.Id);
                          rate.Show();
                      }
                  }));
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

}
