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
    /// Логика взаимодействия для AskRateWindow.xaml
    /// </summary>
    public partial class AskRateWindow : Window
    {
        public AskRateWindow(int rateId, Window parentWindow)
        {
            InitializeComponent();
            DataContext = new AskRateWindowViewModel(rateId, this, parentWindow);
        }
    }
}
