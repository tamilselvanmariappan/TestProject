using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Test.Models
{
    public class Asset
    {
        public Asset() { }

        [Key]
        public string AssetId { get; set; }
        public string FileName { get; set; }
        public string CreatedBy { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "date")]
        public DateTime CreatedOn { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }
        [ForeignKey("MimeType")]
        public int MimeTypeId { get; set; }
        public MimeType MimeType { get; set; }
    }
}
