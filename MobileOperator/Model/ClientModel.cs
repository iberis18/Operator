using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DAL;
using System.Data.Entity;
using System.Windows;

namespace MobileOperator.Model
{
    public class ClientModel : UserModel
    {
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

        public ClientModel(Client c) : base(c.userId)
        {
            client = c;
        }

        override public string Number
        {
            get { return client.number; }
            set
            { client.number = value; }
        }
        public decimal Balance
        {
            get { return (decimal)client.balance; }
            set { client.balance = value; }
        }
        public int Minutes
        {
            get { return (int)client.minutes; }
            set { client.minutes = value; }
        }
        public int SMS
        {
            get { return (int)client.SMS; }
            set { client.SMS = value; }
        }
        public int GB
        {
            get { return (int)client.GB; }
            set { client.GB = value; }
        }
        public int Rate
        {
            get { return (int)client.rate; }
            set { client.rate = value; }
        }

        public virtual string OrganizationName { get; set; }
        public virtual string FIO { get; set; }
        public virtual string PasportDetales { get; set; }
        public virtual string Address { get; set; }
        public bool AddRateHistory()
        {
            RateHistory newRateHistory = new RateHistory { clientId = Id, rateId = Rate, fromDate = DateTime.Now };
            db.RateHistory.Add(newRateHistory);
            RateHistory oldRateHistory = new RateHistory();
            oldRateHistory = db.RateHistory.Where(i => i.clientId == Id && i.tillDate == null).FirstOrDefault();
            if (oldRateHistory != null)
                oldRateHistory.tillDate = DateTime.Now;
            if (db.SaveChanges() != 0)
                return true;
            else return false;
        }

        override public bool Save()
        {
            Client c = db.Client.Find(Id);
            if (c != null)
            {
                c.balance = client.balance;
                c.rate = client.rate;
                c.minutes = client.minutes;
                c.number = client.number;
                c.SMS = client.SMS;
                c.GB = client.GB;

                if (db.SaveChanges() != 0)
                    return true;
                else return false;
            }
            else
            {
                User u = new User()
                {
                    password = this.Password
                };
                db.User.Add(u);
                db.SaveChanges();
                u = db.User.Where(i => i.password == this.Password).FirstOrDefault();
                c = new Client()
                {
                    balance = client.balance,
                    rate = client.rate,
                    minutes = client.minutes,
                    number = client.number,
                    SMS = client.SMS,
                    GB = client.GB,
                    userId = u.id
                };
                db.Client.Add(c);
                return AddRateHistory();
            }
        }

        public override bool Remove()
        {
            Client c = db.Client.Find(this.Id);
            db.Client.Remove(c);
            User u = db.User.Find(this.Id);
            db.User.Remove(u);
            if (db.SaveChanges() != 0)
                return true;
            else return false;
        }
    }
}
