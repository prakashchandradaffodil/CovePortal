using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cove.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Cove.Web.Models.UploadComicViewModel;
using Microsoft.AspNetCore.Mvc;
using Cove.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Cove.Web.Controllers
{
    public class UploadComicController : Controller
    {
        private readonly ILogger<UploadComicController> _logger;
        private readonly IUploadComicService _uploadComicService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UploadComicController(IUploadComicService uploadComicService, ILogger<UploadComicController> logger,
          IWebHostEnvironment hostingEnvironment,
          IConfiguration configuration)
        {
            _uploadComicService = uploadComicService;
            _logger = logger;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet]
        public IActionResult UploadComic()
        {
            var uploadComicViewModel = new UploadComicViewModel();
            uploadComicViewModel.FictionListDDL = _uploadComicService.GetComicUploadFictionList();
            uploadComicViewModel.NonFictionListDDL = _uploadComicService.GetComicUploadNonFictionList();
            uploadComicViewModel.TagContentWarningListDDL = _uploadComicService.GetComicUploadContentTypeList();
            uploadComicViewModel.TagTypesListDDL = _uploadComicService.GetComicUploadTagTypeList();
            uploadComicViewModel.AgeSuitabilityListRBL = _uploadComicService.GetComicUploadAgeAvailabilityList();

            return View("UploadComic",uploadComicViewModel);
        }

        
        [HttpPost]
        public IActionResult UploadComic(UploadComicViewModel uploadComicViewModel)
        {
            if (ModelState.IsValid)
            {
              //prepare data to save as per required mapping
            }
            uploadComicViewModel.FictionListDDL = _uploadComicService.GetComicUploadFictionList();
            uploadComicViewModel.NonFictionListDDL = _uploadComicService.GetComicUploadNonFictionList();
            uploadComicViewModel.TagContentWarningListDDL = _uploadComicService.GetComicUploadContentTypeList();
            uploadComicViewModel.TagTypesListDDL = _uploadComicService.GetComicUploadTagTypeList();
            uploadComicViewModel.AgeSuitabilityListRBL = _uploadComicService.GetComicUploadAgeAvailabilityList();
            return View("UploadComic", uploadComicViewModel);
        }

        [HttpGet]
        public IActionResult Collaboration()
        {
            return View("Collaboration");
        }

       
    }
}