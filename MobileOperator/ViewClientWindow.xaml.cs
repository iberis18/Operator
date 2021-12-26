using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MobileOperator.ViewModel;

namespace MobileOperator
{
    /// <summary>
    /// Логика взаимодействия для ViewClientWindow.xaml
    /// </summary>
    public partial class ViewClientWindow : Window
    {
        public ViewClientWindow(int clientId, int clientStatus)
        {
            InitializeComponent();
            DataContext = new ViewClientWindowViewModel(clientId, clientStatus, this);
        }
        public ViewClientWindow()
        {
            InitializeComponent();
            DataContext = new ViewClientWindowViewModel(this);
        }
    }
}
