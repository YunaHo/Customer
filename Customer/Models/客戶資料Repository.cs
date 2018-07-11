using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Customer.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public IQueryable<客戶資料> 搜尋(string keyword)
        {
            var client = this.All();
            if (!String.IsNullOrEmpty(keyword))
            {
                client = client.Where(p => p.客戶名稱.Contains(keyword));
            }
            client = client.OrderBy(p => p.客戶名稱);
            return client;
        }

        public 客戶資料 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}