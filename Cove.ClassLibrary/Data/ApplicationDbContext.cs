using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Cove.ClassLibrary.Model;
using Cove.ClassLibrary.Extensions;

namespace Cove.ClassLibrary.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<ApplicationUserIdentity> ApplicationUserIdentity { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<Assets> Assets { get; set; }
        public DbSet<UserProfileAssets> UserProfileAssets { get; set; }

        public DbSet<UploadComic> UploadComic { get; set; }
        public DbSet<UploadComicAgeAvailabilityMaster> UploadComicAgeAvailabilityMaster { get; set; }
        public DbSet<UploadComicFictionMaster> UploadComicFictionMaster { get; set; }
        public DbSet<UploadComicNonFictionMaster> UploadComicNonFictionMaster { get; set; }
        public DbSet<UploadComicContentTypeMaster> UploadComicContentTypeMaster { get; set; }
        public DbSet<UploadComicTagTypeMaster> UploadComicTagTypeMaster { get; set; }
        public DbSet<UploadedComicFiction> UploadedComicFiction { get; set; }
        public DbSet<UploadedComicNonFiction> UploadedComicNonFiction { get; set; }
        public DbSet<UploadedComicTagType> UploadedComicTagType { get; set; }
        public DbSet<UploadedComicContentType> UploadedComicContentType { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Uncomment this to seed data in database
            builder.Seed();
        }
    }
}
