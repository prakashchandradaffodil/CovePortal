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

namespace Cove.ClassLibrary.Repositories
{
    public class AssetRepository:IAssetRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AssetRepository> _logger;


        public AssetRepository(ILogger<AssetRepository> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<string> SaveUserProfileAssets(string files)
        {
            var file = files.Split(",");
            var assetIds = "";
            foreach(var f in file)
            {
                var asset = new Assets { AssetValue = f };
               var result= await _context.Assets.AddAsync(asset);
                assetIds = assetIds + result.Entity.AssetId+",";
            }
            await _context.SaveChangesAsync();
            return assetIds.Substring(0,assetIds.Length-1);
        }

        public async Task<bool> DeleteUserProfileAssets(string files, string userId)
        {
            foreach(var file in files.Split(","))
            {
                var asset = await _context.Assets.Where(s => s.AssetValue == file).FirstOrDefaultAsync();
                if(asset!=null)
                _context.Assets.RemoveRange(asset);

                var userasset = await _context.UserProfileAssets.Where(s => s.AssetId.ToLower() == asset.AssetId.ToString()).FirstOrDefaultAsync();
                if(userasset!=null)
                _context.UserProfileAssets.RemoveRange(userasset);
            }

                await _context.SaveChangesAsync();
                return true;
        }

    }
}
