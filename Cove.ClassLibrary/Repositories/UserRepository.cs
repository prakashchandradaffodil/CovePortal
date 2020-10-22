using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cove.ClassLibrary.Data;
using Cove.ClassLibrary.Interfaces;
using Cove.ClassLibrary.Model;
using Cove.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cove.ClassLibrary.Repositories
{
    public class UserRepository:IUserRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserRepository> _logger;


        public UserRepository(ILogger<UserRepository> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<UserProfile> GetUserById(string id)
        {
            if (id != null)
            {
                var user = await _context.UserProfile.Where(s => s.UserId == id).FirstOrDefaultAsync();
                if (user != null)
                {
                    var role = await _context.ApplicationRole.Where(s => s.Id == user.RoleId).FirstOrDefaultAsync();
                    user.RoleId = role.Name;
                    return user;
                }
            }
            return new UserProfile();
        }

        public async Task<bool> EditUserAccountDetails(RegisterModel registerModel)
        {
            var user = await _context.UserProfile.Where(s => s.UserId == registerModel.UserId).FirstOrDefaultAsync();
            if (user != null)
            {
                if (registerModel.Role == "Reader")
                {
                    user.FirstName = registerModel.FirstName;
                    user.LastName = registerModel.LastName;
                    user.Email = registerModel.Email;
                    user.Address = registerModel.Address;
                    user.Phone = registerModel.Phone;

                    var identityUser = await _context.ApplicationUserIdentity.Where(s => s.Id == registerModel.UserId).FirstOrDefaultAsync();
                    identityUser.Email = registerModel.Email;
                    identityUser.NormalizedEmail = registerModel.Email.ToUpper();
                    identityUser.UserName = registerModel.Email;
                    identityUser.NormalizedUserName = registerModel.Email.ToUpper();



                    var contextChanges = _context.UserProfile.Attach(user);
                    contextChanges.State = EntityState.Modified;

                    var contextChangesUserIdenity = _context.ApplicationUserIdentity.Attach(identityUser);
                    contextChangesUserIdenity.State = EntityState.Modified;
                    var result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        return true;
                    }
                }
                else //Creator
                {
                    user.FirstName = registerModel.FirstName;
                    user.LastName = registerModel.LastName;
                    user.Email = registerModel.Email;
                    user.Address = registerModel.Address;
                    user.Phone = registerModel.Phone;
                    user.Profile = registerModel.Profile;
                    user.Specialisations = registerModel.Specialisations;
                    //code to change files/assets


                    var identityUser = await _context.ApplicationUserIdentity.Where(s => s.Id == registerModel.UserId).FirstOrDefaultAsync();
                    identityUser.Email = registerModel.Email;
                    identityUser.NormalizedEmail = registerModel.Email.ToUpper();
                    identityUser.UserName = registerModel.Email;
                    identityUser.NormalizedUserName = registerModel.Email.ToUpper();

                    var contextChanges = _context.UserProfile.Attach(user);
                    contextChanges.State = EntityState.Modified;

                    var contextChangesUserIdenity = _context.ApplicationUserIdentity.Attach(identityUser);
                    contextChangesUserIdenity.State = EntityState.Modified;

                    if (registerModel.AssetIds != null)
                    {
                        foreach (var file in registerModel.AssetIds.Split(","))
                        {
                            var assetUserMap = new UserProfileAssets
                            {
                                AssetId = file,
                                UserId = registerModel.UserId
                            };
                            await _context.UserProfileAssets.AddAsync(assetUserMap);
                        }
                    }

                    var result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public async Task<string> GetUserProfileAssets(string id)
        {
            var assetValues = "";
            var assets = await _context.UserProfileAssets.Where(s => s.UserId == id).Select(s=>s.AssetId).ToListAsync();
            if (assets.Count==0)
            {
                return assetValues;
            }

            foreach(var asset in assets)
            {
                var file = await _context.Assets.Where(s => s.AssetId.ToString().ToLower() == asset.ToLower()).Select(s => s.AssetValue).FirstOrDefaultAsync();
                if (file != null)
                {
                    assetValues = assetValues + file + ",";
                }
            }

           return assetValues.Substring(0, assetValues.Length - 1);
        }

    }
}
