using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Customer.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public IQueryable<客戶資料> 搜尋(string keyword)
        {
            var 客戶資料 = this.All();
            if (!String.IsNullOrEmpty(keyword))
            {
                客戶資料 = 客戶資料.Where(p => p.客戶名稱.Contains(keyword));
            }
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