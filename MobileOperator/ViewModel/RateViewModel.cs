using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MobileOperator.Model;
using System.Windows;

namespace MobileOperator.ViewModel
{
    class RateViewModel : INotifyPropertyChanged
    {
        private RateModel rate = new RateModel();
        public RateViewModel() { }
        public RateViewModel(RateModel r)
        {
            rate = r;
        }
        public RateViewModel(int id)
        {
            rate = new RateModel(id);
        }
        public string Name
        {
            get { return rate.Name; }
            set { rate.Name = value; }
        }
        public int Id
        {
            get { return rate.Id; }
            set { rate.Id = value; }
        }
        public bool Corporate
        {
            get { return rate.Corporate; }
            set { rate.Corporate = value; }
        }
        public decimal ConnectionCost
        {
            get { return rate.ConnectionCost; }
            set { rate.ConnectionCost = value; }
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
        private int clientId;
        public int ClientId
        {
            get { return clientId; }
            set { clientId = value; }
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

        private RelayCommand changeRateCommand;
        public RelayCommand ChangeRateCommand
        {
            get
            {
                return changeRateCommand ??
                  (changeRateCommand = new RelayCommand(obj =>
                  {
                      int? rateId = obj as int?;
                      ClientModel client = new ClientModel(clientId);
                      if (rateId != null)
                      {
                          if (client.Rate == (int)rateId)
                              MessageBox.Show("Вы уже подключены к данному тарифу!");
                          else
                          {
                              RateModel selectedRate = new RateModel((int)rateId);
                              if (client.Balance < selectedRate.ConnectionCost + selectedRate.Cost)
                                  MessageBox.Show("На вашем счету недостаточно средств для подключения тарифа!");
                              else
                              {
                                  client.Balance -= (selectedRate.ConnectionCost + selectedRate.Cost);
                                  client.Rate = selectedRate.Id;
                                  if (client.AddRateHistory())
                                      MessageBox.Show("Подключение прошло успешно!");
                                  else
                                      MessageBox.Show("Произошла ошибка, попроюуйте еще раз!");
                              }
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
