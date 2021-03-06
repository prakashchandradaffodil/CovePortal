﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cove.ClassLibrary.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Cove.ClassLibrary.Interfaces
{
    public interface IUploadComicRepo
    {
        public IEnumerable<SelectListItem> GetComicUploadFictionList();
        public IEnumerable<SelectListItem> GetComicUploadNonFictionList();
        public IEnumerable<SelectListItem> GetComicUploadContentTypeList();
        public IEnumerable<SelectListItem> GetComicUploadTagTypeList();
        public IEnumerable<SelectListItem> GetComicUploadAgeAvailabilityList();
        public Task<bool> UploadComic(UploadComic uploadComicModel);
    }
}
