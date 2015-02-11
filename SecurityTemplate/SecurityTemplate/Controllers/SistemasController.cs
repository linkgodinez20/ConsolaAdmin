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
            return View(sistemas);
        }

        // GET: /Sistemas/Create
        public ActionResult Create()
        {
            return View();
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
                return RedirectToAction("Index");
            }

            return View(sistemas);
        }

        // GET: /Sistemas/Edit/5
        public ActionResult Edit(byte id = 0)
        {
            Sistemas sistemas = repo.Get(id);

            if (sistemas == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return View(sistemas);
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
                return RedirectToAction("Index");
            }
            return View(sistemas);
        }

        // GET: /Sistemas/Delete/5
        public ActionResult Delete(byte id = 0)
        {
            Sistemas sistemas = repo.Get(id);

            if (sistemas == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return View(sistemas);
        }

        // POST: /Sistemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Sistemas sistemas = repo.Get(id);
            repo.Delete(sistemas);
            repo.Save();
            return RedirectToAction("Index");
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
