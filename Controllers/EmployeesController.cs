using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using THI3.Models;

namespace THI3.Controllers
{
    public class EmployeesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Department);
            return View(employees.ToList());
        }

        [ChildActionOnly]
        public PartialViewResult CategoryEmp()
        {
            var li = db.Departments.ToList();
            return PartialView(li);
        }
        
        [Route("List/{deptid}")]
        public ActionResult HienThiTheoPhong(int deptid)
        {
            var li = db.Employees.Where(x => x.deptid == deptid).ToList();
            return View(li);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.deptid = new SelectList(db.Departments, "deptid", "deptname");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Employee nv)
        {
            try {
                db.Employees.Add(nv);
                db.SaveChanges();
                return Json(new { mess = "Add thanh cong!!!", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { mess = "Co loi:"+ex, JsonRequestBehavior.AllowGet });
            }


        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.deptid = new SelectList(db.Departments, "deptid", "deptname", employee.deptid);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(Employee nv)
        {
            try
            {
                Employee nv1 = db.Employees.FirstOrDefault(x=>x.eid==nv.eid);
                nv1.name = nv.name;
                nv1.salary = nv.salary;
                nv1.addr = nv.addr;
                nv1.age = nv.age;
                nv1.image = nv.image;
                nv1.deptid = nv.deptid;
                db.SaveChanges();
                return Json(new { mess = "Sua thanh cong!!!", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { mess = "Co loi:" + ex, JsonRequestBehavior.AllowGet });
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Employee nv)
        {
            try
            {
                Employee employee = db.Employees.Find(nv.eid);
                db.Employees.Remove(employee);
                db.SaveChanges();
                return Json(new { mess = "Xoa thanh cong!!!", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { mess = "Co loi:" + ex, JsonRequestBehavior.AllowGet });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
