using Contract05_Report;
using Entity;
using System.ComponentModel;
using System.Windows.Controls;

namespace _Bus05_SimpleReport
{
    public class SimpleReportBus : ReportIBus
    {
        public SimpleReportBus()
        {

        }
        
        public SimpleReportBus(ReportIDao dao)
        {
            _dao = dao;
        }

        public override ReportIBus createNew(ReportIDao dao)
        {
            return new SimpleReportBus(dao);
        }

        public override BindingList<Product> getProducts(string keyword)
        {
            return _dao.getProducts(keyword);
        }

        public override Dictionary<int, Tuple<int, int>> getRevenueAndProfitAll(int month = 0, int year = 0)
        {
            return _dao.getRevenueAndProfitAll(month, year);
        }

        public override Dictionary<DateOnly, Tuple<int, int>> getRevenueAndProfitDaily(DateOnly from, DateOnly to)
        {
            return _dao.getRevenueAndProfitDaily(from, to);
        }

        public override Dictionary<int, int> getSoldProductQuantityAll(int proId, int month = 0, int year = 0)
        {
            return _dao.getSoldProductQuantityAll(proId, month, year);
        }

        public override Dictionary<DateOnly, int> getSoldProductQuantityDaily(int proId, DateOnly from, DateOnly to)
        {
            return _dao.getSoldProductQuantityDaily(proId, from, to);
        }

        public override List<int> getYears()
        {
            return _dao.getYears();
        }
        
        public override BindingList<Product> getTopHotProducts(int year = 0, int month = 0, int week = 0)
        {
            return _dao.getTopHotProducts(year, month, week);
        }
    }
}