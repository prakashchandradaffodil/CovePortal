using System;
using System.Collections.Generic;
using System.Text;
using Cove.ClassLibrary.Model;
using Microsoft.EntityFrameworkCore;

namespace Cove.ClassLibrary.Extensions
{
    public static class SeedDataModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Id = "1ba54582-b0f7-43f7-95b2-e9750fb282f3",
                    Name = "Reader",
                    NormalizedName = "READER",
                    ConcurrencyStamp = "302ef608-6855-4b47-be16-b57f8d62c545"
                }, new ApplicationRole
                {
                    Id = "28cf989c-5fe1-4fad-a03f-5c21baa352e9",
                    Name = "Creator",
                    NormalizedName = "CREATOR",
                    ConcurrencyStamp = "5ed9cb4b-8f26-4564-a0c9-d66a9fd6d425"
                }
            );

            modelBuilder.Entity<UploadComicAgeAvailabilityMaster>().HasData(
               new UploadComicAgeAvailabilityMaster
               {
                   Id=1,
                   Value = "All Ages"
               }, new UploadComicAgeAvailabilityMaster
               {
                   Id=2,
                   Value = "Teen and Up"
               }, new UploadComicAgeAvailabilityMaster
               {
                   Id=3,
                   Value = "Adult"
               });

            modelBuilder.Entity<UploadComicFictionMaster>().HasData(
              new UploadComicFictionMaster
              {
                  Id = 1,
                  Value = "Action / Adventure"
              }, new UploadComicFictionMaster
              {
                  Id = 2,
                  Value = "Chick Lit"
              }, new UploadComicFictionMaster
              {
                  Id = 3,
                  Value = "Classics"
              }, new UploadComicFictionMaster
              {
                  Id = 4,
                  Value = "Crime"
              }, new UploadComicFictionMaster
              {
                  Id = 5,
                  Value = "Detective"
              }, new UploadComicFictionMaster
              {
                  Id = 6,
                  Value = "Drama"
              }, new UploadComicFictionMaster
              {
                  Id=7,
                  Value = "Fairytale"
              }, new UploadComicFictionMaster
              {
                  Id=8,
                  Value = "Fantasy"
              }, new UploadComicFictionMaster
              {
                  Id=9,
                  Value = "Historical Fiction"
              }, new UploadComicFictionMaster
              {
                  Id=10,
                  Value = "Humour"
              }, new UploadComicFictionMaster
              {
                  Id=11,
                  Value = "LGBTQ"
              }, new UploadComicFictionMaster
              {
                  Id=12,
                  Value = "Mystery"
              }, new UploadComicFictionMaster
              {
                  Id=13,
                  Value = "Paranormalac"
              }, new UploadComicFictionMaster
              {
                  Id=14,
                  Value = "Political"
              }, new UploadComicFictionMaster
              {
                  Id=15,
                  Value = "Romance"
              }, new UploadComicFictionMaster
              {
                  Id=16,
                  Value = "Satire"
              }, new UploadComicFictionMaster
              {
                  Id=17,
                  Value = "Science Fiction"
              }, new UploadComicFictionMaster
              {
                  Id=18,
                  Value = "Spy"
              }, new UploadComicFictionMaster
              {
                  Id=19,
                  Value = "Superhero"
              }, new UploadComicFictionMaster
              {
                  Id=20,
                  Value = "Suspense"
              }, new UploadComicFictionMaster
              {
                  Id = 21,
                  Value = "Thriller"
              }, new UploadComicFictionMaster
              {
                  Id = 22,
                  Value = "Western"
              }, new UploadComicFictionMaster
              {
                  Id = 23,
                  Value = "Women’s Fiction"
              }, new UploadComicFictionMaster
              {
                  Id = 24,
                  Value = "War"
              }, new UploadComicFictionMaster
              {
                  Id=25,
                  Value = "Horror"
              });

            modelBuilder.Entity<UploadComicNonFictionMaster>().HasData(
            new UploadComicNonFictionMaster
            {
                Id=1,
                Value = "Art"

            }, new UploadComicNonFictionMaster
            {
                Id = 2,
                Value = "Autobiography"
            }, new UploadComicNonFictionMaster
            {
                Id = 3,
                Value = "Biography"
            }, new UploadComicNonFictionMaster
            {
                Id = 4,
                Value = "Health / Fitness"
            }, new UploadComicNonFictionMaster
            {
                Id = 5,
                Value = "History"
            }, new UploadComicNonFictionMaster
            {
                Id = 6,
                Value = "Home & Garden"
            }, new UploadComicNonFictionMaster
            {
                Id = 7,
                Value = "LGBTQ"
            }, new UploadComicNonFictionMaster
            {
                Id = 8,
                Value = "Memoir"
            }, new UploadComicNonFictionMaster
            {
                Id = 9,
                Value = "Philosophy"
            }, new UploadComicNonFictionMaster
            {
                Id = 10,
                Value = "Poetry"
            }, new UploadComicNonFictionMaster
            {
                Id = 11,
                Value = "Religion / Spirituality"
            }, new UploadComicNonFictionMaster
            {
                Id = 12,
                Value = "Review"
            }, new UploadComicNonFictionMaster
            {
                Id = 13,
                Value = "Science"
            }, new UploadComicNonFictionMaster
            {
                Id = 14,
                Value = "Self Help"
            }, new UploadComicNonFictionMaster
            {
                Id = 15,
                Value = "Sports & Leisure"
            }, new UploadComicNonFictionMaster
            {
                Id = 16,
                Value = "Travel"
            }, new UploadComicNonFictionMaster
            {
                Id = 17,
                Value = "True Crime"
            }, new UploadComicNonFictionMaster
            {
                Id = 18,
                Value = "War"
            });

            modelBuilder.Entity<UploadComicContentTypeMaster>().HasData(
            new UploadComicContentTypeMaster
            {
                Id = 1,
                Value = "Blasphemy"

            }, new UploadComicContentTypeMaster
            {
                Id = 2,
                Value = "Character Death"
            }, new UploadComicContentTypeMaster
            {
                Id = 3,
                Value = "Explicit"
            }, new UploadComicContentTypeMaster
            {
                Id = 4,
                Value = "Gore"
            }, new UploadComicContentTypeMaster
            {
                Id = 5,
                Value = "Out Dated Ideals"
            }, new UploadComicContentTypeMaster
            {
                Id = 6,
                Value = "Racism"
            }, new UploadComicContentTypeMaster
            {
                Id = 7,
                Value = "Rape / Non - consent"
            }, new UploadComicContentTypeMaster
            {
                Id = 8,
                Value = "Self Harm"
            }, new UploadComicContentTypeMaster
            {
                Id = 9,
                Value = "Sexism"
            }, new UploadComicContentTypeMaster
            {
                Id = 10,
                Value = "Swearing"
            }, new UploadComicContentTypeMaster
            {
                Id = 11,
                Value = "Violence"
            });

            modelBuilder.Entity<UploadComicTagTypeMaster>().HasData(
            new UploadComicTagTypeMaster
            {
                Id = 1,
                Value = "Stand alone"

            }, new UploadComicTagTypeMaster
            {
                Id = 2,
                Value = "Mini Series"
            }, new UploadComicTagTypeMaster
            {
                Id = 3,
                Value = "Ongoing Story"
            }, new UploadComicTagTypeMaster
            {
                Id = 4,
                Value = "Anthology"
            }, new UploadComicTagTypeMaster
            {
                Id=5,
                Value = "Graphic Novel"
            }, new UploadComicTagTypeMaster
            {
                Id = 6,
                Value = "Collection"
            }, new UploadComicTagTypeMaster
            {
                Id = 7,
                Value = "Manga"
            });





        }     
                  
    }
}
