using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyOwnLib.Common;
using System.Collections.ObjectModel;

namespace NotitieApplicatie.BusinessAccessLayer
{
    public class DbRepositoryDisconnected : IDbRepository
    {

        #region Create operations

        public Notitie AddNotitie(Notitie notitie)
        {
            using (NotitieDBContext context = new NotitieDBContext())
            {
                context.Notities.Add(notitie);
                context.SaveChanges();
                return notitie;
            }
        }

        public NotitieBoek AddNotitieBoek(NotitieBoek notitieBoek)
        {
            using (NotitieDBContext context = new NotitieDBContext())
            {
                context.NotitieBoeken.Add(notitieBoek);
                
                // noodzakelijk om EF te laten weten dat die eigenaar die meegeven wordt hier niet weer moet ingevoegd worden als nieuwe record
                // Dit komt omdate de context alleen notitieboek trackt en dus niets weet van Eigenaar context (dus kan niet weten of hij reeds besta)
                // door de eigenaar "vast te hangen" aan de notitieboek context zal deze niet als een nieuwe record nserten bij SaveChanges.

                //https://stackoverflow.com/questions/7884887/prevent-entity-framework-to-insert-values-for-navigational-properties
                //https://entityframework.net/knowledge-base/48816929/prevent-adding-new-record-on-related-table-entity-in-entity-framework

                context.Eigenaars.Attach(notitieBoek.Eigenaar);
                context.SaveChanges();
                return notitieBoek;
            }
        }

        public Categorie AddCategorie(Categorie categorie)
        {
            using (NotitieDBContext context = new NotitieDBContext())
            {
                context.Categorieen.Add(categorie);
                context.SaveChanges();
                return categorie;
            }
        }
        #endregion

        #region Read operations

        public NotitieBoek NotitieboekById(int id)
        {
            using (NotitieDBContext context = new NotitieDBContext())
            {
                return context.NotitieBoeken.Include(x => x.Notities.Select(p => p.Categorie)).Where(x => x.Id == id).FirstOrDefault();
            }
        }

        public FullObservableCollection<NotitieBoek> Notitieboeklijst()
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {
                return new FullObservableCollection<NotitieBoek>(context.NotitieBoeken
                .Include(x => x.Notities)
                .Include(x => x.Eigenaar)
                .ToList());
                }
            }

            public Notitie NotitieById(int id)
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {
                    return context.Notities.Include(x => x.Categorie)
                               .Where(x => x.Id == id)
                               .FirstOrDefault();
                }
            }

            public ObservableCollection<Notitie> Notitielijst()
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {
                    return new ObservableCollection<Notitie>( context.Notities
                    .Include(x => x.NotitieBoek)
                    .Include(x => x.Categorie)
                    .Include(x => x.Eigenaar)
                    .ToList());
                }
            }
            public FullObservableCollection<Categorie> Categorieenlijst()
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {
                    return new FullObservableCollection<Categorie>(context.Categorieen.ToList());
                }
            }

        public FullObservableCollection<Eigenaar> Eigenaarslijst()
        {
            using (NotitieDBContext context = new NotitieDBContext())
            {
                return new FullObservableCollection<Eigenaar>(context.Eigenaars.ToList());
            }
        }

            #endregion

        #region Update operations

            public Notitie UpdateNotitie(Notitie notitie)
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {
                    context.SaveChanges();
                    return notitie;
                }
            }

            public NotitieBoek UpdateNotitieBoek(NotitieBoek notitieBoek)
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {

                if (!context.NotitieBoeken.Local.Contains(notitieBoek))
                {
                    context.Eigenaars.Attach(notitieBoek.Eigenaar);
                    context.NotitieBoeken.Attach(notitieBoek);
                    context.Entry(notitieBoek).State = EntityState.Modified;
                }
                context.SaveChanges();
                return notitieBoek;

            }
        }

            public Categorie UpdateCategorie(Categorie categorie)
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {
                    context.SaveChanges();
                    return categorie;
                }
            }

            #endregion

        #region Delete operations

            public void RemoveNotitie(Notitie notitie)
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {
               

                    context.Notities.Remove(notitie);
                    context.SaveChanges();
                }
            }

            public void RemoveNotitieBoek(NotitieBoek notitieBoek)
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {
                   if((notitieBoek = context.NotitieBoeken.Include(x => x.Notities).FirstOrDefault( x => x.Id == notitieBoek.Id)) != null)
                {
                    foreach (var child in notitieBoek.Notities.ToList())
                    context.Notities.Remove(child);
                    context.SaveChanges();
                    context.NotitieBoeken.Remove(notitieBoek);
                    context.SaveChanges();

                }
              
                }
            }

            public void RemoveCategorie(Categorie categorie)
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {
                    context.Categorieen.Remove(categorie);
                    context.SaveChanges();
                }

            }
            #endregion

    }
}
