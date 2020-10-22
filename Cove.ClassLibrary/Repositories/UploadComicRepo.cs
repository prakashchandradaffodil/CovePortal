using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cove.ClassLibrary.Data;
using Cove.ClassLibrary.Interfaces;
using Cove.ClassLibrary.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cove.ClassLibrary.Migrations;

namespace Cove.ClassLibrary.Repositories
{
    public class UploadComicRepo : IUploadComicRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UploadComicRepo> _logger;


        public UploadComicRepo(ILogger<UploadComicRepo> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IEnumerable<SelectListItem> GetComicUploadFictionList()
        {
            IEnumerable<SelectListItem> fictionMasterList = null;
            try
            {
                fictionMasterList = _context.UploadComicFictionMaster
                                            .Select(i => new SelectListItem()
                                            {
                                                Text = i.Value,
                                                Value = i.Id.ToString()
                                                
                                            });


                return fictionMasterList.AsEnumerable<SelectListItem>();

            }
            catch (Exception ex)
            {
                // throw ex;
            }
            return fictionMasterList;
        }
        public IEnumerable<SelectListItem> GetComicUploadNonFictionList()
        {
            IEnumerable<SelectListItem> NonfictionMasterList = null;
            try
            {
                NonfictionMasterList = _context.UploadComicNonFictionMaster
                                            .Select(i => new SelectListItem()
                                            {
                                                Text = i.Value,
                                                Value = i.Id.ToString()
                                            });


                return NonfictionMasterList.AsEnumerable<SelectListItem>();

            }
            catch (Exception ex)
            {
                // throw ex;
            }
            return NonfictionMasterList;
        }
        public IEnumerable<SelectListItem> GetComicUploadContentTypeList()
        {
            IEnumerable<SelectListItem> ContentTypeMasterList = null;
            try
            {
                ContentTypeMasterList = _context.UploadComicContentTypeMaster
                                            .Select(i => new SelectListItem()
                                            {
                                                Text = i.Value,
                                                Value = i.Id.ToString()
                                            });


                return ContentTypeMasterList.AsEnumerable<SelectListItem>();

            }
            catch (Exception ex)
            {
                // throw ex;
            }
            return ContentTypeMasterList;

        }
        public IEnumerable<SelectListItem> GetComicUploadTagTypeList()
        {
            IEnumerable<SelectListItem> TagTypeMasterList = null;
            try
            {
                TagTypeMasterList = _context.UploadComicTagTypeMaster
                                    .Select(i => new SelectListItem()
                                    {
                                        Text = i.Value,
                                        Value = i.Id.ToString()
                                    });


                return TagTypeMasterList.AsEnumerable<SelectListItem>();

            }
            catch (Exception ex)
            {
                // throw ex;
            }
            return TagTypeMasterList;
        }
        public IEnumerable<SelectListItem> GetComicUploadAgeAvailabilityList()
        {
            IEnumerable<SelectListItem> ageAvialabilityMasterList = null;
            try
            {
                ageAvialabilityMasterList = _context.UploadComicAgeAvailabilityMaster
                                    .Select(i => new SelectListItem()
                                    {
                                        Text = i.Value,
                                        Value = i.Id.ToString()
                                    });


                return ageAvialabilityMasterList.AsEnumerable<SelectListItem>();

            }
            catch (Exception ex)
            {
                // throw ex;
            }
            return ageAvialabilityMasterList;

        }
        public async Task<bool> UploadComic(UploadComic uploadComicModel)
        {
            //if Role is creator then add comic and relevant entries in mapping tables also  
            if (uploadComicModel.Role == "Creator")
            {
                //store pdf upload file in asset and return its assetid as UploadComicAssetId
                var uploadComicData = new UploadComic()
                {
                    //
                    UploadComicAssetId = !string.IsNullOrEmpty(uploadComicModel.UploadComicAssetId) ? uploadComicModel.UploadComicAssetId : "todo",
                    Creator = uploadComicModel.Creator,
                    SeriesTitle = uploadComicModel.SeriesTitle,
                    IssueTitle = uploadComicModel.IssueTitle,
                    Price = uploadComicModel.Price,
                    IssueNumber = uploadComicModel.IssueNumber,
                    SelectedAgeSuitabilityId = uploadComicModel.SelectedAgeSuitabilityId,
                    Description = uploadComicModel.Description,
                    UploadComicThumbnailAssetId = !string.IsNullOrEmpty(uploadComicModel.UploadComicThumbnailAssetId) ? uploadComicModel.UploadComicThumbnailAssetId : "todo",
                    isPublishMyComic = uploadComicModel.isPublishMyComic,
                    Role = uploadComicModel.Role
                };
                await _context.UploadComic.AddAsync(uploadComicData);
                await _context.SaveChangesAsync();

            }

            return true;
        }

    }
}