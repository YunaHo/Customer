using System;
using System.Linq;
using System.Collections.Generic;
using Customer.Models.ExportViewModel;

namespace Customer.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{

        public IQueryable<客戶銀行資訊> 搜尋客戶名稱(IQueryable<客戶銀行資訊> 客戶銀行資訊, string keyword)
        {
            if (!String.IsNullOrEmpty(keyword))
            {
                int iKeyword;
                if (!int.TryParse(keyword, out iKeyword)) iKeyword = 0;
                if (!iKeyword.Equals(0))
                    客戶銀行資訊 = 客戶銀行資訊.Where(p => p.客戶Id == iKeyword);
            }
            return 客戶銀行資訊;
        }

        public IQueryable<匯出客戶銀行資訊ViewModel> Export(IQueryable<客戶銀行資訊> 客戶銀行資訊)
        {
            var 匯出客戶銀行資訊資料 = 客戶銀行資訊.Select(x => new 匯出客戶銀行資訊ViewModel()
            {
                客戶名稱 = x.客戶資料.客戶名稱,
                銀行名稱 = x.銀行名稱,
                銀行代碼 = x.銀行代碼,
                分行代碼 = x.分行代碼,
                帳戶名稱 = x.帳戶名稱,
                帳戶號碼 = x.帳戶號碼
            });

            return 匯出客戶銀行資訊資料;
        }

        public 客戶銀行資訊 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<客戶銀行資訊> All(bool isAll = false)
        {
            if (isAll)
            {
                return base.All();
            }
            return base.All().Where(p => p.是否已刪除 == false);
        }

        public override void Delete(客戶銀行資訊 entity)
        {
            entity.是否已刪除 = true;
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}