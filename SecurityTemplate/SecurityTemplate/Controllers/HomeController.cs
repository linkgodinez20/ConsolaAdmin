using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Security.Services.IServices;

//test
using Security.Core.Repository;
using Security.Core.Model;

namespace SecurityTemplate.Controllers
{
    public class HomeController : Controller
    {
        IRepo<LogIn> repo_login;

        public HomeController(IRepo<LogIn> repo_login)
        {
            this.repo_login = repo_login;
        }

        public HomeController() { 
        }

        public ActionResult Index()
        {
            //bool x = dd.CreateLogin("YoMero","123",1, 2);

            var _login_id = (from loginId in repo_login.GetAll()
                            where loginId.Usuario == "HFlores"
                            select loginId.Id_Login).FirstOrDefault();

            int loginid = Convert.ToInt32(_login_id.ToString());
            

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}