using DAO_QLAC.BUS;
using ProtoQLCA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProtoQLCA.Controllers
{
    public class ShiftController : Controller
    {
        [Authorize(Roles ="admin")]
        public ActionResult Index()
        {
            ShiftService service = new ShiftService();
            var list = service.GetAll();
            ShiftModel model = new ShiftModel();
            model.ListShift = list;
            return View(model);
        }
    }
}