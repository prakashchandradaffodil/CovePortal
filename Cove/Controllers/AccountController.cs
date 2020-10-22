using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cove.Application.Interfaces;
using Cove.Web.Models;
using Cove.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Cove.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        private readonly IAssetService _assetService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;



        public AccountController(IAccountService accountService,ILogger<AccountController> logger,
            IAssetService assetService,
            IWebHostEnvironment hostingEnvironment,
            IConfiguration configuration)
        {
            _accountService = accountService;
            _assetService = assetService;
            _logger = logger;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("Phone");
            Response.Cookies.Delete("Email");
            Response.Cookies.Delete("Password");
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var list = await _accountService.GetAuthenticationSchemes();
            LoginModel model = new LoginModel
            {
                Email = Request.Cookies["Email"],
                Password = Request.Cookies["Password"],
                ReturnUrl = returnUrl,
                ExternalLogins = list
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> LoginPost(LoginModel loginModel)
        {
            var list = await _accountService.GetAuthenticationSchemes();

            if (ModelState.IsValid)
            {

                if (loginModel.RememberMe)
                {
                    //save user cred
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddHours(24);
                    Response.Cookies.Append("Email", loginModel.Email, options);
                    Response.Cookies.Append("Password", loginModel.Password, options);
                }
                else
                {
                    //unsave user cred
                    Response.Cookies.Delete("Email");
                    Response.Cookies.Delete("Password");
                }

                var user=await _accountService.Login(loginModel);
                if (user != null)
                {
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddHours(24);
                    var details = user.Split(",");
                    Response.Cookies.Append("UserId", details[0], options);
                    Response.Cookies.Append("Email", details[1], options);
                    Response.Cookies.Append("Phone", details[2], options);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid Credentials";
                }

                LoginModel model = new LoginModel
                {
                    ReturnUrl = Url.Content("~/"),
                    ExternalLogins = list
                };
                return View("Login", model);
            }
            else
            {
                LoginModel model = new LoginModel
                {
                    ReturnUrl = Url.Content("~/"),
                    ExternalLogins = list
                };
                return View("Login", model);
            }
        }

        [AllowAnonymous]
        public IActionResult ExternalLoginProvider(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action("OnGetCallbackAsync", "Account", new { ReturnUrl = returnUrl });
            var properties=_accountService.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }


        [AllowAnonymous]
        [HttpGet("Account/OnGetCallbackAsync")]
        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            var list = await _accountService.GetAuthenticationSchemes();

            LoginModel model = new LoginModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = list
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, "Error from external provider:" + remoteError);
                return View("Login", model);
            }

            var info = await _accountService.GetExternalLoginInfo();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");
                return View("Login", model);
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _accountService.ExternalLoginSignIn(info);
            if (result)
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var user = await _accountService.FindUserByEmail(email);
                if (user == null)
                {
                    return RedirectToAction("Register", "Account", new { Email = email });
                }
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddHours(24);
                Response.Cookies.Append("UserId", user.UserId, options);
                Response.Cookies.Append("Email", user.Email, options);
                Response.Cookies.Append("Phone", user.Phone==null?"":user.Phone, options);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (email != null)
                {
                    await _accountService.AddExternalLoginUser(email, info);
                    return RedirectToAction("Register", "Account",new { Email =email});
                }
                ViewBag.ErrorTitle = "Email not received from:" + info.LoginProvider;
                ViewBag.ErrorMessage = "Please contact support.";
                return View("Error");

            }

        }

        [HttpGet("/Account/Register")]
        public IActionResult Register(string Email)
        {
            var registerViewModel = new RegisterViewModel { Email =Email };
                var specialisations = _configuration.GetSection("Specialisation").Value.Split(",");
            ViewBag.Specialisation = specialisations;
                //registerViewModel.SpecialisationsListDDL = specialisations.Select(s => new SelectListItem { Text = s, Value = s });
            return View("Register", registerViewModel);
        }

        [HttpPost]
        public async Task<string> UploadFile(IList<IFormFile> file)
        {
            string uniqueFileName = null;
            string files = null;
            if (file != null && file.Count > 0)
            {
                foreach (IFormFile photo in file)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "UploadedImages");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    files =files+ uniqueFileName + ",";
                }
                files = files.Substring(0, files.Length - 1);
                var result = await _assetService.SaveUserProfileAssets(files);
                return result;
            }
            // no files
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                   var model = new RegisterModel();
                    if (registerModel.Role=="Creator")
                    {
                         model = new RegisterModel
                        {
                            Role = registerModel.Role,
                            FirstName = registerModel.FirstName,
                            LastName = registerModel.LastName,
                            Email = registerModel.Email,
                            Password = registerModel.Password,
                            Address = registerModel.Address,
                            DateOfBirth = registerModel.DateOfBirth,
                            Phone = registerModel.Phone,

                            Profile=registerModel.Profile,
                            //FilePath = uniqueFileName,
                             Specialisations = string.Join(",", registerModel.Specialisations),
                             Links = string.Join(",", registerModel.Links),
                             AssetIds=registerModel.AssetIds
                        };
                    }
                    else
                    {
                         model = new RegisterModel
                        {
                            Role = registerModel.Role,
                            FirstName = registerModel.FirstName,
                            LastName = registerModel.LastName,
                            Email = registerModel.Email,
                            Password = registerModel.Password,
                            Address = registerModel.Address,
                            DateOfBirth = registerModel.DateOfBirth,
                            Phone = registerModel.Phone
                        };
                    }
                   
                   var result= await _accountService.Register(model);
                    if (result!=null)
                    {
                        //set userid in cookie
                        CookieOptions options = new CookieOptions();
                        options.Expires = DateTime.Now.AddHours(24);
                        Response.Cookies.Append("UserId", result.UserId, options);
                        Response.Cookies.Append("Email", result.Email, options);
                        Response.Cookies.Append("Phone", result.Phone==null?"":result.Phone, options);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        var specialisation = _configuration.GetSection("Specialisation").Value.Split(",");
                        ViewBag.Specialisation = specialisation;
                        return View("Register");
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("Error:" + e);
                    return View("Register");
                }
            }
            var specialisations = _configuration.GetSection("Specialisation").Value.Split(",");
            ViewBag.Specialisation = specialisations;
            return View("Register",registerModel);
        }

        public async Task<IActionResult> Welcome()
        {
            var email=Request.Cookies["Email"];
            var password=Request.Cookies["Password"];
            if (email != null && password != null)
            {
                var loginModel = new LoginModel { Email = email, Password = password };
             return await LoginPost(loginModel);
            }

            return View();
        }

        public async Task<JsonResult> PhoneAlreadyExists([Bind(Prefix = "Phone")] string phone)
        {
            //if (phone != null)
            //{
            //    if (value != null)
            //    {
            //        if (value.Trim().ToLower() == phone.Trim().ToLower())
            //            return Json(true);
            //    }
            //}
            var currentphone = Request.Cookies["Phone"];
            if (currentphone != null)
            {
                if (phone.Trim().ToLower() == currentphone.Trim().ToLower())
                {
                    return Json(true);
                }
            }


            var exists= await _accountService.PhoneAlreadyExists(phone);
            return Json(!exists);
        }
        
        public async Task<JsonResult> EmailAlreadyExists(string email)
        {
            var currentemail = Request.Cookies["Email"];
            if (currentemail != null)
            {
                if (email.Trim().ToLower() == currentemail.Trim().ToLower())
                {
                    return Json(true);
                }
            }

            var exists= await _accountService.EmailAlreadyExists(email);
           return Json(!exists);
        }




    }
}
