﻿using Accounting.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DataLayer.Repository
{
    public interface ICustomerRepository
    {
        List<Customers> GetAllCustomers();

        IEnumerable<Customers> GetCustomerByFilter(string parameter);

        List<ListCustomerViewModel> GetNamesCustomer(string filter = "");

        Customers GetCustomerById(int customerId);

        bool InsertCustomer(Customers customer);

        bool UpdateCustomer(Customers customer);

        bool DeleteCustomer(Customers customer);

        bool DeleteCustomer(int customerId);

        int GetCustomerIdByName(string name);
        string GetCustomerNameById(int customerId);
    }
}
