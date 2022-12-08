using ShopBanDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanDo.ModelView
{
    public class HomeViewVM
    {
        public List<TinTuc> TinTucs { get; set; }
        public List<ProductHomeVM> Products { get; set; }
       
        public Page Page { get; set; }
    }
}
