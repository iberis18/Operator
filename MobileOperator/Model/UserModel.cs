using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DAL;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace MobileOperator.Model
{
    public class UserModel : INotifyPropertyChanged
    {
        protected User user = new User();
        private int status;

        public UserModel() { }
        public UserModel(User u)
        {
            user = u;
            status = CheckStatus();
        }
        public UserModel(int id)
        {
            DAL.MobileOperator db = new DAL.MobileOperator();
            user = db.User
                .Where(i => i.id == id).FirstOrDefault();
            status = CheckStatus();
        }

        private int CheckStatus ()
        {
            if (user.Admin != null)
                return 1;
            else if (user.Client.UL != null)
                return 2;
            else return 3;
        }

        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public int Id
        {
            get { return user.id; }
            set { user.id = value; }
        }

        public string Password
        {
            get { return user.password; }
            set { user.password = value; }
        }

        public virtual string Number { get; set; }
        //public virtual int Minutes { get; set; }
        //public virtual int SMS { get; set; }
        //public virtual int GB { get; set; }
        //public virtual int Rate { get; set; }
        //public virtual decimal Balance { get; set; }
        //public virtual string FIO { get; set; }
        //public virtual string PasportDetales { get; set; }
        //public virtual string OrganizationName { get; set; }
        //public virtual string Address { get; set; }
        //public virtual void RateHistory() { }

        virtual public bool Save() { return true; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
