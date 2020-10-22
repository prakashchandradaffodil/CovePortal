using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cove.Application.Interfaces
{
    public interface IAssetService
    {
        public Task<string> SaveUserProfileAssets(string files);
        public Task<bool> DeleteUserProfileAssets(string files,string userId);
    }
}
