using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cove.ClassLibrary.Model
{
    public class Assets
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AssetId { get; set; }

        [Required]
        public string AssetValue { get; set; }


    }
}
