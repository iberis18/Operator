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
        override public string OrganizationName
        {
            get { return ul.organizationName; }
            set
            {
                ul.organizationName = value;
                OnPropertyChanged("ULOrganizationName");
            }
        }
        override public string Address
        {
            get { return ul.address; }
            set
            {
                ul.address = value;
                OnPropertyChanged("ULAddress");
            }
        }
    }
}
