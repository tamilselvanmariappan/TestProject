using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Test.Models
{
    public class MimeType
    {
        public MimeType() { }

        [Key]
        public int MimeTypeId { get; set; }
        [Column("Type")]
        public string Type { get; set; }

        public ICollection<Asset> Assets { get; set; }
    }
}
