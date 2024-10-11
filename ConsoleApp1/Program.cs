using Accounting.DataLayer;
using Accounting.DataLayer.Repository;
using Accounting.DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Accounting_DBEntities db = new Accounting_DBEntities();
            ICustomerRepository customer = new CustomerRepository(db);
            Customers AddCustomer = new Customers() 
            { 
                Address = "lefwgrg",
                Email = "lkvlb",
                FullName = "abare",
                Image = "no",
                Mobile = "482358"
            };
            customer.InsertCustomer(AddCustomer);
            customer.Save();

            var list = customer.GetAllCustomers();


        }
    }
}
