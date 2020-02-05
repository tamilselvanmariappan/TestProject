using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Test.Models
{
    public class Country
    {
        public Country() { }

        [Key]
        public int CountryId { get; set; }
        [Column("Name")]
        public string CountryName { get; set; }

        public ICollection<Asset> Assets { get; set; }
    }
}
