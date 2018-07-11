using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Customer.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{

        public IQueryable<客戶銀行資訊> 搜尋(string keyword)
        {
            var 客戶銀行資訊 = this.All();
            if (!String.IsNullOrEmpty(keyword))
            {
                客戶銀行資訊 = 客戶銀行資訊.Where(p => p.帳戶名稱.Contains(keyword));
            }
            客戶銀行資訊 = 客戶銀行資訊.OrderBy(p => p.帳戶名稱);
            return 客戶銀行資訊;
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