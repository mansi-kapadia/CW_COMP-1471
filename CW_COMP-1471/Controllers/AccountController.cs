﻿using CW_COMP_1471.Models;
using CW_COMP_1471.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CW_COMP_1471.Controllers
{
    public class AccountController : Controller
    {
        private static IRoleService roleService;
        private static IUserService userService;

        public AccountController(IRoleService _roleService, IUserService _userService)
        {
            // dependency injection
            roleService = _roleService;
            userService = _userService;
        }

        // Show Login Page
        public IActionResult Login()
        {
            Response.Cookies.Delete("UserSession");
            HttpContext.Session.Remove("LoggedInUser");
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (userService.CheckPassword(model))
            {
                User user = userService.GetUserByUsername(model.Username);

                if (user != null)
                {
                    SaveUserCookie(HttpContext, user.Id);
                    HttpContext.Session.SetInt32("LoggedInUser", user.Id);
                    int? sessionUserId = HttpContext.Session.GetInt32("LoggedInUser");
                }

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Invalid credentials";
            return View();

        }

        // Display the Register Form
        [HttpGet]
        public IActionResult Register()
        {
            var model = userService.GetRegisterModel();
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                var isRegistered = userService.RegisterUser(model);
                if (!isRegistered)
                {
                    ModelState.AddModelError("UserName", "Username already exists.");
                    model.Roles = new SelectList(userService.GetRegisterModel().Roles, "Value", "Text");
                    return View(model);
                }

                return RedirectToAction("Login"); 
            }

            // Reload Roles if validation fails
            model.Roles = new SelectList(userService.GetRegisterModel().Roles, "Value", "Text");

            return View(model);
        }

        // Remove cookie and Session details 
        public IActionResult Logout()
        {
            // Remove the cookie
            Response.Cookies.Delete("UserSession"); 
            HttpContext.Session.Remove("LoggedInUser");
            return RedirectToAction("Login", "Account");
        }

        // Check if user has logged in
        public static bool IsUserLoggedIn(HttpContext httpContext)
        {
            var userCookie = httpContext.Request.Cookies["UserSession"];
            var sessionUserId = httpContext.Session.GetInt32("LoggedInUser");
            if (userCookie == null && sessionUserId == null)
            {
                return false;
            }
            else if (userCookie != null)
            {
                var userId = Convert.ToInt32(userCookie);
                httpContext.Session.SetInt32("LoggedInUser", userId);
                return true;
            }
            else if (sessionUserId != null)
            {
                SaveUserCookie(httpContext, sessionUserId.Value);
                return true;
            }

            return false;
        }

        // Save Cookie details
        public static void SaveUserCookie(HttpContext httpContext, int userId)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddHours(1),
                HttpOnly = true,
                Secure = false,
                IsEssential = true,
            };
            httpContext.Response.Cookies.Append("UserSession", userId.ToString(), cookieOptions);
        }

        // Check if Current user is Admin
        public static bool IsCurrentUserAdmin(HttpContext httpContext)
        {
            var sessionUserId = httpContext.Session.GetInt32("LoggedInUser");
            if (sessionUserId != null)
            {
                var userService = httpContext.RequestServices.GetService(typeof(IUserService)) as IUserService;
                var user = userService?.GetUserById(sessionUserId.Value);
                if (user != null && user.RoleId == 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
