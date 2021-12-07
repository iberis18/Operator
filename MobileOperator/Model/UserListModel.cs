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
    class UserListModel : INotifyPropertyChanged
    {
        //private List<UserModel> allUsers;
        //private List<FLModel> allFLs;
        //private List<ULModel> allULs;
        private List<ClientModel> allClients;
        private List<AdminModel> allAdmins;
        private DAL.MobileOperator db = new DAL.MobileOperator();
        public UserListModel()
        {
            //allUsers = db.User.ToList().Select(i => new UserModel(i)).ToList();
            allAdmins = db.Admin.ToList().Select(i => new AdminModel(i, i.User)).ToList();
            allClients = db.Client.ToList().Select(i => new ClientModel(i.User, i)).ToList();
            //allFLs = db.FL.ToList().Select(i => new FLModel(i.Client.User, i.Client, i)).ToList();
            //allULs = db.UL.ToList().Select(i => new ULModel(i.Client.User, i.Client, i)).ToList();
        }

        //public List<UserModel> AllUsers
        //{
        //    get { return allUsers; }
        //}
        public List<ClientModel> AllClients
        {
            get { return allClients; }
        }
        public List<AdminModel> AllAdmins
        {
            get { return allAdmins; }
        }
        //public List<FLModel> AllFLs
        //{
        //    get { return allFLs; }
        //}
        //public List<ULModel> AllULs
        //{
        //    get { return allULs; }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
