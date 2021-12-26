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
    public class ULModel : ClientModel
    {
        private UL ul = new UL();
        public ULModel() : base() { }
        public ULModel(User u, Client c, UL ul) : base(u, c) { this.ul = ul; }
        public ULModel(int id) : base(id)
        {
            DAL.MobileOperator db = new DAL.MobileOperator();
            ul = db.UL
                .Where(i => i.userId == id).FirstOrDefault();
        }
        public ULModel(UL u) : base(u.userId)
        {
            ul = u;
        }
        override public string OrganizationName
        {
            get { return ul.organizationName; }
            set { ul.organizationName = value; }
        }
        override public string Address
        {
            get { return ul.address; }
            set { ul.address = value; }
        }
        override public bool Save()
        {
            UL ul = db.UL.Find(Id);
            Client c = db.Client.Find(Id);
            if (ul != null && c != null)
            {
                c.balance = Balance;
                c.rate = Rate;
                c.minutes = Minutes;
                c.number = Number;
                c.SMS = SMS;
                ul.address = this.Address;
                ul.organizationName = this.OrganizationName;

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
                    userId = u.id
                };
                db.Client.Add(c);
                if (db.SaveChanges() == 0)
                    return false;
                UL newul = new UL()
                {
                    address = this.Address,
                    organizationName = this.OrganizationName,
                    userId = u.id
                };
                db.UL.Add(newul);
                this.Id = u.id;
                this.Rate = (int)c.rate;
                return AddRateHistory();
            }
        }
        public override bool Remove()
        {
            UL ul = db.UL.Find(this.Id);
            db.UL.Remove(ul);
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
