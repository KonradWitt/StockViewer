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

namespace StockViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ContentControl stockUserControl = new StockUserControl();
        private ContentControl mainUserControl = new MainUserControl();

        public MainWindow()
        {
            InitializeComponent();
            contentControl.Content = mainUserControl;
            
        }
        private void btnStockCheck_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = stockUserControl;
        }

        private void btnStockSearch_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = mainUserControl;
        }

        private void btnStockHistory_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
