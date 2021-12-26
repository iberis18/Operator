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
    /// Логика взаимодействия для AdminDetailingWindow2.xaml
    /// </summary>
    public partial class AdminDetailingWindow2 : Window
    {
        public AdminDetailingWindow2()
        {
            InitializeComponent();
            DataContext = new AdminDetailingWindow2ViewModel(this);
        }
    }
}
