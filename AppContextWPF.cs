using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDomashka
{
    internal class AppContextWPF : DbContext
    {
        public AppContextWPF() : base("Data Source=NIKOLAI;Initial Catalog=Domashka;Integrated Security=True;")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<User> Users { get; set; } = null;

    }
}
