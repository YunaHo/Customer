using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Customer.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public IQueryable<客戶聯絡人> 搜尋(IQueryable<客戶聯絡人> 客戶聯絡人, string keyword)
        {
            if (!String.IsNullOrEmpty(keyword))
            {
                客戶聯絡人 = 客戶聯絡人.Where(p => p.姓名.Contains(keyword));
            }
            客戶聯絡人 = 客戶聯絡人.OrderBy(p => p.姓名);
            return 客戶聯絡人;
        }

        public IQueryable<客戶聯絡人> 搜尋職稱(IQueryable<客戶聯絡人> 客戶聯絡人, string keyword)
        {
            if (!String.IsNullOrEmpty(keyword))
            {
                客戶聯絡人 = 客戶聯絡人.Where(p => p.職稱 == keyword);

            }
            return 客戶聯絡人;
        }

        public Dictionary<string, string> 職稱()
        {
            var 職稱List = this.All().Select(p=>p.職稱).Distinct();
            Dictionary<string, string> JobTitle = new Dictionary<string, string>();
            {
                foreach(var item in 職稱List)
                {
                    JobTitle.Add(item,item);
                }
            }
            return JobTitle;
        }

        public 客戶聯絡人 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public bool IsRepeatMail(int 客戶Id,string CheckMail,int? id)
        {
            var 客戶聯絡人 = this.All();
            bool IsRepeat = false;
            客戶聯絡人 = 客戶聯絡人.Where(p=>p.客戶Id==客戶Id && p.Email==CheckMail);
            if (id!=null) //排除自已的id edit用的
            {
                客戶聯絡人 = 客戶聯絡人.Where(p=>p.Id!=id);
            }
            if (客戶聯絡人.Count() > 0) IsRepeat = true;
            return IsRepeat;
        }

        public IQueryable<客戶聯絡人> All(bool isAll = false)
        {
            if (isAll)
            {
                return base.All();
            }
            return base.All().Where(p => p.是否已刪除 == false);
        }

        public override void Delete(客戶聯絡人 entity)
        {
            entity.是否已刪除 = true;
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}