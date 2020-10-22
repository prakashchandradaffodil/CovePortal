using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cove.ClassLibrary.Interfaces
{
    public interface IAssetRepo
    {
        public Task<string> SaveUserProfileAssets(string files);
        public Task<bool> DeleteUserProfileAssets(string files, string userId);

    }
}
