using System;
using System.Collections.Generic;

#nullable disable

namespace ShopBanDo.Models
{
    public partial class TinTuc
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Scontents { get; set; }
        public string Contents { get; set; }
        public string Thumb { get; set; }
        public string Alias { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool Published { get; set; }
        public string Author { get; set; }
        public int? AccountId { get; set; }
        public string Tags { get; set; }
        public int? CatId { get; set; }
        public bool IsHot { get; set; }
        public bool IsNewfeed { get; set; }
        public string MetaKey { get; set; }
        public string MetaDesc { get; set; }
        public int? Views { get; set; }
    }
}
