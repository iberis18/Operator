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
    class AppViewModel : INotifyPropertyChanged
    {
        private int userId, status;
        private ClientModel client;
        private RateModel rate;
        private ServiceListModel allServices = new ServiceListModel();
        public ObservableCollection<ServiceModel> Services { get; set; }

        public AppViewModel(int userId, int status)
        {
            this.userId = userId;
            this.status = status;
            if (status == 2)
                client = new ULModel(userId);
            else
                client = new FLModel(userId);
            rate = new RateModel(client.Rate);

            Services = new ObservableCollection<ServiceModel> { };
            foreach (int serviceId in allServices.ClientServices(client.Id))
                Services.Add(new ServiceModel(serviceId));
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
            get { return client.Minutes; }
            set { rate.Minutes = value; }
        }
        public int SMS
        {
            get { return client.SMS; }
            set { rate.SMS = value; }
        }
        public int GB
        {
            get { return client.GB; }
            set { client.GB = value; }
        }
        public decimal Balance
        {
            get { return client.Balance; }
            set { client.Balance = value; }
        }
        decimal amount = 0.00m;
        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        public string ClientName
        {
            get
            {
                if (client.Status == 2)
                    return client.OrganizationName;
                else
                    return client.FIO;
            }
            set { }
        }
        public string ClientNumber
        {
            get { return client.Number; }
            set { }
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

        private RelayCommand payCommand;
        public RelayCommand PayCommand
        {
            get
            {
                return payCommand ??
                  (payCommand = new RelayCommand(obj =>
                  {
                      decimal balance = client.Balance;
                      balance += amount;
                      client.Balance = balance;
                      OnPropertyChanged("Balance");
                      if (client.Save())
                          MessageBox.Show("Баланс успешно поплнен!");
                      else
                          MessageBox.Show("Произошла ошибка, попробуйте еще раз!");
                      Amount = 0.00m;
                      OnPropertyChanged("Amount");
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
