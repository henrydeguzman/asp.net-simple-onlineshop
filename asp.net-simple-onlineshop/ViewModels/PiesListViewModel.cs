using System;
using System.Collections.Generic;
using asp.net_simple_onlineshop.Models;

namespace asp.net_simple_onlineshop.ViewModels
{
    public class PiesListViewModel
    {
        public IEnumerable<Pie> Pies { get; set; }

        public string CurrentCategory { get; set; }
    }
}
