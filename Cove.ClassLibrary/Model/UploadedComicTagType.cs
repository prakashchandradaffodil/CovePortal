using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cove.ClassLibrary.Model
{
    public class UploadedComicTagType
    {
        [Key]
        [Required]
        //[ForeignKey("UploadComic")]
        public int ComicId { get; set; }
        //[ForeignKey("UploadComicTagTypeMaster")]
        public int TagTypeId { get; set; }
    }
}