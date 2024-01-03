using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Product : INotifyPropertyChanged, ICloneable
    {
        public int ProId { get; set; }
        public int? CatId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public int? PublishedYear { get; set; }
        public int? Quantity { get; set; }
        public int? OriginalPrice { get; set; }
        public int? SellingPrice { get; set; }
        public int? PromId { get; set; }
        public string? ImagePath { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int? Discount { get; set; } = null;
        public string? CatName { get; set; } = null;

        public event PropertyChangedEventHandler? PropertyChanged;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
