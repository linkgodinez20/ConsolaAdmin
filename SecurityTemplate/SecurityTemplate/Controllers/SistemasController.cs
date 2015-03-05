using System.Linq;
using System.Web.Mvc;
using Security.Core.Model;
using Security.Core.Repository;

namespace Security.Controllers
{
    public class SistemasController : Controller
    {
        private readonly IRepo<Sistemas> repo;

        public SistemasController(IRepo<Sistemas> repo)
        {
            this.repo = repo;
        }

        // GET: /Sistemas/
        public ActionResult Index()
        {
            return View(repo.GetAll().ToList());
        }

        // GET: /Sistemas/Details/5
        public ActionResult Details(byte id = 0)
        {
            Sistemas sistemas = repo.Get(id);

            if (sistemas == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Details", sistemas);
        }

        // GET: /Sistemas/Create
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        // POST: /Sistemas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_Sistema,Nombre,Siglas,Descripcion,Estatus")] Sistemas sistemas)
        {
            if (ModelState.IsValid)
            {
                repo.Add(sistemas);
                repo.Save();

                string url = Url.Action("Index", "Sistemas");
                return Json(new { success = true, url = url });
            }

            return PartialView("_Create", sistemas);
        }

        // GET: /Sistemas/Edit/5
        public ActionResult Edit(byte id = 0)
        {
            Sistemas sistemas = repo.Get(id);

            if (sistemas == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Edit", sistemas);
        }

        // POST: /Sistemas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_Sistema,Nombre,Siglas,Descripcion,Estatus")] Sistemas sistemas)
        {
            if (ModelState.IsValid)
            {
                repo.Update(sistemas);
                repo.Save();

                string url = Url.Action("Index", "Sistemas");
                return Json(new { success = true, url = url });
            }
            return PartialView("_Edit", sistemas);
        }

        // GET: /Sistemas/Delete/5
        public ActionResult Delete(byte id = 0)
        {
            Sistemas sistemas = repo.Get(id);

            if (sistemas == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Delete", sistemas);
        }

        // POST: /Sistemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Sistemas sistemas = repo.Get(id);
            repo.Delete(sistemas);
            repo.Save();

            string url = Url.Action("Index", "Sistemas", new { id = sistemas.Id_Sistema });
            return Json(new { success = true, url = url });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
