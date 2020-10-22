using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cove.ClassLibrary.Model;
using Cove.Web.Models;

namespace Cove.Application.Interfaces
{
    public interface IUserService
    {
        public Task<UserProfile> GetUserById(string id);
        public Task<string> GetUserProfileAssets(string id);
        public Task<bool> EditUserAccountDetails(RegisterModel registerModel);


    }
}
