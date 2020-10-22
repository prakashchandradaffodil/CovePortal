using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cove.Application.Interfaces;
using Cove.Web.Models;
using Cove.Web.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Cove.Web.Controllers
{
    public class ManageAccountController : Controller
    {

        private readonly ILogger<AccountController> _logger;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IAssetService _assetService;


        public ManageAccountController(IUserService userService, ILogger<AccountController> logger,
          IWebHostEnvironment hostingEnvironment,
          IAssetService assetService,
          IConfiguration configuration)
        {
            _userService = userService;
            _logger = logger;
            _assetService = assetService;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        
        public IActionResult ManageAccountOptions()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Download(string filename)
        {
            try
            {
                if (filename == null)
                    return Content("filename not present");

                var path = Path.Combine(new string[] {
                               Directory.GetCurrentDirectory(),
                               "wwwroot", "UploadedImages", filename });

                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, GetContentType(path), Path.GetFileName(path));
            }catch(Exception e)
            {
                _logger.LogError("Exception:" + e);
                 return Content("filename not present");
            }
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".pdf", "application/pdf"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"}
            };
        }

        public async Task<IActionResult> AccountDetails()
        {
            var userId=Request.Cookies["UserId"];
            var user = await _userService.GetUserById(userId);
            RegisterViewModel model = new RegisterViewModel();

            if (user.RoleId == "Reader")
            {
                model = new RegisterViewModel
                {
                    Role = user.RoleId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Address = user.Address,
                    Phone = user.Phone
                };
            }
            else //Creator
            {
                var files = await _userService.GetUserProfileAssets(userId);
                string uploadedFiles = null;
                //var allfiles = Directory.GetFiles()
                if (files.Split(",").Length == 1 && files.Split(",")[0] == "")
                {
                    uploadedFiles = null;
                }
                else
                {
                    uploadedFiles = files;
                }


                model = new RegisterViewModel
                {
                    Role = user.RoleId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Address = user.Address,
                    Phone = user.Phone,

                    //Files=allfiles,
                    UploadedFiles= uploadedFiles,
                    Profile=user.Profile,
                    Links=user.Links.Split(","),
                    Specialisations=user.Specialisations.Split(",")
                };

            }
              var specialisations = _configuration.GetSection("Specialisation").Value.Split(",");
            ViewBag.Specialisation = specialisations;
            
            return PartialView("AccountDetails",model);
        }

        [HttpPost]
        public async Task<string> UploadFile(IList<IFormFile> file,string[] uploadedChangedFiles,string[] initialFiles)
        {
          

            string uniqueFileName = null;
            string files = null;
            string result = null;
            if (file != null && file.Count > 0)
            {
                foreach (IFormFile photo in file)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "UploadedImages");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    files = files + uniqueFileName + ",";
                }
                files = files.Substring(0, files.Length - 1);
                result = await _assetService.SaveUserProfileAssets(files);
            }

            var deleteFiles = "";
            //compare uploadedChangedFiles and initialFiles
            if (initialFiles == uploadedChangedFiles&&file.Count==0)
            {
                //no change in assets
                return null;
            }
            foreach (var initialFile in initialFiles)
            {
                if (uploadedChangedFiles.Any(s => s == initialFile))
                {
                    continue;
                }
                else
                {
                    deleteFiles = deleteFiles + initialFile + ",";
                }
            }
            var userId = Request.Cookies["UserId"];
            if (deleteFiles != "")
            {
                var result1 = await _assetService.DeleteUserProfileAssets(deleteFiles.Substring(0, deleteFiles.Length - 1), userId);
            }
            // no files
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> EditAccountDetails(RegisterViewModel registerModel)
        {
            var specialisations = _configuration.GetSection("Specialisation").Value.Split(",");
            ViewBag.Specialisation = specialisations;

            var userId = Request.Cookies["UserId"];
            if (ModelState.IsValid)
            {
                try
                {
                    var model = new RegisterModel();
                    if (registerModel.Role == "Creator")
                    {
                        model = new RegisterModel
                        {
                            UserId=userId,
                            Role = registerModel.Role,
                            FirstName = registerModel.FirstName,
                            LastName = registerModel.LastName,
                            Email = registerModel.Email,
                            //Password = registerModel.Password,
                            Address = registerModel.Address,
                            //DateOfBirth = registerModel.DateOfBirth,
                            Phone = registerModel.Phone,

                            Profile = registerModel.Profile,
                            //FilePath = uniqueFileName,
                            Specialisations = string.Join(",", registerModel.Specialisations),
                            //Links = string.Join(",", registerModel.Links),
                            AssetIds = registerModel.AssetIds
                        };
                    }
                    else
                    {
                        model = new RegisterModel
                        {
                            UserId=userId,
                            Role = registerModel.Role,
                            FirstName = registerModel.FirstName,
                            LastName = registerModel.LastName,
                            Email = registerModel.Email,
                            Address = registerModel.Address,
                            Phone = registerModel.Phone
                        };
                    }

                    var result = await _userService.EditUserAccountDetails(model);
                    if (result)
                    {
                        ViewBag.Message = "Updates Saved";
                        return View("ManageAccountOptions");
                    }
                    else
                    {
                        //var specialisation = _configuration.GetSection("Specialisation").Value.Split(",");
                        //ViewBag.Specialisation = specialisation;
                        ViewBag.ErrorMessage = "Error in Updating Details";
                        return Ok("Error");
                    }
                }
                catch(Exception e)
                {
                    _logger.LogError("Error:" + e);
                    return View("ManageAccountOptions");
                }
            }
            return View("ManageAccountOptions",registerModel);
        }
    }
}
