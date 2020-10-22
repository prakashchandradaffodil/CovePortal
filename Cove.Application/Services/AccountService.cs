using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cove.Application.Interfaces;
using Cove.ClassLibrary.Interfaces;
using Cove.ClassLibrary.Model;
using Cove.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Cove.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _accountRepo;

        public AccountService(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public async Task<UserProfile> Register(RegisterModel registerModel)
        {
            return await _accountRepo.Register(registerModel);
        }

        public async Task<List<AuthenticationScheme>> GetAuthenticationSchemes()
        {
            return await _accountRepo.GetAuthenticationSchemes();
        }
        public async Task<string> Login(LoginModel loginModel)
        {
            return await _accountRepo.Login(loginModel);
        }

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
        {
            return _accountRepo.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfo()
        {
            return await _accountRepo.GetExternalLoginInfo();
        }

        public async Task<bool> ExternalLoginSignIn(ExternalLoginInfo info)
        {
            return await _accountRepo.ExternalLoginSignIn(info);
        }

        public async Task<UserProfile> FindUserByEmail(string email)
        {
            return await _accountRepo.FindUserByEmail(email);
        }
        public async Task<bool> AddExternalLoginUser(string email, ExternalLoginInfo info)
        {
            return await _accountRepo.AddExternalLoginUser(email, info);
        }
        public async Task<bool> PhoneAlreadyExists(string phone)
        {
            return await _accountRepo.PhoneAlreadyExists(phone);
        }

        public async Task<bool> EmailAlreadyExists(string email)
        {
            return await _accountRepo.EmailAlreadyExists(email);
        }
    }
}
