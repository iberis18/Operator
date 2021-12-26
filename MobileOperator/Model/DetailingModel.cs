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
    class DetailingModel : INotifyPropertyChanged
    {
        private List<CallModel> allCalls;
        private DAL.MobileOperator db = new DAL.MobileOperator();
        public DetailingModel(int clientId, string clientNumber, DateTime from, DateTime till)
        {
            checkDB(db);
            allCalls = new List<CallModel> { };
            SearchCalls(clientId, clientNumber, from, till);
        }

        public DetailingModel(string clientName, string clientNumber, DateTime from, DateTime till)
        {
            checkDB(db);
            allCalls = new List<CallModel> { };
            ClientModel client = new ClientModel(); ;
            FL fl;
            UL ul;
            if (clientName != "" && clientNumber != "")
            {
                Client c = db.Client.Where(i => i.number == clientNumber).FirstOrDefault();
                if (c != null)
                    if (c.FL != null && c.FL.FIO == clientName)
                        client = new FLModel(c.userId);
                    else if (c.UL != null && c.UL.organizationName == clientName)
                        client = new ULModel(c.userId);
            }
            else
            {
                if (clientName != "")
                {
                    fl = db.FL.Where(i => i.FIO == clientName).FirstOrDefault();
                    if (fl != null)
                        client = new FLModel(fl);
                    else
                    {
                        ul = db.UL.Where(i => i.organizationName == clientName).FirstOrDefault();
                        if (ul != null)
                            client = new ULModel(ul);
                    }
                }
                if (clientNumber != "")
                {
                    Client c = db.Client.Where(i => i.number == clientNumber).FirstOrDefault();
                    if (c != null)
                        if (c.FL != null)
                            client = new FLModel(c.userId);
                        else
                            client = new ULModel(c.userId);
                }
            }
            if (client.Id != 0)
            {
                SearchCalls(client.Id, client.Number, from, till);
            }
        }

        private void SearchCalls(int clientId, string clientNumber, DateTime from, DateTime till)
        {
            allCalls = db.Call
                .Where(i => (i.callerId == clientId || i.calledId == clientId || i.calledNumber == clientNumber || i.callerNumber == clientNumber) && i.time <= till && i.time >= from)
                .ToList()
                .Select(i => new CallModel(i))
                .ToList();
            foreach (CallModel call in allCalls)
            {
                if (call.CallerId == clientId)
                {
                    call.Direction = "Исходящий";
                    if (call.CalledId != 0)
                    {
                        ClientModel c = new ClientModel(call.CalledId);
                        call.Number = c.Number;
                    }
                    else
                        call.Number = call.CalledNumber;
                }
                else
                {
                    call.Direction = "Входящий";
                    if (call.CallerId != 0)
                    {
                        ClientModel c = new ClientModel(call.CallerId);
                        call.Number = c.Number;
                    }
                    else
                        call.Number = call.CallerNumber;
                }
            }
        }
        public List<CallModel> AllCalls
        {
            get { return allCalls; }
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
