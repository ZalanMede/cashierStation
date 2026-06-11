using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cashierStation_zaroProject.Models
{
    internal class Customer
    {
        // EZEK SZERINT FELESLEGESEN SZENVEDTEM EZZEL???? 
        // NEM VOLT TÉNYLEGESEN "KÖTELEZŐ CLASS" ELNEVEZÉSSEL EGYÜTT????
        // Sírok ;-;
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        [Unique]
        public long CardCode { get; set; } // Generated as a 16 digit number, unique for each customer
        public int Points { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Customer()
        {
        }

        public Customer(long cardCode, int points, string fullName, string email, string phoneNumber)
        {
            CardCode = cardCode;
            Points = points;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public Customer(int id, long cardCode, int points, string fullName, string email, string phoneNumber)
        {
            Id = id;
            CardCode = cardCode;
            Points = points;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
