using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cove.ClassLibrary.Data;
using Cove.ClassLibrary.Interfaces;
using Cove.ClassLibrary.Model;
using Cove.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cove.ClassLibrary.Repositories
{
    public class AccountRepository:IAccountRepo
    {
        private readonly UserManager<ApplicationUserIdentity> _user;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUserIdentity> _signInManager;
        private readonly ILogger<AccountRepository> _logger;


        public AccountRepository(ILogger<AccountRepository> logger, UserManager<ApplicationUserIdentity> user,
           SignInManager<ApplicationUserIdentity> signInManager, ApplicationDbContext context)
        {
            _logger = logger;
            _user = user;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<string> Login(LoginModel loginModel)
        {
            var user = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, true);
            var userId = await _context.UserProfile.Where(s => s.Email == loginModel.Email).FirstOrDefaultAsync();
            if (user.Succeeded&&userId!=null)
            {
                return userId.UserId+","+userId.Email+","+userId.Phone;
            }
            else
            {
                return null;
            }
        }


        public async Task<List<AuthenticationScheme>> GetAuthenticationSchemes()
        {
           var list= (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return list;
        }


        public async Task<UserProfile> Register(RegisterModel registerModel)
        {
            var user1 = await _user.FindByEmailAsync(registerModel.Email);

            if (user1 == null)
            {
                var adduser = new ApplicationUserIdentity
                {
                    Email = registerModel.Email,
                    UserName = registerModel.Email,
                };
                var result = await _user.CreateAsync(adduser, registerModel.Password);
                if (result.Succeeded)
                {
                    var roleId = await _context.ApplicationRole.Where(s => s.Name == registerModel.Role).FirstOrDefaultAsync();
                    if (registerModel.Role == "Reader")
                    {
                        var user = new UserProfile
                        {
                            FirstName = registerModel.FirstName,
                            LastName = registerModel.LastName,
                            Address = registerModel.Address,
                            DateOfBirth = registerModel.DateOfBirth.ToString(),
                            Email = registerModel.Email,
                            Phone = registerModel.Phone,
                            UserId = adduser.Id,
                            RoleId =roleId.Id
                    };
                        await _context.UserProfile.AddAsync(user);
                        await _context.SaveChangesAsync();
                        return user;
                    }
                    else // Creator
                    {
                        var user2 = new UserProfile
                        {
                            FirstName = registerModel.FirstName,
                            LastName = registerModel.LastName,
                            Address = registerModel.Address,
                            DateOfBirth = registerModel.DateOfBirth.ToString(),
                            Email = registerModel.Email,
                            Phone = registerModel.Phone,
                            UserId = adduser.Id,
                            RoleId =roleId.Id,

                            Profile=registerModel.Profile,
                            Links=registerModel.Links,
                            Specialisations=registerModel.Specialisations,
                            //FilePaths=registerModel.FilePath
                    };
                        if (registerModel.AssetIds != null)
                        {
                            foreach (var file in registerModel.AssetIds.Split(","))
                            {
                                var assetUserMap = new UserProfileAssets
                                {
                                    AssetId = file,
                                    UserId = adduser.Id
                                };
                                await _context.UserProfileAssets.AddAsync(assetUserMap);
                            }
                        }
                        await _context.UserProfile.AddAsync(user2);
                        await _context.SaveChangesAsync();
                        return user2;
                    }
                   
                }
            }
            else
            {
                await _user.AddPasswordAsync(user1, registerModel.Password);
                var roleId = await _context.ApplicationRole.Where(s => s.Name == registerModel.Role).FirstOrDefaultAsync();
                if (registerModel.Role == "Reader")
                {
                    var user = new UserProfile
                    {
                        FirstName = registerModel.FirstName,
                        LastName = registerModel.LastName,
                        Address = registerModel.Address,
                        DateOfBirth = registerModel.DateOfBirth.ToString(),
                        Email = registerModel.Email,
                        Phone = registerModel.Phone,
                        UserId = user1.Id,
                        RoleId =roleId.Id
                };

                    await _context.UserProfile.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return user;
                }
                else // Creator
                {

                    var user = new UserProfile
                    {
                        FirstName = registerModel.FirstName,
                        LastName = registerModel.LastName,
                        Address = registerModel.Address,
                        DateOfBirth = registerModel.DateOfBirth.ToString(),
                        Email = registerModel.Email,
                        Phone = registerModel.Phone,
                        UserId = user1.Id,
                        RoleId =roleId.Id,

                        Profile=registerModel.Profile,
                        //FilePaths=registerModel.FilePath,
                        Links=registerModel.Links,
                        Specialisations=registerModel.Specialisations
                };
                    if (registerModel.AssetIds != null)
                    {
                        foreach (var file in registerModel.AssetIds.Split(","))
                        {
                            var assetUserMap = new UserProfileAssets
                            {
                                AssetId = file,
                                UserId = user1.Id
                            };
                            await _context.UserProfileAssets.AddAsync(assetUserMap);
                        }
                    }

                    await _context.UserProfile.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return user;
                }

            }
            return new UserProfile();
        }

        


        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider,string redirectUrl)
        {
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return properties;
        }



        public async Task<bool> AddExternalLoginUser(string email,ExternalLoginInfo info)
        {
            var user = await _user.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUserIdentity
                {
                    UserName = email,
                    Email = email
                };
                await _user.CreateAsync(user);
            }
            await _user.AddLoginAsync(user, info);
            await _signInManager.SignInAsync(user, isPersistent: false);
            return true;
        }


        public async Task<UserProfile> FindUserByEmail(string email)
        {
           return await _context.UserProfile.Where(s => s.Email == email).FirstOrDefaultAsync();
        }
        public async Task<bool> ExternalLoginSignIn(ExternalLoginInfo info)
        {
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<ExternalLoginInfo> GetExternalLoginInfo()
        {
           return await _signInManager.GetExternalLoginInfoAsync();
        }


        public async Task<bool> PhoneAlreadyExists(string phone)
        {
            var user = await _context.UserProfile.Where(s => s.Phone == phone).FirstOrDefaultAsync();
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> EmailAlreadyExists(string email)
        {
            var user = await _context.UserProfile.Where(s => s.Email == email).FirstOrDefaultAsync();
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
