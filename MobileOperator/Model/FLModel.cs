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
            DAL.MobileOperator db = new DAL.MobileOperator();
            fl = db.FL
                .Where(i => i.userId == id).FirstOrDefault();
        }

        override public string FIO
        {
            get { return fl.FIO; }
            set
            {
                fl.FIO = value;
                OnPropertyChanged("FLFIO");
            }
        }
        override public string PasportDetales
        {
            get { return fl.pasportDetales; }
            set
            {
                fl.pasportDetales = value;
                OnPropertyChanged("FLPasportDetales");
            }
        }
    }
}
