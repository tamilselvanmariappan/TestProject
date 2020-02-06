using Data.Test.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Test
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=TestDBConnectionString")
        {
            this.Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer<DataContext>(new CreateDatabaseIfNotExists<DataContext>());
        }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<MimeType> MimeTypes { get; set; }
    }
}
