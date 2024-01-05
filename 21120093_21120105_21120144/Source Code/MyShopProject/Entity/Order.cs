using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Order : INotifyPropertyChanged
    {
        public int OrdId { get; set; }
        public int? CusId { get; set; }
        public int? FinalPrice { get; set; }
        public int? FinalProfit { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CusName { get; set; } = null;

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
