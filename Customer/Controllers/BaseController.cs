using Customer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer.Controllers
{
    public abstract class BaseController : Controller
    {
        protected int pageSize = 10;
        protected 客戶資料Repository 客戶資料repo;

        protected 客戶資料Entities db = new 客戶資料Entities();

    }
}