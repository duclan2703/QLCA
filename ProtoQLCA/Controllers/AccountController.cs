using DAO_QLAC.BUS;
using DTO_QLAC;
using ProtoQLCA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProtoQLCA.Controllers
{
    public class AccountController : Controller
    {
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            AccountService service = new AccountService();
            Account account = new Account();
            var list = service.GetListAccount();
            AccountModel model = new AccountModel();
            model.Listaccount = list;
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult CreateAccount()
        {
            OfficeService service1 = new OfficeService();
            RolService role = new RolService();
            var account = new Account();
            account.DateOfBirth = DateTime.Now;
            List<Office> list = service1.GetAll().ToList();
            SelectList officelist = new SelectList(list, "OfficeID", "OfficeName");
            ViewBag.OfficeList = officelist;
            List<Rol> listrole = role.GetAll().ToList();
            SelectList rolelist = new SelectList(listrole, "RoleID", "Role_Name");
            ViewBag.RoleList = rolelist;
            return View(account);
        }
        
        [HttpPost]
        public ActionResult CreateAccount(Account account)
        {
            AccountService service = new AccountService();
            service.CreateNew(account);
            int userid = service.GetUserIDByName(account.Name);
            service.CreateRole(account.RoleID, userid);
            return Json(new { result = "Redirect", url = Url.Action("Create", "Account") });
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int ID)
        {
            AccountService service = new AccountService();
            OfficeService service1 = new OfficeService();
            RolService role = new RolService();
            var account = service.Getbykey(ID);
            List<Office> list = service1.GetAll().ToList();
            SelectList officelist = new SelectList(list, "OfficeID", "OfficeName");
            ViewBag.OfficeList = officelist;
            List<Rol> listrole = role.GetAll().ToList();
            SelectList rolelist = new SelectList(listrole, "RoleID", "Role_Name");
            ViewBag.RoleList = rolelist;
            return View(account);
        }

        [HttpPost]
        public ActionResult Edit(Account model)
        {
            try
            {
                AccountService service = new AccountService();
                service.Update(model);
                return RedirectToAction("Index", "Account");
            }
            catch(Exception e)
            {
                return View("Error");
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int ID)
        {
            AccountService service = new AccountService();
            try
            {
                if (ID < 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var account = service.Getbykey(ID);
                if (account == null)
                {
                    return HttpNotFound();
                }
                service.Delete(account.UserID);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
       
}