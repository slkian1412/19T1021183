using _19T1021183.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _19T1021183.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Trang đăng nhập vào hệ thống
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(string userName = "", string password = "")
        {
            ViewBag.UserName = userName;
            if(string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "Vui lòng nhập đủ thông tin");
                return View();
            }
            var userAccount = UserAccountService.Authorize(AccountTypes.Employee, userName, password);
            if (userAccount == null)
            {
                ModelState.AddModelError("", "Đăng nhập thất bại");
                return View();
            }
            string cookieValue = Newtonsoft.Json.JsonConvert.SerializeObject(userAccount);
            FormsAuthentication.SetAuthCookie(cookieValue, false);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}