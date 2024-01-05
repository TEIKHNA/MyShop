using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract07_Customers
{
    public abstract class CustomersIDao
    {
        public abstract BindingList<Customer> getAll();
        public abstract int add(Customer cus);
        public abstract int del(int id);
        public abstract int edit(int id, Customer cus);
    }
}
