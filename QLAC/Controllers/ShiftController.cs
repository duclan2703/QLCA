using FX.Core;
using NHibernate.Mapping;
using QLAC.Domain;
using QLAC.IServices;
using QLAC.Models;
using QLAC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QLAC.Controllers
{
    public class ShiftController : Controller
    {
        public ActionResult ShiftIndex()
        {
            IShiftService service = new ShiftService();
            var list = service.GetAll();
            ShiftIndex model = new ShiftIndex();
            model.ListShift = list;
            return View(model);
        }
        public ActionResult Details(int id)
        {
            IShiftService service = new ShiftService();;
            var Shift = service.Getbykey(id);
            return View(Shift);
        }


        public ActionResult Create()
        {
            IShiftService service = new ShiftService();
            var Shift = new Shift();
            return View(Shift);
        }


        [HttpPost]
        public ActionResult Create(Shift shift)
        {
            try
            {
                //TryUpdateModel<Shift>(shift);
                IShiftService service = new ShiftService();;
                service.CreateNew(shift);
                return RedirectToAction("ShiftIndex");
            }
            catch (Exception e)
            {
                return View();
            }
        }


        public ActionResult Edit(int ID)
        {
            IShiftService service = new ShiftService();;
            var shift = service.Getbykey(ID);
            return View(shift);
        }


        [HttpPost]
        public ActionResult Edit(Shift model)
        {
            try
            {
                //TryUpdateModel<Shift>(model);
                IShiftService service = new ShiftService();;
                service.Update(model);
                
                return RedirectToAction("ShiftIndex");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            IShiftService service = new ShiftService();
            try
            {
                if (id < 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var Shift = service.Getbykey(id);
                if (Shift == null)
                {
                    return HttpNotFound();
                }
                service.Delete(Shift.ID);
                
                return RedirectToAction("ShiftIndex");
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }
    }
}