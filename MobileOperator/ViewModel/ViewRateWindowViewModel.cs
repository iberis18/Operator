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
    class ViewRateWindowViewModel : INotifyPropertyChanged
    {
        private int rateId, userId;
        private RateModel rate;
        private ClientModel client;
        Window window;
        public ViewRateWindowViewModel(int userId, int rateId, Window window)
        {
            this.rateId = rateId;
            this.userId = userId;
            this.window = window;
            rate = new RateModel(rateId);
            client = new ClientModel(userId);
        }
        public String Rate
        {
            get { return rate.Name; }
            set { rate.Name = value; }
        }
        public decimal Cost
        {
            get { return rate.Cost; }
            set { rate.Cost = value; }
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
        public int GB
        {
            get { return rate.GB; }
            set { rate.GB = value; }
        }
        public int SMS
        {
            get { return rate.SMS; }
            set { rate.SMS = value; }
        }
        public int Minutes
        {
            get { return rate.Minutes; }
            set { rate.Minutes = value; }
        }
        public decimal ConnectionCost
        {
            get { return rate.ConnectionCost; }
            set { rate.ConnectionCost = value; }
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

        private RelayCommand changeRateCommand;
        public RelayCommand ChangeRateCommand
        {
            get
            {
                return changeRateCommand ??
                  (changeRateCommand = new RelayCommand(obj =>
                  {
                      if (client.Rate == rateId)
                          MessageBox.Show("Вы уже подключены к данному тарифу!");
                      else
                      {
                          if (client.Balance < rate.ConnectionCost + rate.Cost)
                              MessageBox.Show("На вашем счету недостаточно средств для подключения тарифа!");
                          else
                          {
                              client.Balance -= (rate.ConnectionCost + rate.Cost);
                              client.Rate = rate.Id;
                              if (client.AddRateHistory())
                                  MessageBox.Show("Подключение прошло успешно!");
                              else
                                  MessageBox.Show("Произошла ошибка, попроюуйте еще раз!");
                          }
                      }
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
