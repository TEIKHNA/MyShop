using Entity;
using System.ComponentModel;
using System.Windows.Controls;

namespace Contract05_Report
{
    public abstract class ReportIBus
    {
        protected ReportIDao _dao;
        public abstract BindingList<Product> getProducts(string keyword);
        public abstract ReportIBus createNew(ReportIDao dao);
        public abstract Dictionary<int, Tuple<int, int>> getRevenueAndProfitAll(int month = 0, int year = 0);
        public abstract Dictionary<DateOnly, Tuple<int, int>> getRevenueAndProfitDaily(DateOnly from, DateOnly to);
        public abstract Dictionary<int, int> getSoldProductQuantityAll(int proId, int month = 0, int year = 0);
        public abstract Dictionary<DateOnly, int> getSoldProductQuantityDaily(int proId, DateOnly from, DateOnly to);
        public abstract List<int> getYears();
        public abstract BindingList<Product> getTopHotProducts(int year = 0, int month = 0, int week = 0);
    }
}