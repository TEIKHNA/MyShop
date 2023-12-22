using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Account : INotifyPropertyChanged
    {
        public int AccId { get; set; }  
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
