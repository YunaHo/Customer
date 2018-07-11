namespace Customer.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(usp_客戶資料統計_ResultMetaData))]
    public partial class usp_客戶資料統計_Result
    {
    }
    
    public partial class usp_客戶資料統計_ResultMetaData
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 客戶名稱 { get; set; }
        public Nullable<int> 銀行數量 { get; set; }
        public Nullable<int> 聯絡人數量 { get; set; }
    }
}
