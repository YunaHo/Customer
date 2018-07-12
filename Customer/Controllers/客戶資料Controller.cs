using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Customer.Models;
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
        public ActionResult Index(string keyword,string keyword2 , int Page = 1)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                keyword = ViewBag.keywordVB;
            }
            if (string.IsNullOrEmpty(keyword2))
            {
                keyword2 = ViewBag.keyword2VB;
            }
            var 客戶資料=客戶資料repo.搜尋(客戶資料repo.All(),keyword);
            客戶資料 = 客戶資料repo.搜尋分類(客戶資料, keyword2);

            ViewBag.keywordVB = keyword;
            ViewBag.keyword2VB = keyword2;

            ViewBag.客戶分類Id = new SelectList(客戶分類repo.All(), "Id", "分類名稱", keyword2);

            客戶資料 = 客戶資料.OrderBy(p => p.客戶名稱);
            return View(客戶資料.ToPagedList(Page, pageSize));
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
            ViewBag.客戶分類Id = new SelectList(客戶分類repo.All(), "Id", "分類名稱");
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
            ViewBag.客戶分類Id = new SelectList(客戶分類repo.All(), "Id", "分類名稱", 客戶資料.客戶分類Id);
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
            ViewBag.客戶分類Id = new SelectList(客戶分類repo.All(), "Id", "分類名稱", 客戶資料.客戶分類Id);
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
    }
}
