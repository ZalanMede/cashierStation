using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cashierStation_zaroProject.Models
{
    internal class Item
    {
        public Item()
        {

        }

        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        [Unique]
        public string Name { get; set; }
        public double OriginalPrice { get; set; }
        public double CurrentPrice { get; set; }
        public double DiscountPercent { get; set; }
        public int Amount { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public double Total { get { return CurrentPrice * Amount; } }

        public Item(string name, double originalPrice, double discountPercent, string category, string manufacturer, string description)
        {
            Name = name;
            OriginalPrice = originalPrice;
            DiscountPercent = discountPercent;
            Category = category;
            Manufacturer = manufacturer;
            Description = description;

            Amount = 1;
            CurrentPrice = OriginalPrice * (1 - DiscountPercent / 100);
        }

        public Item(string name, double originalPrice, string category, string manufacturer, string description)
        {
            Name = name;
            OriginalPrice = originalPrice;
            Category = category;
            Manufacturer = manufacturer;
            Description = description;

            Amount = 1;
            DiscountPercent = 0;
            CurrentPrice = OriginalPrice;
        }
    }
}
