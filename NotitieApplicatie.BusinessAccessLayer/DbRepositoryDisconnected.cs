using NotitieApplicatie.DataAccessLayer;
using System;
using System.Data.Entity;
using MyOwnLib.Common;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using NotitieApplicatie.DataAccessLayer.Exceptions;

namespace NotitieApplicatie.BusinessAccessLayer
{
    public class DbRepositoryDisconnected : IDbRepository
    {

        #region Create operations

        public Notitie AddNotitie(Notitie notitie)
        {
            try
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {
                    notitie.Eigenaar = notitie.NotitieBoek.Eigenaar;
                    context.Notities.Add(notitie);
                    context.Eigenaars.Attach(notitie.Eigenaar);
                    context.Categorieen.Attach(notitie.Categorie);
                    context.NotitieBoeken.Attach(notitie.NotitieBoek);
                    foreach (Notitie item in notitie.NotitieBoek.Notities.ToList())
                    {
                        if(item != notitie) { 
                        context.Notities.Attach(item);
                        }
                    }
                    context.SaveChanges();
                    return notitie;
                }
            }
            catch (DbUpdateException ex)
            {
                MyLogger.GetInstance().Error("DB insert error from AddNotitie: " + ex.Message);

            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException)
                {
                    MyLogger.GetInstance().Error("SqlException from AddNotitie: " + ex.Message);
                }
                else
                {
                    MyLogger.GetInstance().Error("Uncaught Exception from AddNotitie: " + ex.Message);
                }   
            }
            notitie = null;
            return notitie;
        }

        public NotitieBoek AddNotitieBoek(NotitieBoek notitieBoek)
        {
            try
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
            catch (DbUpdateException ex)
            {
                MyLogger.GetInstance().Error("DB insert error from AddNotitieBoek: " + ex.Message);

            }

            catch (Exception ex)
            {
                if (ex.InnerException is SqlException)
                {
                    MyLogger.GetInstance().Error("SqlException from AddNotitieBoek: " + ex.Message);
                }
                else
                {
                    MyLogger.GetInstance().Error("Uncaught Exception from AddNotitieBoek: " + ex.Message);
                }
            }
            notitieBoek = null;
            return notitieBoek;
        }

        public Categorie AddCategorie(Categorie categorie)
        {
            try
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {
                    context.Categorieen.Add(categorie);
                    context.SaveChanges();
                    return categorie;
                }
            }
            catch (DbUpdateException ex)
            {
                MyLogger.GetInstance().Error("DB insert error from AddCategorie: " + ex.Message);
            }

            catch (Exception ex)
            {
                if (ex.InnerException is SqlException)
                {
                    MyLogger.GetInstance().Error("SqlException from AddCategorie: " + ex.Message);
                }
                else
                {
                    MyLogger.GetInstance().Error("Uncaught Exception from AddCategorie: " + ex.Message);
                }
            }
            categorie = null;
            return categorie;

        }
        #endregion

        #region Read operations

        public NotitieBoek NotitieboekById(int id)
        {
            try
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {
                    MyLogger.GetInstance().Info("I came from DB");
                    return context.NotitieBoeken.Include(x => x.Notities.Select(p => p.Categorie)).Where(x => x.NotitieBoekId == id).FirstOrDefault();

                }
            }
            catch (Exception)
            {

                throw;
            }
       
        }

        public FullObservableCollection<NotitieBoek> Notitieboeklijst()
            {
            try
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {
                    
                    return new FullObservableCollection<NotitieBoek>(context.NotitieBoeken
                    .Include(x => x.Notities)
                    .Include(x => x.Eigenaar)
                    .ToList());
                }
            }
            catch (Exception)
            {

                throw;
            }
          
            }

        public Notitie NotitieById(int id)
            {
            try
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {

                    return context.Notities.Include(x => x.Categorie)
                                   .Where(x => x.Id == id)
                                   .FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
               
            }

        public ObservableCollection<Notitie> Notitielijst()
            {
            try
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {

                    return new ObservableCollection<Notitie>(context.Notities
                        .Include(x => x.NotitieBoek)
                        .Include(x => x.Categorie)
                        .Include(x => x.Eigenaar)
                        .ToList());
                }
            }
            catch (Exception)
            {

                throw;
            }
         
            }

        public FullObservableCollection<Categorie> Categorieenlijst()
            {
            try
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {
                    return new FullObservableCollection<Categorie>(context.Categorieen.ToList());
                }
            }
            catch (Exception)
            {

                throw;
            }
           
            }

        public FullObservableCollection<Eigenaar> Eigenaarslijst()
        {
            try
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {
                    return new FullObservableCollection<Eigenaar>(context.Eigenaars.ToList());
                }
            }
            catch (Exception)
            {

                throw;
            }
         
        }

            #endregion

        #region Update operations

            public Notitie UpdateNotitie(Notitie notitie)
            {
            try
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {
                    context.SaveChanges();
                    return notitie;
                }
            }
            catch (Exception)
            {

                throw;
            }
              
            }

            public NotitieBoek UpdateNotitieBoek(NotitieBoek notitieBoek)
            {
            try
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {

                    if (!context.NotitieBoeken.Local.Contains(notitieBoek))
                    {

                        if (notitieBoek.EigenaarId != notitieBoek.Eigenaar.EigenaarId)
                        {
                            notitieBoek.EigenaarId = notitieBoek.Eigenaar.EigenaarId;

                            if (notitieBoek.Notities != null)
                            {

                                foreach (var child in notitieBoek.Notities.ToList())
                                {
                                    context.Notities.Attach(child);
                                    child.EigenaarId = notitieBoek.EigenaarId;
                                    context.SaveChanges();
                                }
                            }
                            context.Entry(notitieBoek).State = EntityState.Modified;
                            context.SaveChanges();
                        }
                        else
                        {
                            var test = context.NotitieBoeken.FirstOrDefault(x=> x.NotitieBoekId == notitieBoek.NotitieBoekId);
                            context.Entry(test).CurrentValues.SetValues(notitieBoek);
                            context.Entry(test).State = EntityState.Modified;
                            context.SaveChanges();
                        }  

                    }

                    return notitieBoek;

                }
            }
            catch (DbUpdateException ex)
            {
                MyLogger.GetInstance().Error("DB insert error from UpdateNotitieBoek: " + ex.Message);

            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException)
                {
                    MyLogger.GetInstance().Error("SqlException from UpdateNotitieBoek: " + ex.Message);
                }
                else
                {
                    MyLogger.GetInstance().Error("Uncaught Exception from UpdateNotitieBoek: " + ex.Message);
                }
            }
            notitieBoek = null;
            return notitieBoek;

        }

            public Categorie UpdateCategorie(Categorie categorie)
            {
            try
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {
                    context.Entry(categorie).State = EntityState.Modified;
                    context.SaveChanges();
                    return categorie;
                }
            }
            catch (Exception)
            {

                throw;
            }
          
            }

            #endregion

        #region Delete operations

            public Boolean RemoveNotitie(Notitie notitie)
            {
            try
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {
                    Notitie VerwijderdeNotitie = context.Notities.Where(x => x.Id == notitie.Id).FirstOrDefault();
                    if (VerwijderdeNotitie != null)
                    {
                        context.Notities.Remove(VerwijderdeNotitie);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception();
                    }


                }
            }
            catch (Exception)
            {

                return false;

            }
              
            }

            public Boolean RemoveNotitieBoek(NotitieBoek notitieBoek)
            {

            try
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {
                    if ((notitieBoek = context.NotitieBoeken.Include(x => x.Notities).FirstOrDefault(x => x.NotitieBoekId == notitieBoek.NotitieBoekId)) != null)
                    {
                        foreach (var child in notitieBoek.Notities.ToList())
                            context.Notities.Remove(child);
                        context.SaveChanges();
                        context.NotitieBoeken.Remove(notitieBoek);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception();
                    }

                }
            }
            catch (Exception)
            {
                return false;
            }
             
            }

            public Boolean RemoveCategorie(Categorie categorie)
            {
            try
            {
                using (NotitieDBContext context = new NotitieDBContext())
                {
                    if ((categorie = context.Categorieen.FirstOrDefault(x => x.CategorieId == categorie.CategorieId)) != null)

                        context.Categorieen.Remove(categorie);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException)
                {
                    MyLogger.GetInstance().Error("SqlException from AddNotitie: " + ex.Message + ex.InnerException);
                }
                else
                {
                    MyLogger.GetInstance().Error("Exception from AddNotitie: " + ex.Message + ex.InnerException);
     
                }
            }

            catch(Exception ex)
            {
                MyLogger.GetInstance().Error(" Unspecified Exception from AddNotitie: " + ex.Message + ex.InnerException);
             
            }
            return false;

        }

        #endregion

    }
}
