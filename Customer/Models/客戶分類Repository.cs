using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Customer.Models
{   
	public  class 客戶分類Repository : EFRepository<客戶分類>, I客戶分類Repository
	{

	}

	public  interface I客戶分類Repository : IRepository<客戶分類>
	{

	}
}