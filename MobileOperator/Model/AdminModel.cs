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
    public class AdminModel : UserModel
    {
        private Admin admin = new Admin();
        public AdminModel() : base() { }
        public AdminModel(Admin a, User u) : base(u)
        {
            admin = a;
        }
        public AdminModel(int id) : base(id)
        {
            DAL.MobileOperator db = new DAL.MobileOperator();
            admin = db.Admin
                .Where(i => i.userId == id).FirstOrDefault();
        }

        public string Login
        {
            get { return admin.login; }
            set
            {
                admin.login = value;
                OnPropertyChanged("AdminLogin");
            }
        }
    }
}
