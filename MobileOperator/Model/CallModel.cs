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
    class CallModel : INotifyPropertyChanged
    {
        private DAL.MobileOperator db = new DAL.MobileOperator();
        private Call call;

        public CallModel(int id)
        {
            checkDB(db);
            call = db.Call.Find(id);
        }
        public CallModel(Call c)
        {
            checkDB(db);
            call = c;
        }

        public CallModel() { checkDB(db); }

        public int Id
        {
            get { return call.id; }
            set { call.id = value; }
        }
        public int CallerId
        {
            get { if (call.callerId != null) return call.callerId; else return 0; }
            set { call.callerId = value; }
        }
        public string CallerNumber
        {
            get { return call.callerNumber; }
            set { call.callerNumber = value; }
        }
        public int CalledId
        {
            get { if (call.calledId != null) return (int)call.calledId; else return 0; }
            set { call.calledId = value; }
        }
        public string CalledNumber
        {
            get { return call.calledNumber; }
            set { call.calledNumber = value; }
        }
        public DateTime Time
        {
            get { return call.time; }
            set { call.time = value; }
        }
        public string TimeToString
        {
            get { return call.time.ToString("G"); }
            set { }
        }
        public TimeSpan Duration
        {
            get { return call.duration; }
            set { call.duration = value; }
        }
        public int Type
        {
            get { return call.typeId; }
            set { call.typeId = value; }
        }
        public string TypeName
        {
            get
            {
                DAL.Type typeName = db.Type.Find(call.typeId);
                return typeName.name;
            }
            set { }
        }
        public decimal Cost
        {
            get { return call.cost; }
            set { call.cost = value; }
        }
        string direction;
        public string Direction
        {
            get
            { return direction; }
            set { direction = value; }
        }
        string number;
        public string Number
        {
            get { return number; }
            set { number = value; }
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
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
