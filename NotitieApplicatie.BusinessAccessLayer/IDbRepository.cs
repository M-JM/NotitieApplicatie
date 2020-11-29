using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.BusinessAccessLayer
{
    public interface IDbRepository 
    {
        Notitie AddNotitie(Notitie notitie);
        NotitieBoek AddNotitieBoek(NotitieBoek notitieBoek);
        Categorie AddCategorie(Categorie categorie);
        NotitieBoek NotitieboekById(int id);
        List<NotitieBoek> Notitieboeklijst();
        Notitie NotitieById(int id);
        List<Notitie> Notitielijst();
        List<Categorie> Categorieenlijst();
        Notitie UpdateNotitie(Notitie notitie);
        NotitieBoek UpdateNotitieBoek(NotitieBoek notitieBoek);
        Categorie UpdateCategorie(Categorie categorie);
        void RemoveNotitie(Notitie notitie);
        void RemoveNotitieBoek(NotitieBoek notitieBoek);
        void RemoveCategorie(Categorie categorie);
        List<Eigenaar> Eigenaarslijst();
    }
}
