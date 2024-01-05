using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract04_Orders
{
    public abstract class OrdersIBus
    {
        protected OrdersIDao _dao;
        public abstract OrdersIBus createNew(OrdersIDao dao);
        public abstract BindingList<Product> getAvailProducts(string keyword);
        public abstract Tuple<int, BindingList<Order>> getOrders(int skip, int take, DateTime? startDate = null, DateTime? endDate = null);
        public abstract Order getOneOrder(int id);
        public abstract int delOneOrder(int id);
        public abstract int addOneOrder(Order order);
        public abstract BindingList<Customer> getCustomers();
        public abstract BindingList<OrderDetail> getOrderDetails(int ordId);
        public abstract OrderDetail getOneOrderDetail(int id);
        public abstract int delOneOrderDetail(int? id);
        public abstract int editOneOrderDetail(OrderDetail detail);
        public abstract int addOneOrderDetail(OrderDetail detail);
        public abstract int getProductLimit(int? proId);
        public abstract int updateOrderPrice(int ordId);
        public abstract int setCustomerToOrder(int? cusId, int ordId);
        public abstract int minusProductQuantity(int? proId);
    }
}
