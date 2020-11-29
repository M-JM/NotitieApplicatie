using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.BusinessAccessLayer
{
    public class DbRepositoryConnected : IDbRepository
    {
        public NotitieDBContext Context { get; private set; }

        public DbRepositoryConnected()
        {
            Context = new NotitieDBContext();
        }


        #region Create operations

        public Notitie AddNotitie(Notitie notitie)
        {
          Context.Notities.Add(notitie);                  
          Context.SaveChanges();
            return notitie;
        }

        public NotitieBoek AddNotitieBoek(NotitieBoek notitieBoek)
        {
            Context.NotitieBoeken.Add(notitieBoek);
            Context.SaveChanges();
            return notitieBoek;
        }

        public Categorie AddCategorie(Categorie categorie)
        {
            Context.Categorieen.Add(categorie);
            Context.SaveChanges();
            return categorie;
        }

        #endregion

        #region Read operations

        public NotitieBoek NotitieboekById(int id)
        {
            return Context.NotitieBoeken.Include(x => x.Notities.Select(p => p.Categorie)).Where(x=>x.Id == id).FirstOrDefault();
        }
        public List<NotitieBoek> Notitieboeklijst()
        {
            return Context.NotitieBoeken
                .Include(x => x.Notities)
                .ToList(); 
        }

        public Notitie NotitieById(int id)
        {
            return Context.Notities.Include(x => x.Categorie)
                                   .Where(x => x.Id == id)                     
                                   .FirstOrDefault();
        }

        public List<Notitie> Notitielijst()
        {
            return Context.Notities
                .Include(x => x.Categorie)
                .Include(x =>x.Eigenaar)
                .ToList();
        }

        public List<Categorie> Categorieenlijst()
        {
            return Context.Categorieen.ToList();
        }

        #endregion

        #region Update operations

        public Notitie UpdateNotitie(Notitie notitie)
        {
            Context.SaveChanges();
            return notitie;
        }

        public NotitieBoek UpdateNotitieBoek(NotitieBoek notitieBoek)
        {
          
            Context.SaveChanges();

            return notitieBoek;

        }

        public Categorie UpdateCategorie(Categorie categorie)
        {   
            Context.SaveChanges();
            return categorie;
        }

        #endregion

        #region Delete operations

        public void RemoveNotitie(Notitie notitie)
        {
            Context.Notities.Remove(notitie);
            Context.SaveChanges();
        }

        public void RemoveNotitieBoek(NotitieBoek notitieBoek)
        {
            Context.NotitieBoeken.Remove(notitieBoek);
            Context.SaveChanges();

        }

        public void RemoveCategorie(Categorie categorie)
        {
            Context.Categorieen.Remove(categorie);
            Context.SaveChanges();
        }

        public List<Eigenaar> Eigenaarslijst()
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
