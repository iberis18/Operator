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
    class RateListModel : INotifyPropertyChanged
    {
        private List<RateModel> allCorporateRates;
        private List<RateModel> allNotCorporateRates;
        private List<RateModel> allRates;
        private DAL.MobileOperator db = new DAL.MobileOperator();
        public RateListModel()
        {
            checkDB(db);
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
            allRates = db.Rate.ToList().Select(i => new RateModel(i)).ToList();
        }
        public List<RateModel> AllCorporateRates
        {
            get { return allCorporateRates; }
        }
        public List<RateModel> AllNotCorporateRates
        {
            get { return allNotCorporateRates; }
        }
        public List<RateModel> AllRates
        {
            get { return allRates; }
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
