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

namespace cashierStation_zaroProject
{
    /// <summary>
    /// Interaction logic for StationControllerWindow.xaml
    /// </summary>
    public partial class StationControllerWindow : Window
    {
        public StationControllerWindow()
        {
            InitializeComponent();
        }

        private void NumButton_Click(object sender, RoutedEventArgs e)
        {
            Button senderBtn = (Button)sender;
            string hitNum = (string)senderBtn.Content;

            numInputTxtBox.Text += hitNum.ToString();
        }

        private void numEnter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void numDel_Click(object sender, RoutedEventArgs e)
        {
            numInputTxtBox.Text = numInputTxtBox.Text.Remove(numInputTxtBox.Text.Length - 1);
        }
    }
}
