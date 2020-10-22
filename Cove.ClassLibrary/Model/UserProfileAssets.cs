using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cove.ClassLibrary.Model
{
    public class UserProfileAssets
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserProfileAssetId { get; set; }
        public string UserId { get; set; }
        public string AssetId { get; set; }

    }
}
