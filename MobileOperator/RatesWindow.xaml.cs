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
    /// Логика взаимодействия для RatesWindow.xaml
    /// </summary>
    public partial class RatesWindow : Window
    {

        public RatesWindow(int userId, int status)
        {
            InitializeComponent();
            DataContext = new RateWindowViewModel(userId, status, this);
        }
    }
}
