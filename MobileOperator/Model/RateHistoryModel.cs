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
    public class RateHistoryModel
    {
        private RateHistory rate = new RateHistory();
        private DAL.MobileOperator db = new DAL.MobileOperator();
        public RateHistoryModel() { checkDB(db); }
        public RateHistoryModel(RateHistory r)
        {
            checkDB(db);
            rate = r;
        }
        public RateHistoryModel(int id)
        {
            checkDB(db);
            rate = db.RateHistory.Find(id);
        }

        public int Id
        {
            get { return rate.id; }
            set { rate.id = value; }
        }
        public int ClientId
        {
            get { return rate.clientId; }
            set { rate.clientId = value; }
        }
        public int RateId
        {
            get { return rate.rateId; }
            set { rate.rateId = value; }
        }
        public string RateName
        {
            get
            {
                DAL.Rate rateName = db.Rate.Find(rate.rateId);
                return rateName.name;
            }
            set { }
        }
        public DateTime FromDate
        {
            get { return rate.fromDate; }
            set { rate.fromDate = value; }
        }
        public string FromDateToString
        {
            get { return rate.fromDate.ToString("G"); }
            set { }
        }
        public DateTime TillDate
        {
            get { return (DateTime)rate.tillDate; }
            set { rate.tillDate = value; }
        }
        public string TillDateToString
        {
            get { if (rate.tillDate != null) return ((DateTime)rate.tillDate).ToString("G"); else return ""; }
            set { }
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
    }
}
