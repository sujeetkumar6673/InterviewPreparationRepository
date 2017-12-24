using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InterViewPreparation.Models;

namespace InterViewPreparation.Controllers
{
    public class RegisterController : Controller
    {
        InterviewPreparationEntities1 db = new InterviewPreparationEntities1();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SaveData(Person model)
        {
            db.Persons.Add(model);
            db.SaveChanges();
            return Json("Registration Successful", JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckVaildUser(Person model)
        {
            string result = "Fail";
            var DataItem = db.Persons.Where(x => x.EmailId == model.EmailId && x.Password == model.Password).SingleOrDefault();
            if (DataItem != null)
            {
                Session["UserID"] = DataItem.ID.ToString();
                Session["UserName"] = DataItem.UserName.ToString();
                result = "Success";
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult AfterLogin()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");

            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }
        
    }
}
