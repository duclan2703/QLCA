using ProtoQLCA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using OfficeOpenXml.Table;
using System.Data;
using DTO_QLAC;
using DAO_QLAC.BUS;

namespace ProtoQLCA.Controllers
{
    public class OrderController : Controller
    {
        [Authorize(Roles = "chief")]
        public ActionResult Index()
        {
            OrderService service = new OrderService();
            OfficeService offservice = new OfficeService();
            var list = service.GetListOrder();
            OrderModel model = new OrderModel();
            model.Listorder = list;
            List<Office> lstoffname = offservice.GetOfficeName();
            SelectList namelist = new SelectList(lstoffname, "", "OfficeName");
            ViewBag.Listname = namelist;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(FilterModel model)
        {
            OrderService service = new OrderService();
            OfficeService offservice = new OfficeService();
            OrderModel order = new OrderModel();
            var lstorder = service.GetFilter(model.Name, model.OfficeName, model.FromDate, model.ToDate);
            order.Listorder = lstorder;
            List<Office> lstoffname = offservice.GetOfficeName();
            SelectList namelist = new SelectList(lstoffname, "", "OfficeName");
            ViewBag.Listname = namelist;
            return View(order);
        }
        /// <summary>
        /// tạo view phiếu ăn cá nhân
        /// </summary>
        /// <returns></returns>
        public ActionResult Personalform()
        {
            AccountService service = new AccountService();
            var order = new OrderModel();
            var username = HttpContext.User.Identity.Name;
            var name = service.GetNameByUserName(username);
            ViewBag.Name = name;
            var officeid = service.GetOfficeIDNameByUsername(username);
            var officename = service.GetOfficeNameByOfficeID(officeid);
            ViewBag.Officename = officename;
            return View(order);
        }

        /// <summary>
        /// submit phiếu ăn sau đó cập nhật vào model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Personalform(OrderModel model)
        {
            OrderService service = new OrderService();
            AccountService accountservice = new AccountService();
            Order order = new Order();
            var username = HttpContext.User.Identity.Name;
            Account acclogin = accountservice.GetAccountLogin(username);
            order.UserID = acclogin.UserID;
            order.OfficeID = acclogin.OfficeID;
            order.Name = acclogin.Name;
            order.OrderDate = model.OrderDate;
            order.EditDate = model.OrderDate;
            order.EditBy = "";
            if (model.Ca1)
            {
                order.ShiftID = 1;
                order.Quantity = model.Quantity;
                service.CreateNew(order);
            }
            if (model.Ca2)
            {
                order.ShiftID = 2;
                order.Quantity = model.Quantity;
                service.CreateNew(order);
            }
            if (model.Ca3)
            {
                order.ShiftID = 3;
                order.Quantity = model.Quantity;
                service.CreateNew(order);
            }
            return RedirectToAction("Success");
        }

        public ActionResult Multipleform()
        {
            AccountService service = new AccountService();
            OfficeService offservice = new OfficeService();
            var order = new MultiOrderModel();
            var username = HttpContext.User.Identity.Name;
            
            var officeid = service.GetOfficeIDNameByUsername(username);
            var officename = service.GetOfficeNameByOfficeID(officeid);
            ViewBag.offname = officename;
            List<Account> listname = service.GetNamebyOfficeID(officeid);
            foreach (var item in listname)
            {
                MultiOrderModel staff = new MultiOrderModel();
                staff.Name = item.Name;
                staff.UserID = item.UserID;
                order.Liststaff.Add(staff);
            }
            ViewBag.Listname = new SelectList(order.Liststaff, "UserID", "Name");
            return View(order);
        }

        [HttpPost]
        public ActionResult MultipleformCreate(string orderlist)
        {
            OrderService service = new OrderService();
            AccountService accountservice = new AccountService();
            var listorder = JsonConvert.DeserializeObject<List<OrderInput>>(orderlist);
            Order order = new Order();
            foreach (var orderitem in listorder)
            {
                order.Name = orderitem.Name;
                order.UserID = accountservice.GetUserIDByName(orderitem.Name);
                order.OrderDate = Convert.ToDateTime(DateTime.ParseExact(orderitem.OrderDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None));
                order.OfficeID = accountservice.GetOfficeIDByUserID(order.UserID);
                order.EditDate = order.OrderDate;
                order.EditBy = "";
                if (orderitem.Quantity1 != "")
                {
                    order.Quantity = Convert.ToInt32(orderitem.Quantity1);
                    order.ShiftID = 1;
                }
                if (orderitem.Quantity2 != "")
                {
                    order.Quantity = Convert.ToInt32(orderitem.Quantity2);
                    order.ShiftID = 2;
                }
                if (orderitem.Quantity3 != "")
                {
                    order.Quantity = Convert.ToInt32(orderitem.Quantity3);
                    order.ShiftID = 3;
                }
                service.CreateNew(order);
            }
            return Json(new { result = "Redirect", url = Url.Action("Success", "Order") });
        }

        public ActionResult GetListName(string keyword, int fetchsize)
        {
            AccountService service = new AccountService();
            var username = HttpContext.User.Identity.Name;
            var officeid = service.GetOfficeIDNameByUsername(username);
            var order = new MultiOrderModel();
            List<Account> listname = service.GetNamebyOfficeID(officeid);
            foreach (var item in listname)
            {
                Staff staff = new Staff();
                staff.Name = item.Name;
                staff.ID = item.UserID;
                order.Liststaffs.Add(staff);
            }
            var namelist = order.Liststaffs;
            if (!string.IsNullOrEmpty(keyword))
            {
                listname = listname.Where(x => x.Name.ToUpper().Contains(keyword.ToUpper())).ToList();
                return Json(listname, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(namelist, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Success()
        {
            OrderService service = new OrderService();
            OfficeService offservice = new OfficeService();
            var list = service.GetListOrder();
            OrderModel model = new OrderModel();
            model.Listorder = list;
            List<Office> lstoffname = offservice.GetOfficeName();
            SelectList namelist = new SelectList(lstoffname, "", "OfficeName");
            ViewBag.Listname = namelist;
            return View(model);
        }

        [HttpPost]
        public ActionResult Success(FilterModel model)
        {
            OrderService service = new OrderService();
            OfficeService offservice = new OfficeService();
            AccountService accservice = new AccountService();
            OrderModel order = new OrderModel();
            if (!String.IsNullOrEmpty(model.Name))
            {
                var name = accservice.GetNameByUserID(Convert.ToInt32(model.Name));
                var lstorder = service.GetFilter(name, model.OfficeName, model.FromDate, model.ToDate);
                order.Listorder = lstorder;
            }
            else
            {
                var lstorder = service.GetFilter(model.Name, model.OfficeName, model.FromDate, model.ToDate);
                order.Listorder = lstorder;
            }
            List<Office> lstoffname = offservice.GetOfficeName();
            SelectList namelist = new SelectList(lstoffname, "", "OfficeName");
            ViewBag.Listname = namelist;
            order.FromDate = model.FromDate;
            order.ToDate = model.ToDate;
            return View(order);
        }

        public ActionResult Edit(int ID)
        {
            OrderService service = new OrderService();
            var order = service.Getbykey(ID);
            var shift = service.GetShiftNameByShiftID(order.ShiftID);
            ViewBag.shiftname = shift;
            var office = service.GetOfficeNameByOfficeID(order.OfficeID);
            ViewBag.officename = office;
            var name = service.GetNameByOrderID(order.OrderID);
            ViewBag.name = name;
            var date = service.GetOrderDateByOrderID(order.OrderID);
            ViewBag.date = date;
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(Order model)
        {
            try
            {
                OrderService service = new OrderService();
                model.EditBy = HttpContext.User.Identity.Name;
                service.Update(model);
                return RedirectToAction("Index", "Order");
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult Delete(int ID)
        {
            OrderService service = new OrderService();
            try
            {
                if (ID < 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var order = service.Getbykey(ID);
                if (order == null)
                {
                    return HttpNotFound();
                }
                service.Delete(order.OrderID);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public List<Xuatdulieuanca> CreateDulieu(FilterModel filter)
        {
            OrderService service = new OrderService();
            var resultlist = new List<Xuatdulieuanca>();
            AccountService accservice = new AccountService();
            OrderModel order = new OrderModel();
            if (!String.IsNullOrEmpty(filter.Name))
            {
                var lstorder = service.GetFilter(filter.Name, filter.OfficeName, filter.FromDate, filter.ToDate);
                order.Listorder = lstorder;
            }
            else
            {
                var lstorder = service.GetFilter(filter.Name, filter.OfficeName, filter.FromDate, filter.ToDate);
                order.Listorder = lstorder;
            }
            foreach (var item in order.Listorder)
            {
                var a = new Xuatdulieuanca()
                {
                    OrderID = item.OrderID,
                    Name = item.Name,
                    Tenca = item.ShiftName,
                    Quantity = item.Quantity,
                    OrderDate = item.OrderDate
                };
                resultlist.Add(a);
            }
            return resultlist;
        }

        private Stream CreateExcelFile(FilterModel filter)
        {
            var list = CreateDulieu(filter);
            Stream stream = new MemoryStream();
            int total = 0;
            // Results Output
            MemoryStream output = new MemoryStream();
            var filePath = Path.Combine(HttpContext.Server.MapPath("~/App_Data/"), "anca_template.xlsx");
            FileInfo template = new FileInfo(filePath);
            // Read Template
            using (FileStream templateDocumentStream = System.IO.File.OpenRead(filePath))
            {
                using (var excelPackage = new ExcelPackage(template))
                {
                    var workSheet = excelPackage.Workbook.Worksheets[1];
                    BindingFormatForExcel(workSheet, list);
                    excelPackage.SaveAs(output);

                }
                return output;
            }
        }

        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<Xuatdulieuanca> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 10;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[4, 1].Value = "Phiếu số";
            worksheet.Cells[4, 2].Value = "Tên";
            worksheet.Cells[4, 3].Value = "Tên ca";
            worksheet.Cells[4, 4].Value = "Số lượng";
            worksheet.Cells[4, 5].Value = "Ngày đặt";

            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A4:E4"])
            {
                //// Set PatternType
                //range.Style.Fill.PatternType = ExcelFillStyle.DarkGray;
                //// Set Màu cho Background
                //range.Style.Fill.BackgroundColor.SetColor(Color.Aqua);
                // Canh giữa cho các text
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                range.Style.Font.SetFromFont(new Font("Arial", 12));
                // Set Border
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                //// Set màu ch Border
                //range.Style.Border.Bottom.Color.SetColor(Color.Blue);
                range.Style.Font.Bold = true;
            }

            for (int i = 0; i < listItems.Count; i++)
            {
                var item = listItems[i];
                worksheet.Cells[i + 5, 1].Value = item.OrderID;
                worksheet.Cells[i + 5, 2].Value = item.Name;
                worksheet.Cells[i + 5, 3].Value = item.Tenca;
                worksheet.Cells[i + 5, 4].Value = item.Quantity;
                worksheet.Cells[i + 5, 5].Value = item.OrderDate.ToString("dd-MM-yyyy");
                // Format lại color nếu như thỏa điều kiện
                //if (item.Money > 20050)
                //{
                //    // Ở đây chúng ta sẽ format lại theo dạng fromRow,fromCol,toRow,toCol
                //    using (var range = worksheet.Cells[i + 2, 1, i + 2, 4])
                //    {
                //        // Format text đỏ và đậm
                //        range.Style.Font.Color.SetColor(Color.Red);
                //        range.Style.Font.Bold = true;
                //    }
                //}
            }
            //worksheet.Cells[4, 1, listItems.Count + 5, 5].AutoFitColumns();
        }

        private DataTable ReadFromExcelfile(string path, string sheetName)
        {
            // Khởi tạo data table
            DataTable dt = new DataTable();
            // Load file excel và các setting ban đầu
            using (ExcelPackage package = new ExcelPackage(new FileInfo(path)))
            {
                if (package.Workbook.Worksheets.Count < 1)
                {
                    // Log - Không có sheet nào tồn tại trong file excel của bạn
                    return null;
                }
                // Khởi Lấy Sheet đầu tiện trong file Excel để truy vấn, truyền vào name của Sheet để lấy ra sheet cần, nếu name = null thì lấy sheet đầu tiên
                ExcelWorksheet workSheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == sheetName) ?? package.Workbook.Worksheets.FirstOrDefault();
                // Đọc tất cả các header
                foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
                {
                    dt.Columns.Add(firstRowCell.Text);
                }
                // Đọc tất cả data bắt đầu từ row thứ 2
                for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
                {
                    // Lấy 1 row trong excel để truy vấn
                    var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
                    // tạo 1 row trong data table
                    var newRow = dt.NewRow();
                    foreach (var cell in row)
                    {
                        newRow[cell.Start.Column - 1] = cell.Text;

                    }
                    dt.Rows.Add(newRow);
                }
            }
            return dt;
        }

        [HttpGet]
        public ActionResult Export(FilterModel filter)
        {
            try
            {
                // Gọi lại hàm để tạo file excel
                var stream = CreateExcelFile(filter);
                // Tạo buffer memory stream để hứng file excel
                var buffer = stream as MemoryStream;
                // Đây là content Type dành cho file excel, còn rất nhiều content-type khác nhưng cái này mình thấy okay nhất
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
                // File name của Excel này là ExcelDemo
                Response.AddHeader("Content-Disposition", "attachment; filename=Dulieusuatan.xlsx");
                // Lưu file excel của chúng ta như 1 mảng byte để trả về response
                Response.BinaryWrite(buffer.ToArray());
                // Send tất cả ouput bytes về phía clients
                Response.Flush();
                Response.End();
                // Redirect về luôn trang index :D
                return new HttpStatusCodeResult(200, "OK");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, "BadRequest");
            }
        }
    }
}