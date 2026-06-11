using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cashierStation_zaroProject.Models
{
    internal class Receipt
    {
        public int Id { get; set; }
        public int CashierId { get; set; }
        public long CustomerPointsCardCode { get; set; }
        public DateTime Date { get; set; }
        public double TotalAmount { get; set; }
        public List<Item> Items { get; set; }
        public Receipt()
        {
            Items = new List<Item>();
        }
        public Receipt(int cashierId, long customerPtsCardCode, DateTime date, double totalAmount, List<Item> items)
        {
            CashierId = cashierId;
            CustomerPointsCardCode = customerPtsCardCode;
            Date = date;
            TotalAmount = totalAmount;
            Items = items;
        }
        public Receipt(int cashierId, DateTime date, double totalAmount, List<Item> items)
        {
            CashierId = cashierId;
            Date = date;
            TotalAmount = totalAmount;
            Items = items;
        }
    }
}
