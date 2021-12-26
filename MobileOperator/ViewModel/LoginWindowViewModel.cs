using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileOperator.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace MobileOperator.ViewModel
{
    class LoginWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Window window;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        string status;
        private RelayCommand checkUserStatusCommand;
        public RelayCommand CheckUserStatusCommand
        {
            get
            {
                return checkUserStatusCommand ??
                  (checkUserStatusCommand = new RelayCommand(obj =>
                  {
                      status = obj as string;
                  }));
            }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; NotifyPropertyChanged("Username"); }
        }

        UserListModel allUsers;
        public ObservableCollection<LoginModel> Users { get; set; }
        public LoginWindowViewModel(Window window)
        {
            Users = new ObservableCollection<LoginModel> { };
            allUsers = new UserListModel();
            this.window = window;
        }

        //89203695412 — 111
        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                  (loginCommand = new RelayCommand(obj =>
                  {
                      PasswordBox passwordBox = obj as PasswordBox;
                      string clearTextPassword = passwordBox.Password;
                      if (status == "Client")
                      {
                          foreach (ClientModel user in allUsers.AllClients)
                              Users.Add(new LoginModel()
                              {
                                  Status = user.Status,
                                  Id = user.Id,
                                  Password = user.Password,
                                  Login = user.Number
                              });
                      }
                      else if (status == "Admin")
                      {
                          foreach (AdminModel user in allUsers.AllAdmins)
                              Users.Add(new LoginModel()
                              {
                                  Status = user.Status,
                                  Id = user.Id,
                                  Password = user.Password,
                                  Login = user.Login
                              });
                      }
                      else MessageBox.Show("Вы клиент или администратор? Отметьте свой статус!");

                      var u = Users.Where(i => i.Login == username && i.Password == clearTextPassword).FirstOrDefault();
                      if (u != null)
                      {
                          if (u.Status == 1)
                          {
                              AdminMainWindow main = new AdminMainWindow();
                              main.Show();
                          }
                          else
                          {
                              MainWindow main = new MainWindow(u.Id, u.Status);
                              main.Show();
                          }
                          window.Close();
                      }
                      else MessageBox.Show("Неверный логин или пароль!");
                  }));
            }
        }
    }
}
