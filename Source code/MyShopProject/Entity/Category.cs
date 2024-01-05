using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Category : INotifyPropertyChanged, ICloneable
    {
        public int CatId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
