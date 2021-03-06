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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MobileOperator.ViewModel;

namespace MobileOperator
{
    /// <summary>
    /// Логика взаимодействия для ChangeRateWindow.xaml
    /// </summary>
    public partial class ChangeRateWindow : Window
    {
        public ChangeRateWindow(int userId)
        {
            InitializeComponent();
            DataContext = new ChangeRateWindowViewModel(userId, this);
        }
    }
}
