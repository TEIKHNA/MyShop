using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Promotion : INotifyPropertyChanged
    {
        public int PromId { get; set; } 
        public string? Detail { get; set; }
        public int? DiscountPercent {  get; set; }   

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
