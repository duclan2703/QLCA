using ProtoQLCA.Areas.Admin.Models;
using ProtoQLCA.Service;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace ProtoQLCA.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        //Get: Admin/Login
        public ActionResult Index()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AdminService service = new AdminService();
                    var admin = service.GetByUsername(model.Username);
                    if (admin != null && admin.Password == model.Password && admin.Username == model.Username)
                    {
                        var usrname = admin.Username;
                        HttpContext.Session.Add("username", usrname);
                        FormsAuthentication.SetAuthCookie(usrname, true);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View("Index");
                    }
                }
                else
                {
                    return View("Index");
                }
            }
            catch (Exception e)
            {
                return View();
            }

        }
        [Authorize]
        public ActionResult Mainpage()
        {
            return View();
        }
        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
    }
}