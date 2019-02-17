using chatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using RabbitMQ.Client;
using RabbitMQ.Util;
namespace chatApp.Controllers
{
    public class HomeController : Controller
    {
        DataLayer dl = new DataLayer();
        // GET: Home  
        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public JsonResult sendmsg(string message, string friend)
        {
            RabbitMQBll obj = new RabbitMQBll();
            IConnection con = obj.GetConnection();
            bool flag = obj.send(con, message, friend);
            return Json(null);
        }
        [HttpPost]
        public JsonResult receive()
        {
            try
            {
                RabbitMQBll obj = new RabbitMQBll();
                IConnection con = obj.GetConnection();
                string userqueue = Session["username"].ToString();
                string message = obj.receive(con, userqueue);
                return Json(message);
            }
            catch (Exception)
            {

                return null;
            }


        }
        public ActionResult login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult login(FormCollection fc)
        {
            string email = fc["txtemail"].ToString();
            string password = fc["txtpassword"].ToString();
            userModel user = dl.login(email, password);
            if (user.userid > 0)
            {
                ViewData["status"] = 1;
                ViewData["msg"] = "login Successful...";
                Session["username"] = user.email;
                Session["userid"] = user.userid.ToString();
                return RedirectToAction("Index");
            }
            else
            {

                ViewData["status"] = 2;
                ViewData["msg"] = "invalid Email or Password...";
                return View();
            }

        }

        [HttpPost]
        public JsonResult friendlist()
        {
            int id = Convert.ToInt32(Session["userid"].ToString());
            List<userModel> users = dl.getusers(id);
            List<ListItem> userlist = new List<ListItem>();
            foreach (var item in users)
            {
                userlist.Add(new ListItem
                {
                    Value = item.email.ToString(),
                    Text = item.email.ToString()

                });
            }
            return Json(userlist);
        }
    }
}