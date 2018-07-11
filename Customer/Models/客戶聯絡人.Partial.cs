namespace Customer.Models
{
    using Customer.Models.InputValidations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            客戶聯絡人Repository 客戶聯絡人Repo = RepositoryHelper.Get客戶聯絡人Repository();
            if (this.Id == 0)
            {
                if (客戶聯絡人Repo.IsRepeatMail(this.客戶Id, this.Email, null))
                {
                    yield return new ValidationResult("同一客戶的聯絡人，不可有相同的Email", new string[] { "Email" });
                }
            }
            else
            {
                if (客戶聯絡人Repo.IsRepeatMail(this.客戶Id, this.Email, this.Id))
                {
                    yield return new ValidationResult("同一客戶的聯絡人，不可有相同的Email", new string[] { "Email" });
                }

            }
        
            //if (this.ClientId == 0)
            //{
            //    // 通常 P.K. 為 0 代表著正在執行「新增」動作
            //}
            //if (this.Longitude.HasValue != this.Latitude.HasValue)
            //{
            //    yield return new ValidationResult("經緯度欄位必須一起設定", new string[] { "Longitude", "Latitude" });
            //}
        }
    }
    
    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [PhoneNumber("phone", ErrorMessage = "手機格式錯誤")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [PhoneNumber("tel",ErrorMessage ="電話格式錯誤")]
        public string 電話 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
