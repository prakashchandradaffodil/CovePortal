using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cove.ClassLibrary.Model
{
   public class UploadedComicNonFiction
    {
        [Key]
        [Required]
        //[ForeignKey("UploadComic")]
        public int ComicId { get; set; }

        //[ForeignKey("UploadComicNonFictionMaster")]
        public int NonFictionId { get; set; }
    }
}

