﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Customer.Models;
using Customer.Models.ExportExcel;
using Customer.Models.Search;
using Customer.Models.ViewModel;
using X.PagedList;

namespace Customer.Controllers
{
    public class 客戶聯絡人Controller : BaseController
    {
        public 客戶聯絡人Controller()
        {
            客戶資料repo = RepositoryHelper.Get客戶資料Repository();
            客戶聯絡人repo = RepositoryHelper.Get客戶聯絡人Repository();
        }
        // GET: 客戶聯絡人
        public ActionResult Index(string keyword,string  keyword2, int Page = 1)
        {
            ViewBag.Find職稱 = new SelectList(客戶聯絡人repo.職稱(), "key", "value");
            ViewBag.SearchViewModel = new 客戶聯絡人SearchViewModel();
            SoreViewModel SoreVM = new SoreViewModel();
            SoreVM.SortName = "Id";
            ViewBag.SoreViewModel = SoreVM;
            return View(客戶聯絡人repo.All().ToPagedList(1, pageSize));
        }

        [HttpPost]
        public ActionResult Index(客戶聯絡人SearchViewModel 客戶聯絡人Search, SoreViewModel SoreVM)
        {
            ViewBag.Find職稱 = new SelectList(客戶聯絡人repo.職稱(), "key", "value");
            var 客戶聯絡人 = 客戶聯絡人repo.搜尋(客戶聯絡人repo.All(), 客戶聯絡人Search.Find姓名);
            客戶聯絡人 = 客戶聯絡人repo.搜尋職稱(客戶聯絡人, 客戶聯絡人Search.Find職稱);
            客戶聯絡人 = 客戶聯絡人.OrderBy($"{SoreVM.SortName} {SoreVM.SortOrder}");
            ViewBag.SearchViewModel = 客戶聯絡人Search;
            ViewBag.SoreViewModel = SoreVM;
            return View(客戶聯絡人.ToPagedList(SoreVM.Page, pageSize));
        }

        public ActionResult Export()
        {
            ExportExcelResult ExportExcel = new ExportExcelResult();
            var 匯出客戶聯絡人資料 = 客戶聯絡人repo.Export(客戶聯絡人repo.All());
            DataTable dt客戶聯絡人資料 = ExportExcel.LinqQueryToDataTable(匯出客戶聯絡人資料);
            ExportExcel.ExportData = dt客戶聯絡人資料;
            ExportExcel.FileName = "客戶聯絡人資料";
            ExportExcel.SheetName = "客戶聯絡人資料";

            return ExportExcel.ExportExcel();
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = 客戶聯絡人repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(客戶資料repo.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                客戶聯絡人repo.Add(客戶聯絡人);
                客戶聯絡人repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(客戶資料repo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 =客戶聯絡人repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(客戶資料repo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                var db = 客戶聯絡人repo.UnitOfWork.Context;
                db.Entry(客戶聯絡人).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(客戶資料repo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 =客戶聯絡人repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 =客戶聯絡人repo.Find(id);
            客戶聯絡人repo.Delete(客戶聯絡人);
            客戶聯絡人repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult BatchUpdate(客戶聯絡人BatchViewModel[] data,客戶資料SearchViewModel 客戶資料Search, SoreViewModel SoreVM)
        {
            if (ModelState.IsValid)
            {
                foreach (var vm in data)
                {
                    var contact = 客戶聯絡人repo.Find(vm.Id);
                    contact.職稱 = vm.職稱;
                    contact.手機 = vm.手機;
                    contact.電話 = vm.電話;
                }
                客戶聯絡人repo.UnitOfWork.Commit();
                return RedirectToAction("Index",new { 客戶資料Search, SoreVM});
            }
            
            return View("Index");
        }
    }
}
