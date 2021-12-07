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
        private RateListModel allRates = new RateListModel();
        public ObservableCollection<RateViewModel> Rates { get; set; }
        public ChangeRateWindowViewModel(int userId)
        {
            this.userId = userId;
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

}
