using FX.Core;
using NHibernate.Mapping;
using QLAC.Domain;
using QLAC.IServices;
using QLAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QLAC.Controllers
{
    public class StaffController : Controller
    {
        public ActionResult StaffIndex()
        {
              IStaffService service = IoC.Resolve<IStaffService>();
            var list = service.GetAll();
            //var a = service.Getbygender("");
            StaffIndex model = new StaffIndex();
            model.ListStaff = list;
            return View(model);
        }
        public ActionResult Details(int id)
        {
            IStaffService service = IoC.Resolve<IStaffService>();         
            var staff = service.Getbykey(id);
            return View(staff);
        }


        public ActionResult Create()
        {
            IStaffService service = IoC.Resolve<IStaffService>();
            var staff = new Staff();
            return View(staff);
        }


        [HttpPost]
        public ActionResult Create(Staff staff)
        {
            try
            {
                TryUpdateModel<Staff>(staff);
                IStaffService service = IoC.Resolve<IStaffService>();
                service.CreateNew(staff);
                service.CommitChanges();
                return RedirectToAction("StaffIndex");
            }
            catch(Exception e)
            {
                return View();
            }
        }


        public ActionResult Edit(int ID)
        {
            IStaffService service = IoC.Resolve<IStaffService>();
            var staff = service.Getbykey(ID);
            return View(staff);
        }


        [HttpPost]
        public ActionResult Edit(Staff model)
        {
            try
            {
                TryUpdateModel<Staff>(model);
                IStaffService service = IoC.Resolve<IStaffService>();
                service.Update(model);
                service.CommitChanges();
                return RedirectToAction("StaffIndex");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            IStaffService service = IoC.Resolve<IStaffService>();
            try
            {
                if (id < 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var staff = service.Getbykey(id);
                if (staff == null)
                {
                    return HttpNotFound();
                }
                service.Delete(staff);
                service.CommitChanges();
                return RedirectToAction("StaffIndex");
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }        
    }
}