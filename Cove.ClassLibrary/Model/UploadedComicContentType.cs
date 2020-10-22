using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cove.ClassLibrary.Model
{
    public class UploadedComicContentType
    {
        [Key]
        [Required]
        //[ForeignKey("UploadComic")]
        public int ComicId { get; set; }

        //[ForeignKey("UploadComicContentTypeMaster")]
        public int ContentTypeId { get; set; }
    }
}

