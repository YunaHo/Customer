using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customer.Models.ExportViewModel
{
    public class 匯出客戶銀行資訊ViewModel
    {
        public string 客戶名稱 { get; set; }
        public string 銀行名稱 { get; set; }
        public int 銀行代碼 { get; set; }
        public int? 分行代碼 { get; set; }
        public string 帳戶名稱 { get; set; }
        public string 帳戶號碼 { get; set; }

    }
}