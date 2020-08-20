using DAO_QLAC.BUS;
using DTO_QLAC;
using ProtoQLCA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;


namespace ProtoQLCA.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                AccountService service = new AccountService();
                var user = service.GetByUsername(model.Username);
                if (user != null && user.Password == model.Password && user.Username == model.Username)
                {
                    var usrname = user.Username;
                    HttpContext.Session.Add("username", usrname);
                    FormsAuthentication.SetAuthCookie(usrname, true);
                    return RedirectToAction("Mainpage", "Home");
                }
                else
                {
                    model.Password = "";
                    ModelState.AddModelError("", "Sai mat khau hoac tai khoan khong dung");
                    return View(model);
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

        [Authorize]
        public ActionResult ChangePassword()
        {
            AccountService service = new AccountService();
            ChangePasswordModel model = new ChangePasswordModel();
            var user = HttpContext.User.Identity.Name;
            //model.OldPassword = service.GetAccountPassword(user);
            model.Username = user;
            ViewBag.user = user;
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            AccountService service = new AccountService();
            model.Username = HttpContext.User.Identity.Name;
            Account account = new Account();
            account = service.GetAccountLogin(model.Username);
            if (account.Password == model.OldPassword && model.NewPassword == model.ConfirmNewPassword)
            {
                account.Username = model.Username;
                account.Password = model.NewPassword;
            }
            service.UpdatePassword(account);
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
    }
}