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
using cashierStation_zaroProject.Models;

namespace cashierStation_zaroProject
{
    /// <summary>
    /// Interaction logic for StationControllerWindow.xaml
    /// </summary>
    public partial class StationControllerWindow : Window
    {
        public SystemSate sysState;
        public StationControllerWindow()
        {
            InitializeComponent();
            sysState = SystemSate.ItemManagement;
        }

        private void NumButton_Click(object sender, RoutedEventArgs e)
        {
            Button senderBtn = (Button)sender;
            string hitNum = (string)senderBtn.Content;
            if (hitNum == "●")
            {
                hitNum = ".";
            }

            numInputTxtBox.Text += hitNum.ToString();
        }

        private void numEnter_Click(object sender, RoutedEventArgs e)
        {
            switch (sysState)
            {
                case SystemSate.ItemManagement:
                    break;
                case SystemSate.CashIn:
                    break;
                case SystemSate.CreditIn:
                    break;
                case SystemSate.OtherIn:
                    break;
                default:
                    break;
            }
        }

        private void numDel_Click(object sender, RoutedEventArgs e)
        {
            if (numInputTxtBox.Text.Length > 0)
            {
                numInputTxtBox.Text = numInputTxtBox.Text.Remove(numInputTxtBox.Text.Length - 1);
            }
        }

        private void numCancelAll_Click(object sender, RoutedEventArgs e)
        {
            sysState = SystemSate.ItemManagement;
            payRec.Text = "0.00 HUF";
            payChange.Text = "0.00 HUF";
        }

        private void paymentCash_Click(object sender, RoutedEventArgs e)
        {
            sysState = SystemSate.CashIn;
        }

        private void paymentCredit_Click(object sender, RoutedEventArgs e)
        {
            sysState = SystemSate.CreditIn;
        }

        private void paymentOther_Click(object sender, RoutedEventArgs e)
        {
            sysState = SystemSate.OtherIn;
        }
    }
}
