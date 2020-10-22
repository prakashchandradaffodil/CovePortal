using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cove.Application.Interfaces;
using Cove.ClassLibrary.Interfaces;
using Cove.ClassLibrary.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Cove.Application.Services
{
    public class UploadComicService : IUploadComicService
    {
        private readonly IUploadComicRepo _uploadComicRepo;

        public UploadComicService(IUploadComicRepo uploadComicRepo)
        {
            _uploadComicRepo = uploadComicRepo;
        }

        public  IEnumerable<SelectListItem> GetComicUploadFictionList()
        {
            return  _uploadComicRepo.GetComicUploadFictionList();
        }
        public IEnumerable<SelectListItem> GetComicUploadNonFictionList()
        {
            return _uploadComicRepo.GetComicUploadNonFictionList();
        }
        public IEnumerable<SelectListItem> GetComicUploadContentTypeList()
        {
            return _uploadComicRepo.GetComicUploadContentTypeList();
        }
        public IEnumerable<SelectListItem> GetComicUploadTagTypeList()
        {
            return _uploadComicRepo.GetComicUploadTagTypeList();
        }
        public IEnumerable<SelectListItem> GetComicUploadAgeAvailabilityList()
        {
            return _uploadComicRepo.GetComicUploadAgeAvailabilityList();
        }
        public Task<bool> UploadComic(UploadComic uploadComicModel)

        {
            return _uploadComicRepo.UploadComic(uploadComicModel);
        }

    }
}


