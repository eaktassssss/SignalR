using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ObsSignalR.Models.Context;

namespace ObsSignalR.Controllers
{
    public class ObsController : Controller
    {
        public async Task<ActionResult> Index()
        {
            using (ObsContext db = new ObsContext())
            {
                var students = await db.Students.ToListAsync();
                return View(students);
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Create(Students students)
        {
            using (ObsContext db = new ObsContext())
            {
                if (!ModelState.IsValid)
                {
                    return Json(new {result = "işlem sırasında hata oluştu", succeeded = false});
                }
                db.Students.Add(students);
                await db.SaveChangesAsync();
                return Json(new { result = "İşlem başarılı", succeeded = true });
            }
        }
        public async Task<ActionResult> Edit(int? id)
        {
            using (ObsContext db = new ObsContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Students students = await db.Students.FindAsync(id);
                if (students == null)
                {
                    return HttpNotFound();
                }
                return View(students);
            }
        }
        [HttpPost]
       
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,IdentityNumber,PhoneNumber")] Students students)
        {
            using (ObsContext db = new ObsContext())
            {
                if (!ModelState.IsValid)
                {
                    return View(students);
                }
                db.Entry(students).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Obs");
            }
        }
        public async Task<ActionResult> Delete(int? id)
        {
            using (ObsContext db = new ObsContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Students students = await db.Students.FindAsync(id);
                if (students == null)
                {
                    return HttpNotFound();
                }
                return View(students);
            }
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (ObsContext db = new ObsContext())
            {
                Students students = await db.Students.FindAsync(id);
                db.Students.Remove(students);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Obs");
            }
        }
    }
}
