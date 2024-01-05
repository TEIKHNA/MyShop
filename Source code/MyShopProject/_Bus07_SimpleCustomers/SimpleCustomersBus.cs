using Contract07_Customers;
using Entity;
using System.ComponentModel;

namespace _Bus07_SimpleCustomers
{
    public class SimpleCustomersBus : CustomersIBus
    {
        public SimpleCustomersBus()
        {

        }

        public SimpleCustomersBus(CustomersIDao dao)
        {
            _dao = dao;
        }

        public override CustomersIBus createNew(CustomersIDao dao)
        {
            return new SimpleCustomersBus(dao);
        }

        public override int del(int id)
        {
            return _dao.del(id);
        }

        public override BindingList<Customer> getAll()
        {
            return _dao.getAll();
        }

        public override int add(Customer cus)
        {
            return _dao.add(cus);
        }

        public override int edit(int id, Customer cus)
        {
            return _dao.edit(id, cus);
        }
    }
}