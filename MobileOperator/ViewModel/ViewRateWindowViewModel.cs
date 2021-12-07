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
        public ViewRateWindowViewModel(int userId, int rateId)
        {
            this.rateId = rateId;
            this.userId = userId;
            rate = new RateModel(rateId);
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
                      client = new ClientModel(userId);
                      //if (client.Status == 2)
                      //    client = new ULModel(userId);
                      //else
                      //    client = new FLModel(userId);

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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

}
