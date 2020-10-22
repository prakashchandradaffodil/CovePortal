using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace Cove.ClassLibrary.Model
{
   public class UploadComic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        //[ForeignKey("Asset")]
        public string UploadComicAssetId { get; set; }       

        [Required]
        [Display(Name = "Creator(s)")]
        public string Creator { get; set; }

        [Display(Name = "Series Title")]
        public string SeriesTitle { get; set; }

        [Display(Name = "Issue title")]
        public string IssueTitle { get; set; }


        [Required(ErrorMessage = "Price is required")]
        [RegularExpression(@"^\d+\.\d{0,4}$", ErrorMessage = "Enter Price (4 digit after decimal allowed)")]
        [DisplayName("Price")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Issue Number")]
        [StringLength(4, ErrorMessage = "Issue Number can not be more than 4 digit")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid Issue Number (4 digit numeric allowed)")]

        public string IssueNumber { get; set; }

        public int SelectedAgeSuitabilityId { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(1000, ErrorMessage = "Description  length shoud be between 10-1000 characters.", MinimumLength = 10)]
        public string Description { get; set; }


        public string UploadComicThumbnailAssetId { get; set; }

        public bool isPublishMyComic { get; set; }

        public string Role { get; set; }
    }
}

      