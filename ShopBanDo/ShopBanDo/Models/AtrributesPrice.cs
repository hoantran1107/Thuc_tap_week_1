using System;
using System.Collections.Generic;

#nullable disable

namespace ShopBanDo.Models
{
    public partial class AtrributesPrice
    {
        public int AttributesPriceId { get; set; }
        public int? AttributeId { get; set; }
        public int? ProductId { get; set; }
        public int? Price { get; set; }
        public bool Active { get; set; }
    }
}
