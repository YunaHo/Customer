using System;
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
using X.PagedList;

namespace Customer.Controllers
{
    public class 客戶資料Controller : BaseController
    {
        public 客戶資料Controller()
        {
            客戶資料repo = RepositoryHelper.Get客戶資料Repository();
            客戶分類repo = RepositoryHelper.Get客戶分類Repository();
        }

        // GET: 客戶資料
        public ActionResult Index()
        {
            ViewBag.Find客戶分類 = new SelectList(客戶分類repo.All(), "Id", "分類名稱");
            ViewBag.SearchViewModel = new 客戶資料SearchViewModel();
            ViewBag.SoreViewModel = new SoreViewModel();

            return View(客戶資料repo.All().ToPagedList(1, pageSize));
        }


        [HttpPost]
        public ActionResult Index(客戶資料SearchViewModel 客戶資料Search, SoreViewModel SoreVM)
        {
            ViewBag.Find客戶分類 = new SelectList(客戶分類repo.All(), "Id", "分類名稱");
            var 客戶資料 = 客戶資料repo.搜尋(客戶資料repo.All(), 客戶資料Search.Find客戶名稱);
            客戶資料 = 客戶資料repo.搜尋分類(客戶資料, 客戶資料Search.Find客戶分類);
            客戶資料 = 客戶資料.OrderBy($"{SoreVM.SortName} {SoreVM.SortOrder}");
            ViewBag.SearchViewModel = 客戶資料Search;
            ViewBag.SoreViewModel = SoreVM;
            return View(客戶資料.ToPagedList(SoreVM.Page, pageSize));
        }
        public ActionResult Export()
        {
            ExportExcelResult ExportExcel = new ExportExcelResult();
            var 匯出的客戶資料 = 客戶資料repo.Export(客戶資料repo.All());
            DataTable dt客戶資料 = ExportExcel.LinqQueryToDataTable(匯出的客戶資料);
            ExportExcel.ExportData = dt客戶資料;
            ExportExcel.FileName = "客戶資料";
            ExportExcel.SheetName = "客戶資料";

            return ExportExcel.ExportExcel();
        }



        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = 客戶資料repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            ViewBag.Find客戶分類 = new SelectList(客戶分類repo.All(), "Id", "分類名稱");
            return View();
        }

        // POST: 客戶資料/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類Id")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                客戶資料repo.Add(客戶資料);
                客戶資料repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = 客戶資料repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            ViewBag.Find客戶分類 = new SelectList(客戶分類repo.All(), "Id", "分類名稱", 客戶資料.客戶分類Id);
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類Id")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                var db = 客戶資料repo.UnitOfWork.Context;
                db.Entry(客戶資料).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Find客戶分類 = new SelectList(客戶分類repo.All(), "Id", "分類名稱", 客戶資料.客戶分類Id);
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = 客戶資料repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = 客戶資料repo.Find(id);
            客戶資料repo.Delete(客戶資料);
            客戶資料repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                客戶資料repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult ConcatList(int id)
        {
            ViewData.Model = 客戶資料repo.Find(id);
            return PartialView("ConcatList");
        }


    }
}
