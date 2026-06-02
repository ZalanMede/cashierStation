using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cashierStation_zaroProject.Models
{
    internal interface InterFGenRepo<T> where T : new()
    {
        List<T> GetAll();
        void Insert(T item);
        void Update(T item);
        void Delete(T item);
    }
}
