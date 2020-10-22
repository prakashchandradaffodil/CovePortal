using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cove.Application.Interfaces;
using Cove.ClassLibrary.Interfaces;
using Cove.ClassLibrary.Model;
using Cove.Web.Models;

namespace Cove.Application.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<UserProfile> GetUserById(string id)
        {
            return await _userRepo.GetUserById(id);
        }

        public async Task<bool> EditUserAccountDetails(RegisterModel registerModel)
        {
            return await _userRepo.EditUserAccountDetails(registerModel);
        }

        public async Task<string> GetUserProfileAssets(string id)
        {
            return await _userRepo.GetUserProfileAssets(id);
        }
    }
}
