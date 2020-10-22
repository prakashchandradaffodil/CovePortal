using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cove.Application.Interfaces;
using Cove.ClassLibrary.Interfaces;

namespace Cove.Application.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepo _assetRepo;

        public AssetService(IAssetRepo assetRepo)
        {
            _assetRepo = assetRepo;
        }
        public async Task<string> SaveUserProfileAssets(string files)
        {
            return await _assetRepo.SaveUserProfileAssets(files);
        }

        public async Task<bool> DeleteUserProfileAssets(string files, string userId)
        {
            return await _assetRepo.DeleteUserProfileAssets(files,userId);
        }

    }
}
