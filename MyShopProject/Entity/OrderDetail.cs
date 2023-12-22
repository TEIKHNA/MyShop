using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class OrderDetail : INotifyPropertyChanged, ICloneable
    {
        public int OrdDetId { get; set; }
        public int? OrdId { get; set; }
        public int? ProId { get; set; }
        public int? Quantity {  get; set; }  
        public int? PricePerItem { get; set; }
        public int? ProfitPerItem { get; set; }

        public string? ProName { get; set; } = null;

        public string? ImagePath { get; set; } = null;


        public event PropertyChangedEventHandler? PropertyChanged;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
