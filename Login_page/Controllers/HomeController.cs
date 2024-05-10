using Login_page.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Login_page.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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
        public ActionResult Dashboard()
        {
            if (Session["user"] != null)
            {
                List<BaseEquipment> plsData = BaseEquipment.ListEquipmentData();
                DataTable dt = BaseCustomer.ListCustomer_Equipment();

                ViewBag.dt = dt;
                ViewBag.plstData = plsData;
               
                ViewBag.txtName = "";
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpPost]
        public ActionResult Dashboard(FormCollection f,string btnSubmit)
        {
            if (btnSubmit == "Save Equipment")
            {
                BaseEquipment baseEquipment = new BaseEquipment();
                baseEquipment.Name = f["ddlEquipmentName"].ToString();
                baseEquipment.EcCount = Convert.ToInt32(f["txtQuantity"].ToString());
                baseEquipment.EntryDate = Convert.ToDateTime(f["txtEntryDate"].ToString());
                int returnresult = baseEquipment.SaveEquipment();
                if (returnresult > 0)
                {
                    @ViewBag.OperationResult = "Save Successfully";
                }


            }
            if(btnSubmit== "Save Assignment")
            {
                int CustomerID = Convert.ToInt32(f["ddlPartialCustomer"].ToString()); 
                int Equipment_ID = Convert.ToInt32(f["ddlPartialEquipment"].ToString()); 
                int quantity = Convert.ToInt32(f["txtPartialEquiQuantity"].ToString());     
                 
                BaseCustomer.SaveAssignment(CustomerID,Equipment_ID, quantity);
                @ViewBag.OperationResult = "Save Successfully";
            }


            List<BaseEquipment> plstData = BaseEquipment.ListEquipmentData();
            ViewBag.plstData = plstData;
            DataTable dt = BaseCustomer.ListCustomer_Equipment();
            ViewBag.dt = dt; 
            ViewBag.txtName = "";
            if(btnSubmit=="Search")
            {
                ViewBag.txtName = f["txtName"].ToString();
            }
            ViewBag.FormCollection = f;
            return View();
        }

      //Api
        [HttpGet]
         public ActionResult BsEquipment() {

            List<BaseEquipment> plsData = BaseEquipment.ListEquipmentData();
            var plist=(from p in plsData select new
            {
                Equipment_ID=p.Equipment_ID,
                Name =p.Name,
                EcCount=p.EcCount,
                EntryDate=p.EntryDate.ToString("dd/MM/yyyy")
            }).ToList();
               return Json(plist, JsonRequestBehavior.AllowGet);
            

             }
        [HttpGet]
       public ActionResult LsCustomer()
        {
            List<BaseCustomer> listCustomer = BaseCustomer.ListCustomer();
            return Json(listCustomer, JsonRequestBehavior.AllowGet);
        }

    }
}