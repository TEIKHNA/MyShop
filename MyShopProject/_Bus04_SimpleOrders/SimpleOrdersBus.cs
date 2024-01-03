using Contract04_Orders;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Bus04_SimpleOrders
{
    public class SimpleOrdersBus : OrdersIBus
    {
        public SimpleOrdersBus() { }

        public SimpleOrdersBus(OrdersIDao dao)
        {
            _dao = dao;
        }

        public override int addOneOrder(Order order)
        {
            return _dao.addOneOrder(order);
        }

        public override int addOneOrderDetail(OrderDetail detail)
        {
            return _dao.addOneOrderDetail(detail);
        }

        public override OrdersIBus createNew(OrdersIDao dao)
        {
            return new SimpleOrdersBus(dao);
        }

        public override int delOneOrder(int id)
        {
            return _dao.delOneOrder(id);
        }

        public override int delOneOrderDetail(int? id)
        {
            return _dao.delOneOrderDetail(id);
        }

        public override int editOneOrderDetail(OrderDetail detail)
        {
            return (_dao.editOneOrderDetail(detail));
        }

        public override BindingList<Customer> getCustomers()
        {
            return _dao.getCustomers();
        }

        public override Order getOneOrder(int id)
        {
            return _dao.getOneOrder(id);
        }

        public override OrderDetail getOneOrderDetail(int id)
        {
            return _dao.getOneOrderDetail(id);
        }

        public override BindingList<OrderDetail> getOrderDetails(int ordId)
        {
            return _dao.getOrderDetails(ordId);
        }

        public override Tuple<int, BindingList<Order>> getOrders(int skip, int take, DateTime? startDate = null, DateTime? endDate = null)
        {
            return _dao.getOrders(skip, take, startDate, endDate);
        }

        public override BindingList<Product> getAvailProducts(string keyword)
        {
            return _dao.getAvailProducts(keyword);
        }

        public override int getProductLimit(int? proId)
        {
            return _dao.getProductLimit(proId);
        }

        public override int updateOrderPrice(int ordId)
        {
            return _dao.updateOrderPrice(ordId);
        }

        public override int setCustomerToOrder(int? cusId, int ordId)
        {
            return _dao.setCustomerToOrder(cusId, ordId);
        }

        public override int minusProductQuantity(int? proId)
        {
            return _dao.minusProductQuantity(proId);
        }
    }
}
