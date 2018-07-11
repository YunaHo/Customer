namespace Customer.Models
{
    using Customer.Models.InputValidations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(客戶資料MetaData))]
    public partial class 客戶資料 
    {

        //internal static object ToPagedList(int page, int pageSize)
        //{
        //    throw new NotImplementedException();
        //}

    }

    public partial class 客戶資料MetaData
    {
        [Required]
        public int Id { get; set; }
        
        [DisplayFormat()]
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 客戶名稱 { get; set; }
        
        [StringLength(8, ErrorMessage="欄位長度不得大於 8 個字元")]
        [Required]
        [RegularExpression(@"^\d{8,8}",ErrorMessage ="統一編號為8碼數字")]
        public string 統一編號 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        [PhoneNumber("tel", ErrorMessage = "電話格式錯誤")]
        public string 電話 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [PhoneNumber("tel", ErrorMessage = "傳真格式錯誤")]
        public string 傳真 { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        public string 地址 { get; set; }

        [EmailAddress]
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        public string Email { get; set; }

        [Required]
        public int 客戶分類Id { get; set; }

        public virtual 客戶分類 客戶分類 { get; set; }
        public virtual ICollection<客戶銀行資訊> 客戶銀行資訊 { get; set; }
        public virtual ICollection<客戶聯絡人> 客戶聯絡人 { get; set; }
    }
}
