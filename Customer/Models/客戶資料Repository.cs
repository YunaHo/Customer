using System;
using System.Linq;
using System.Collections.Generic;
using Customer.Models.ExportViewModel;

namespace Customer.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public IQueryable<客戶資料> 搜尋(IQueryable<客戶資料> 客戶資料,string keyword)
        {
            if (!String.IsNullOrEmpty(keyword))
            {
                客戶資料 = 客戶資料.Where(p => p.客戶名稱.Contains(keyword));
            }
            return 客戶資料;
        }

        public IQueryable<客戶資料> 搜尋分類(IQueryable<客戶資料> 客戶資料,string keyword)
        {
            if (!String.IsNullOrEmpty(keyword))
            {
                int iKeyword;
                if (!int.TryParse(keyword, out iKeyword)) iKeyword = 0;
                if (!iKeyword.Equals(0))
                    客戶資料 = 客戶資料.Where(p => p.客戶分類Id == iKeyword);
            }
            return 客戶資料;
        }

        public IQueryable<匯出客戶資料ViewModel> Export(IQueryable<客戶資料> 客戶資料)
        {
            var 匯出的客戶資料 = 客戶資料.Select(x => new 匯出客戶資料ViewModel()
            {
                客戶分類 = x.客戶分類.分類名稱,
                客戶名稱 = x.客戶名稱,
                統一編號 = x.統一編號,
                電話 = x.電話,
                傳真 = x.傳真,
                地址 = x.地址,
                Email = x.Email
            });
           
            return 匯出的客戶資料;
        }

        public IQueryable<客戶資料> Sort(IQueryable<客戶資料> 客戶資料, string OrderName,string currentSort)
        {
            if (!string.IsNullOrEmpty(currentSort))
            {
                switch (OrderName)
                {
                    case "分類名稱":
                        if (currentSort.Equals("desc"))
                            客戶資料 = 客戶資料.OrderByDescending(p => p.客戶分類.分類名稱);
                        else
                            客戶資料 = 客戶資料.OrderBy(p => p.客戶分類.分類名稱);
                        break;
                    case "客戶名稱":
                        if (currentSort.Equals("desc"))
                            客戶資料 = 客戶資料.OrderByDescending(p => p.客戶名稱);
                        else
                            客戶資料 = 客戶資料.OrderBy(p => p.客戶名稱);
                        break;
                    default:
                        客戶資料 = 客戶資料.OrderBy(p => p.客戶名稱);
                        break;
                }
            }
            else
                客戶資料 = 客戶資料.OrderBy(p => p.客戶名稱);
            return 客戶資料;
        }
        public 客戶資料 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<客戶資料> All(bool isAll = false)
        {
            if (isAll)
            {
                return base.All();
            }
            return base.All().Where(p => p.是否已刪除==false);
        }

        public override void Delete(客戶資料 entity)
        {
            entity.是否已刪除 = true;
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}