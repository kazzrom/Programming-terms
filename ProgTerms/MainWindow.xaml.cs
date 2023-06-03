using ProgTerms.Controllers;
using ProgTerms.Pages;
using ProgTerms.AppData;
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

namespace ProgTerms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Properties.Frame = FrameMain;
            Properties.Frame.Navigate(new PageMain());
            Properties.BtnBack = BtnBack;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.IsMain)
            {
                Properties.Frame.Navigate(new PageMain());
                BtnBack.Visibility = Visibility.Collapsed;
            }
            else
            {
                Properties.Frame.Navigate(new PageTerm());
                Properties.IsMain = true;
            }
        }
    }
}
