using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DAL;
using System.Data.Entity;

namespace MobileOperator.Model
{
    public class ClientModel : UserModel
    {
        private DAL.MobileOperator db = new DAL.MobileOperator();
        private Client client = new Client();
        public ClientModel() : base() { }
        public ClientModel(User u, Client c) : base(u)
        {
            client = c;
        }
        public ClientModel(int id) : base(id)
        {
            client = db.Client.Find(id);
        }

        override public string Number
        {
            get { return client.number; }
            set
            {
                client.number = value;
                //OnPropertyChanged("Number");
            }
        }
        public decimal Balance
        {
            get { return (decimal)client.balance; }
            set
            {
                client.balance = value;
                //OnPropertyChanged("Balance");
            }
        }
        public int Minutes
        {
            get { return (int)client.minutes; }
            set
            {
                client.minutes = value;
                //OnPropertyChanged("Minutes");
            }
        }
        public int SMS
        {
            get { return (int)client.SMS; }
            set
            {
                client.SMS = value;
                //OnPropertyChanged("SMS");
            }
        }
        public int GB
        {
            get { return (int)client.GB; }
            set
            {
                client.GB = value;
                //OnPropertyChanged("GB");
            }
        }
        public int Rate
        {
            get { return (int)client.rate; }
            set
            {
                client.rate = value;
                //OnPropertyChanged("Rate");
            }
        }
        public virtual string OrganizationName { get; set; }
        public virtual string FIO { get; set; }
        public virtual string PasportDetales { get; set; }
        public virtual string Address { get; set; }
        public bool AddRateHistory()
        {
            db.RateHistory.Add(new DAL.RateHistory() { clientId = client.userId, rateId = (int)client.rate, fromDate = DateTime.Now });
            RateHistory rateHistory = new RateHistory();
            rateHistory = db.RateHistory.Where(i => i.clientId == Id && i.tillDate == null).FirstOrDefault();
            if (rateHistory != null)
                rateHistory.tillDate = DateTime.Now;
            if (db.SaveChanges() != 0)
                return true;
            else return false;
        }
        override public bool Save()
        {
            //User u =  db.User.Find(Id);
            //u = user;
            Client c = db.Client.Find(Id);
            c.balance = client.balance;
            c.rate = client.rate;
            if (db.SaveChanges() != 0)
                return true;
            else return false;
        }
    }
}
