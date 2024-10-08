﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Customer : INotifyPropertyChanged
    {
        public int CusId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Tel { get; set; }
        public string? Email { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
