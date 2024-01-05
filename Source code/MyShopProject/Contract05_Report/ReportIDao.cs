using Entity;
using System.ComponentModel;

namespace Contract05_Report
{
    public abstract class ReportIDao
    {
        public abstract BindingList<Product> getProducts(string keyword);
        public abstract Dictionary<int, Tuple<int, int>> getRevenueAndProfitAll(int month = 0, int year = 0);
        public abstract Dictionary<DateOnly, Tuple<int, int>> getRevenueAndProfitDaily(DateOnly from, DateOnly to);
        public abstract Dictionary<int, int> getSoldProductQuantityAll(int proId, int month = 0, int year = 0);
        public abstract Dictionary<DateOnly, int> getSoldProductQuantityDaily(int proId, DateOnly from, DateOnly to);
        public abstract List<int> getYears();
        public abstract BindingList<Product> getTopHotProducts(int year = 0, int month = 0, int week = 0);
    }
}