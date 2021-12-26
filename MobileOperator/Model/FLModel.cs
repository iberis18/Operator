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
    public class FLModel : ClientModel
    {
        private FL fl = new FL();
        public FLModel() : base() { }
        public FLModel(User u, Client c, FL f) : base(u, c)
        {
            fl = f;
        }
        public FLModel(int id) : base(id)
        {
            fl = db.FL.Where(i => i.userId == id).FirstOrDefault();
        }
        public FLModel(FL f) : base(f.userId)
        {
            fl = f;
        }

        override public string FIO
        {
            get { return fl.FIO; }
            set { fl.FIO = value; }
        }
        override public string PasportDetales
        {
            get { return fl.pasportDetales; }
            set { fl.pasportDetales = value; }
        }
        override public bool Save()
        {
            FL fl = db.FL.Find(Id);
            Client c = db.Client.Find(Id);
            if (fl != null && c != null)
            {
                c.balance = Balance;
                c.rate = Rate;
                c.minutes = Minutes;
                c.number = Number;
                c.SMS = SMS;
                fl.FIO = this.FIO;
                fl.pasportDetales = this.PasportDetales;

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
                if (db.SaveChanges() == 0)
                    return false;
                c = new Client()
                {
                    balance = this.Balance,
                    rate = this.Rate,
                    minutes = this.Minutes,
                    number = this.Number,
                    SMS = this.SMS,
                    GB = this.GB,
                    userId = u.id,
                };
                db.Client.Add(c);
                if (db.SaveChanges() == 0)
                    return false;
                FL newfl = new FL()
                {
                    FIO = this.FIO,
                    pasportDetales = this.PasportDetales,
                    userId = u.id
                };
                db.FL.Add(newfl);
                this.Id = u.id;
                this.Rate = (int)c.rate;
                return AddRateHistory();
            }
        }
        public override bool Remove()
        {
            FL f = db.FL.Find(this.Id);
            db.FL.Remove(f);
            Client c = db.Client.Find(this.Id);

            List<RateHistory> rateHistory;
            rateHistory = db.RateHistory.Where(i => i.clientId == c.userId).ToList();
            foreach (RateHistory r in rateHistory)
                db.RateHistory.Remove(r);
            List<ServiceHistory> serviceHistory;
            serviceHistory = db.ServiceHistory.Where(i => i.clientId == c.userId).ToList();
            foreach (ServiceHistory s in serviceHistory)
                db.ServiceHistory.Remove(s);
            List<Call> calls;
            calls = db.Call.Where(i => i.callerId == c.userId).ToList();
            foreach (Call call in calls)
                db.Call.Remove(call);
            calls = db.Call.Where(i => i.calledId == c.userId).ToList();
            foreach (Call call in calls)
                db.Call.Remove(call);

            db.Client.Remove(c);
            User u = db.User.Find(this.Id);
            db.User.Remove(u);
            if (db.SaveChanges() != 0)
                return true;
            else return false;
        }
    }
}
