using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DAL;

namespace MobileOperator.Model
{
    public class RateModel : INotifyPropertyChanged
    {
        private Rate rate = new Rate();

        public RateModel(Rate r)
        {
            rate = r;
        }
        public RateModel(int id)
        {
            DAL.MobileOperator db = new DAL.MobileOperator();
            rate = db.Rate.Find(id);
        }

        public RateModel() { }

        public int Id
        {
            get { return rate.id; }
            set { rate.id = value; }
        }
        public string Name
        {
            get { return rate.name; }
            set
            {
                rate.name = value;
                OnPropertyChanged("RateName");
            }
        }
        public bool Corporate
        {
            get { return (bool)rate.corporate; }
            set { rate.corporate = value; }
        }
        public decimal ConnectionCost
        {
            get { return (decimal)rate.connectionCost; }
            set
            {
                rate.connectionCost = value;
                OnPropertyChanged("RateConnectionCost");
            }
        }
        public decimal Cost
        {
            get { return (decimal)rate.cost; }
            set
            {
                rate.cost = value;
                OnPropertyChanged("RateCost");
            }
        }
        public int Minutes
        {
            get { return (int)rate.minutes; }
            set
            {
                rate.minutes = value;
                OnPropertyChanged("RateMinutes");
            }
        }
        public int GB
        {
            get { return (int)rate.GB; }
            set
            {
                rate.GB = value;
                OnPropertyChanged("RateGB");
            }
        }
        public int SMS
        {
            get { return (int)rate.SMS; }
            set
            {
                rate.SMS = value;
                OnPropertyChanged("RateSMS");
            }
        }
        public decimal CityCost
        {
            get { return (decimal)rate.cityCost; }
            set
            {
                rate.cityCost = value;
                OnPropertyChanged("RateCityCost");
            }
        }
        public decimal IntercityCost
        {
            get { return (decimal)rate.intercityCost; }
            set
            {
                rate.intercityCost = value;
                OnPropertyChanged("RateIntercityCost");
            }
        }
        public decimal InternationalCost
        {
            get { return (decimal)rate.internationalCost; }
            set
            {
                rate.internationalCost = value;
                OnPropertyChanged("RateInternationalCoat");
            }
        }

        public decimal SMSCost
        {
            get { return (decimal)rate.SMSCost; }
            set
            {
                rate.SMSCost = value;
                OnPropertyChanged("RateSMSCost");
            }
        }
        public decimal GBCost
        {
            get { return (decimal)rate.GBCost; }
            set
            {
                rate.GBCost = value;
                OnPropertyChanged("RateGBCost");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
