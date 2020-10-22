using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cove.ClassLibrary.Model
{
    public class UploadedComicFiction
    {
        [Key]
        [Required]
        //[ForeignKey("UploadComic")]
        public int ComicId { get; set; }

        //[ForeignKey("UploadComicFictionMaster")]
        public int FictionId { get; set; }
    }
}

