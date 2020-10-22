using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cove.ClassLibrary.Model;
using Cove.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Cove.ClassLibrary.Interfaces
{
    public interface IAccountRepo
    {
        public Task<UserProfile> Register(RegisterModel registerModel);
        public Task<string> Login(LoginModel loginModel);
        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl);
        public Task<List<AuthenticationScheme>> GetAuthenticationSchemes();
        public Task<ExternalLoginInfo> GetExternalLoginInfo();
        public Task<bool> ExternalLoginSignIn(ExternalLoginInfo info);
        public Task<UserProfile> FindUserByEmail(string email);
        public Task<bool> AddExternalLoginUser(string email, ExternalLoginInfo info);
        public Task<bool> PhoneAlreadyExists(string phone);
        public Task<bool> EmailAlreadyExists(string email);


    }
}
