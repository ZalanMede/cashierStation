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
        private List<Item> scannedItems = new List<Item>();
        public StationControllerWindow()
        {
            InitializeComponent();
            sysState = SystemSate.ItemManagement;
        }

        private void NumButton_Click(object sender, RoutedEventArgs e)
        {
            if (sysState == SystemSate.ItemManagement || sysState == SystemSate.CashIn)
            {
                Button senderBtn = (Button)sender;
                string hitNum = (string)senderBtn.Content;
                if (hitNum == "●")
                {
                    hitNum = ".";
                }
                numInputTxtBox.Text += hitNum.ToString();
            }
        }

        private void numEnter_Click(object sender, RoutedEventArgs e)
        {
            switch (sysState)
            {
                case SystemSate.ItemManagement:
                    var itemRepo = new GenericRepository<Item>(App.dataBasePath);
                    if (scannedItems.Find(x => x.Id == int.Parse(numInputTxtBox.Text)) == null)
                    {
                        try
                        {
                            Item searchedItem = itemRepo.GetAll().First(i => i.Id == int.Parse(numInputTxtBox.Text));
                            scannedItems.Add(searchedItem);
                            RefreshData();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Ilyen áru nem létezik.");
                        }
                    }
                    else
                    {
                        scannedItems.Find(x => x.Id == int.Parse(numInputTxtBox.Text)).Amount++;
                        RefreshData();
                    }
                    numInputTxtBox.Text = "";
                    break;
                case SystemSate.CashIn:
                    payRec.Text = (double.Parse(numInputTxtBox.Text) + double.Parse(payRec.Text.Replace(" HUF", ""))).ToString("0.00") + " HUF";

                    double payNeeded = double.Parse(payDue.Text.Replace(" HUF", ""));
                    double payGiven = double.Parse(payRec.Text.Replace(" HUF", ""));

                    double change = (payNeeded - payGiven) * -1;

                    if (change >= 0)
                    {
                        payChange.Text = change.ToString("0.00") + " HUF";
                        sysState = SystemSate.CashOut;
                    }

                    numInputTxtBox.Text = "";
                    break;
                case SystemSate.CreditIn:
                    // Ide menne a tényleges credit kártyás fizetés logika
                    payRec.Text = payDue.Text;
                    sysState = SystemSate.CreditOut;
                    break;
                case SystemSate.OtherIn:
                    // Ide menne a tényleges egyéb fizetési mód logikája
                    payRec.Text = payDue.Text;
                    sysState = SystemSate.OtherOut;
                    break;
                case SystemSate.CreditOut:
                    PaymentDone();
                    break;
                case SystemSate.OtherOut:
                    PaymentDone();
                    break;
                case SystemSate.CashOut:
                    PaymentDone();
                    break;
                default:
                    MessageBox.Show("Error: Nincs SystemState");
                    break;
            }
        }

        private void PaymentDone()
        {
            transactionGrid.Background = Brushes.LightGreen;
            numInputTxtBox.Text = "";
            sysState = SystemSate.ItemManagement;
            paymentCash.Background = Brushes.LightGray;
            paymentCredit.Background = Brushes.LightGray;
            paymentOther.Background = Brushes.LightGray;
            numInputTxtBox.Background = Brushes.White;

            payRec.Text = "0.00 HUF";
            payChange.Text = "0.00 HUF";
            scannedItems.Clear();
            RefreshData();

            itemDataGrid.IsEnabled = true;
            MessageBox.Show("TRANZAKCIÓ SIKERES");
            transactionGrid.Background = Brushes.LightGray;
        }

        private void RefreshData()
        {
            itemDataGrid.Items.Clear();
            scannedItems.ForEach(i => itemDataGrid.Items.Add(i));
            payDue.Text = (scannedItems.Sum(i => i.Total)).ToString("0.00") + " HUF";
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
            if (sysState == SystemSate.ItemManagement)
            {
                payRec.Text = "0.00 HUF";
                payChange.Text = "0.00 HUF";
                numInputTxtBox.Text = "";
                scannedItems.Clear();
                RefreshData();
            }
            else if (sysState == SystemSate.CashIn || sysState == SystemSate.CreditIn || sysState == SystemSate.OtherIn)
            {
                numInputTxtBox.Text = "";
                sysState = SystemSate.ItemManagement;
                paymentCash.Background = Brushes.LightGray;
                paymentCredit.Background = Brushes.LightGray;
                paymentOther.Background = Brushes.LightGray;
                numInputTxtBox.Background = Brushes.White;
                itemDataGrid.IsEnabled = true;
            }

        }

        private void decreaseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Item item)
            {
                if ((string)button.CommandParameter == "inc")
                {
                    item.Amount++;
                }
                else if ((string)button.CommandParameter == "dec" && item.Amount > 0)
                {
                    item.Amount--;
                }
            }
            RefreshData();
        }

        private void deleteItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Item item)
            {
                scannedItems.Remove(item);
                RefreshData();
            }
        }

        private void paymentCash_Click(object sender, RoutedEventArgs e)
        {
            if (sysState == SystemSate.ItemManagement)
            {
                sysState = SystemSate.CashIn;
                numInputTxtBox.Text = "";
                paymentCash.Background = Brushes.LightGreen;
                numInputTxtBox.Background = Brushes.LightGreen;
                payDue.Text = (scannedItems.Sum(i => i.Total)).ToString("0.00") + " HUF";
                itemDataGrid.IsEnabled = false;
            }
        }

        private void paymentCredit_Click(object sender, RoutedEventArgs e)
        {
            if (sysState == SystemSate.ItemManagement)
            {
                sysState = SystemSate.CreditIn;
                numInputTxtBox.Text = "";
                paymentCredit.Background = Brushes.LightGreen;
                numInputTxtBox.Background = Brushes.LightGreen;
                payDue.Text = (scannedItems.Sum(i => i.Total)).ToString("0.00") + " HUF";
                itemDataGrid.IsEnabled = false;
            }
        }

        private void paymentOther_Click(object sender, RoutedEventArgs e)
        {
            if (sysState == SystemSate.ItemManagement)
            {
                sysState = SystemSate.OtherIn;
                numInputTxtBox.Text = "";
                paymentOther.Background = Brushes.LightGreen;
                numInputTxtBox.Background = Brushes.LightGreen;
                payDue.Text = (scannedItems.Sum(i => i.Total)).ToString("0.00") + " HUF";
                itemDataGrid.IsEnabled = false;
            }
        }
    }
}
