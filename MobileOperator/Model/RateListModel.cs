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
    class RateListModel : INotifyPropertyChanged
    {
        private List<RateModel> allCorporateRates;
        private List<RateModel> allNotCorporateRates;
        private DAL.MobileOperator db = new DAL.MobileOperator();
        public RateListModel()
        {
            allCorporateRates = db.Rate
                .Where(i => i.corporate == true)
                .ToList()
                .Select(i => new RateModel(i))
                .ToList();
            allNotCorporateRates = db.Rate
                .Where(i => i.corporate == false)
                .ToList()
                .Select(i => new RateModel(i))
                .ToList();
        }

        public List<RateModel> AllCorporateRates
        {
            get { return allCorporateRates; }
        }
        public List<RateModel> AllNotCorporateRates
        {
            get { return allNotCorporateRates; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
