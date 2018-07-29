using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customer.Models.Search
{
    public class SoreViewModel
    {
        public int Page { get; set; } = 1;
        public string SortName { get; set; } = "";
        public string SortOrder { get; set; } = "DESC";
        public int pageSize = 10;
    }

}