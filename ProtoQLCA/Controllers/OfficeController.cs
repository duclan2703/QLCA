using DAO_QLAC.BUS;
using DTO_QLAC;
using ProtoQLCA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProtoQLCA.Controllers
{
    
    public class OfficeController : Controller
    {
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            OfficeService service = new OfficeService();
            var list = service.GetAll();
            OfficeModel model = new OfficeModel();
            model.ListOffice = list; 
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult CreateNew()
        {
            var office = new Office();
            return View(office);
        }

        [HttpPost]
        public ActionResult CreateNew(Office office)
        {
            OfficeService service = new OfficeService();
            service.CreateNew(office);
            return RedirectToAction("Index", "Office");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int ID)
        {
            OfficeService service = new OfficeService();
            var office = service.Getbykey(ID);
            return View(office);
        }
        
        [HttpPost]
        public ActionResult Edit(Office model)
        {
            try
            {                
                OfficeService service = new OfficeService();
                service.Update(model);
                return RedirectToAction("Index", "Office");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int ID)
        {
            OfficeService service = new OfficeService();
            try
            {
                if (ID < 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var office = service.Getbykey(ID);
                if (office == null)
                {
                    return HttpNotFound();
                }
                service.Delete(office.OfficeID);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
}