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
    public class LoginModel : INotifyPropertyChanged
    {
        int id;
        string password;
        string login;
        int status;

        public LoginModel() { }

        public int Status
        {
            get
            { return status; }
            set
            {
                status = value;
            }
        }
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
            }
        }
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
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
