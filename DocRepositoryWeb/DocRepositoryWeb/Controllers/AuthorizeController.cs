using DocRepositoryWeb.Models;
using ModelDomainDoc.Models;
using ModelDomainDoc.Services;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DocRepositoryWeb.Controllers
{
    public class AuthorizeController : Controller
    {
        private const string COOKIE_NAME = "ELMA_Cookies";
        private static IUserRepository userrepository { get; set; }

        public AuthorizeController()
        {
            if (userrepository == null)
            {
                userrepository = new UserRepository();
            }
        }

        // GET: Authorize
        [AllowAnonymous]
        public ActionResult Execute()
        {
            ViewData.Model = new AuthorizeModel();
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Execute(AuthorizeModel user)
        {
            if (ModelState.IsValid)
            {
                bool checkUser = userrepository.CheckUser(user.Login, user.Password);
                if (checkUser)
                {
                    var userFullName = userrepository.GetUserByLogin(user.Login).FullName;
                    FormsAuthentication.SetAuthCookie(user.Login, true);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public ActionResult Exit()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registr()
        {
            ViewData.Model = new RegistrModel();
            return View();
        }

        [HttpPost]
        public ActionResult Registr(RegistrModel regUser)
        {
            if (ModelState.IsValid)
            {
                ViewData.Model = regUser;
                return Result(regUser);
            }
                return View();
        }


        public ActionResult Result(RegistrModel regUser)
        {
            var result = "";
            if (ModelState.IsValid)
            {
                if (userrepository.GetUserByLogin(regUser.Login) != null)
                {
                    result = "Пользователь с таким логином уже существует";
                }
                else
                {
                    // Занесем пользователя в базу данных
                    var user = userrepository.Create();
                    user.FirstName = regUser.FirstName;
                    user.LastName = regUser.LastName;
                    user.Login = regUser.Login;
                    user.Password = regUser.Password;

                    userrepository.Update(user);
                    result = "Регистрация прошла успешно";
                }
            }
            ViewData.Model = result;
            return View("Result");
        }
    }
}