using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace NotitieApplicatie.DataAccessLayer
{
    public class NotitieDBContext : DbContext
    {
        #region Properties & Fields

        
        public DbSet<Notitie> Notities { get; set; }

        public DbSet<Categorie> Categorieen { get; set; }

        public DbSet<NotitieBoek> NotitieBoeken { get; set; }

        public DbSet<Eigenaar> Eigenaars { get; set; }

        #endregion

        #region Constructor

        public NotitieDBContext()
            :base("MijnNotitieApplicatie")
        {
            Database.SetInitializer<NotitieDBContext>(new ModelBuilder());
        }

        #endregion
    }
}
