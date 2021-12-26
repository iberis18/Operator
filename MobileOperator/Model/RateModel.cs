using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DAL;
using System.Windows;

namespace MobileOperator.Model
{
    public class RateModel : INotifyPropertyChanged
    {
        private Rate rate = new Rate();
        protected DAL.MobileOperator db = new DAL.MobileOperator();

        public RateModel(Rate r)
        {
            checkDB(db);
            rate = r;
        }
        public RateModel(int id)
        {
            checkDB(db);
            rate = db.Rate.Find(id);
        }

        public RateModel() { checkDB(db); }

        public int Id
        {
            get { return rate.id; }
            set { rate.id = value; }
        }
        public string Name
        {
            get { if (rate.name != null) return rate.name; else return ""; }
            set { rate.name = value; }
        }
        public bool Corporate
        {
            get { if (rate.corporate != null) return (bool)rate.corporate; else return false; }
            set { rate.corporate = value; }
        }
        public decimal ConnectionCost
        {
            get { if (rate.connectionCost != null) return (decimal)rate.connectionCost; else return 0; }
            set { rate.connectionCost = value; }
        }
        public decimal Cost
        {
            get { if (rate.cost != null) return (decimal)rate.cost; else return 0; }
            set { rate.cost = value; }
        }
        public int Minutes
        {
            get { if (rate.minutes != null) return (int)rate.minutes; else return 0; }
            set { rate.minutes = value; }
        }
        public int GB
        {
            get { if (rate.GB != null) return (int)rate.GB; else return 0; }
            set { rate.GB = value; }
        }
        public int SMS
        {
            get { if (rate.SMS != null) return (int)rate.SMS; else return 0; }
            set { rate.SMS = value; }
        }
        public decimal CityCost
        {
            get { if (rate.cityCost != null) return (decimal)rate.cityCost; else return 0; }
            set { rate.cityCost = value; }
        }
        public decimal IntercityCost
        {
            get { if (rate.intercityCost != null) return (decimal)rate.intercityCost; else return 0; }
            set { rate.intercityCost = value; }
        }
        public decimal InternationalCost
        {
            get
            {
                if (rate.internationalCost != null)
                    return (decimal)rate.internationalCost;
                else
                    return 0;
            }
            set { rate.internationalCost = value; }
        }

        public decimal SMSCost
        {
            get { if (rate.SMSCost != null) return (decimal)rate.SMSCost; else return 0; }
            set { rate.SMSCost = value; }
        }
        public decimal GBCost
        {
            get { if (rate.GBCost != null) return (decimal)rate.GBCost; else return 0; }
            set { rate.GBCost = value; }
        }
        public bool Save()
        {
            Rate r = db.Rate.Find(Id);
            if (r != null)
            {
                r.cityCost = this.CityCost;
                r.connectionCost = this.ConnectionCost;
                r.corporate = this.Corporate;
                r.cost = this.Cost;
                r.GB = this.GB;
                r.GBCost = this.GBCost;
                r.intercityCost = this.IntercityCost;
                r.internationalCost = this.InternationalCost;
                r.minutes = this.Minutes;
                r.name = this.Name;
                r.SMS = this.SMS;
                r.SMSCost = this.SMSCost;
            }
            else
            {
                r = new Rate()
                {
                    cityCost = this.CityCost,
                    connectionCost = this.ConnectionCost,
                    corporate = this.Corporate,
                    cost = this.Cost,
                    GB = this.GB,
                    GBCost = this.GBCost,
                    intercityCost = this.IntercityCost,
                    internationalCost = this.InternationalCost,
                    minutes = this.Minutes,
                    name = this.Name,
                    SMS = this.SMS,
                    SMSCost = this.SMSCost
                };
                db.Rate.Add(r);
            }
            if (db.SaveChanges() != 0)
                return true;
            else return false;
        }

        public bool Remove()
        {
            Rate r = db.Rate.Find(Id);
            List<RateHistory> rateHistory;
            rateHistory = db.RateHistory.Where(i => i.rateId == r.id).ToList();
            foreach (RateHistory rate in rateHistory)
                db.RateHistory.Remove(rate);
            List<Client> clients;
            clients = db.Client.Where(i => i.rate == r.id).ToList();
            foreach (Client client in clients)
                db.Client.Remove(client);
            db.Rate.Remove(r);

            if (db.SaveChanges() != 0)
                return true;
            else return false;
        }

        private void DBException()
        {
            MessageBox.Show("Ошибка подключения к базе, приложение будет закрыто", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            Environment.Exit(0);
        }

        private void checkDB(DAL.MobileOperator db)
        {
            try
            {
                if (!db.Database.Exists())
                {
                    DBException();
                }
            }
            catch (System.InvalidOperationException)
            {
                DBException();
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
