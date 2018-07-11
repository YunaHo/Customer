namespace Customer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(客戶分類MetaData))]
    public partial class 客戶分類
    {
    }
    
    public partial class 客戶分類MetaData
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 分類名稱 { get; set; }
        [Required]
        public bool 是否已刪除 { get; set; }
    
        public virtual ICollection<客戶資料> 客戶資料 { get; set; }
    }
}
